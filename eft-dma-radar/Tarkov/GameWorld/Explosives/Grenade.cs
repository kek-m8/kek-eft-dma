using eft_dma_radar.Tarkov.EFTPlayer;
using eft_dma_radar.UI.ESP;
using eft_dma_radar.UI.Misc;
using eft_dma_radar.UI.Radar;
using eft_dma_shared.Common.ESP;
using eft_dma_shared.Common.Maps;
using eft_dma_shared.Common.Misc;
using eft_dma_shared.Common.Misc.Data;
using eft_dma_shared.Common.Players;
using eft_dma_shared.Common.Unity;
using Microsoft.AspNetCore.Http.HttpResults;
using OpenTK.Graphics.OpenGL;

namespace eft_dma_radar.Tarkov.GameWorld.Explosives
{
    /// <summary>
    /// Represents a 'Hot' grenade in Local Game World.
    /// </summary>
    public sealed class Grenade : IExplosiveItem, IWorldEntity, IMapEntity, IESPEntity
    {
        public static implicit operator ulong(Grenade x) => x.Addr;
        private static readonly uint[] _toPosChain =
            ObjectClass.To_GameObject.Concat(new uint[] { GameObject.ComponentsOffset, 0x8, 0x38 }).ToArray();
        private readonly Stopwatch _sw = Stopwatch.StartNew();
        private readonly ConcurrentDictionary<ulong, IExplosiveItem> _parent;

        private readonly Config _config = Program.Config;

        /// <summary>
        /// Base Address of Grenade Object.
        /// </summary>
        public ulong Addr { get; }

        /// <summary>
        /// Position Pointer for the Vector3 location of this object.
        /// </summary>
        private ulong PosAddr { get; }

        /// <summary>
        /// True if grenade is currently active.
        /// </summary>
        public bool IsActive => _sw.Elapsed.TotalSeconds < 12f;

        /// <summary>
        /// True if the grenade has detonated.
        /// Doesn't work on smoke grenades.
        /// </summary>
        private bool IsDetonated
        {
            get
            {
                return Memory.ReadValue<bool>(this + Offsets.Grenade.IsDestroyed, false);
            }
        }

        /// <summary>
        /// Get name of grenade
        /// </summary>
        public string getName()
        {
            var weaponSource = Memory.ReadPtr(this + Offsets.Grenade.WeaponSource, false);
            var template = Memory.ReadPtr(weaponSource + Offsets.LootItem.Template, false);
            var idPtr = Memory.ReadValue<Types.MongoID>(template + Offsets.ItemTemplate._id, false);
            var id = Memory.ReadUnityString(idPtr.StringID, useCache: false);
            ID = id;
            if (EftDataManager.AllItems.TryGetValue(id, out var item))
            {
                return item.ShortName;
            }
            else
            {
                return null;
            }
        }

        public string Name;
        private string ID;
        public Grenade(ulong baseAddr, ConcurrentDictionary<ulong, IExplosiveItem> parent)
        {
            Addr = baseAddr;
            _parent = parent;
            if (IsDetonated)
                throw new Exception("Grenade is already detonated.");
            PosAddr = Memory.ReadPtrChain(baseAddr, _toPosChain, false);
            Name = getName();
            Refresh();
        }

        /// <summary>
        /// Get the updated Position of this Grenade.
        /// </summary>
        public void Refresh()
        {
            if (!this.IsActive)
            {
                return;
            }
            else if (IsDetonated)
            {
                _parent.TryRemove(this, out _);
                return;
            }
            Position = Memory.ReadValue<Vector3>(PosAddr + 0x90, false);
        }

        #region Interfaces

        private Vector3 _position;
        public ref Vector3 Position => ref _position;

        public void Draw(SKCanvas canvas, LoneMapParams mapParams, ILocalPlayer localPlayer)
        {
            if (!IsActive)
                return;
            var circlePosition = Position.ToMapPos(mapParams.Map).ToZoomedPos(mapParams);
            var size = 5 * MainForm.UIScale;
            SKPaints.ShapeOutline.StrokeWidth = SKPaints.PaintExplosives.StrokeWidth + 2f * MainForm.UIScale;
            if(Name is not null)
            {
                if(GameData.GrenadeData.TryGetValue(Name, out var grenadeData))
                {
                    SKPoint grenadePos = new SKPoint(circlePosition.X - (11f * MainForm.UIScale), circlePosition.Y - (15f * MainForm.UIScale));
                    if (grenadeData != null)
                    {
                        DrawCustomImage(ref grenadeData, canvas, grenadePos);
                        canvas.DrawText(Name, new SKPoint(circlePosition.X, circlePosition.Y + 20f * MainForm.UIScale), SKPaints.TextOutline);
                        canvas.DrawText(Name, new SKPoint(circlePosition.X, circlePosition.Y + 20f * MainForm.UIScale), SKPaints.TextGrenade);
                    }
                }
                else
                {
                    canvas.DrawCircle(circlePosition, size, SKPaints.ShapeOutline); // Draw outline
                    canvas.DrawCircle(circlePosition, size, SKPaints.PaintExplosives); // draw LocalPlayer marker
                }
            }
             
        }


        List<Vector3> _trailWorld = new();
        int maxTrailLength = 250;

        public void DrawESP(SKCanvas canvas, LocalPlayer localPlayer)
        {
            if (!_config.ESP.ShowGrenades)
                return;
            if (!IsActive)
                return;
            if (Vector3.Distance(localPlayer.Position, Position) > ESP.Config.GrenadeDrawDistance)
                return;
            if (!CameraManagerBase.WorldToScreen(ref _position, out var scrPos))
                return;
            if(_config.ESP.ShowGrenadeTracer)
            {
                _trailWorld.Add(Position);
                if (_trailWorld.Count > maxTrailLength)
                    _trailWorld.RemoveAt(0);
                var screenTrail = new List<SKPoint>();
                foreach (var wp in _trailWorld)
                {
                    var worldPos = wp;
                    if (CameraManagerBase.WorldToScreen(ref worldPos, out var sp, true, true))
                        screenTrail.Add(sp);
                }
                if (screenTrail.Count > 1)
                {
                    using var paint = new SKPaint
                    {
                        Style = SKPaintStyle.Stroke,
                        StrokeWidth = 3f * ESP.Config.LineScale,
                        IsAntialias = true,
                        StrokeCap = SKStrokeCap.Round,
                        Shader = SKShader.CreateLinearGradient(
                            screenTrail[0],
                            screenTrail[screenTrail.Count - 1],
                            new[] { SKColors.Transparent, SKColors.Yellow.WithAlpha(220) },
                            null,
                            SKShaderTileMode.Clamp
                        )
                    };

                    using var path = new SKPath();
                    path.MoveTo(screenTrail[0]);
                    for (int i = 1; i < screenTrail.Count; i++)
                        path.LineTo(screenTrail[i]);

                    canvas.DrawPath(path, paint);
                }
            }
            if (Name is not null && _config.ESP.ShowGrenadeIcons)
            {
                if (GameData.GrenadeData.TryGetValue(Name, out var grenadeData))
                {
                    SKPoint grenadePos = scrPos;
                    grenadePos.Y -= 15f;
                    grenadePos.X -= 11f;
                    if (grenadeData != null)
                    {
                        DrawCustomImage(ref grenadeData, canvas, grenadePos);
                    }
                }
                else
                {
                    goto notFound;
                }

                if (_config.ESP.ShowGrenadeName)
                {
                    var grenadeName =
                        new List<string> { Name, Utils.GetDistPretty(Position, localPlayer.Position) };
                    new SKPoint { X = scrPos.X, Y = scrPos.Y + 20f }
                        .DrawESPText(canvas, this, localPlayer, false, SKPaints.TextImpLootESP, grenadeName?.ToArray());
                }

                return;
            }

            if (_config.ESP.ShowGrenadeName)
            {
                var grenadeName =
                    new List<string> { Name, Utils.GetDistPretty(Position, localPlayer.Position) };
                new SKPoint { X = scrPos.X, Y = scrPos.Y + 20f }
                    .DrawESPText(canvas, this, localPlayer, false, SKPaints.TextImpLootESP, grenadeName?.ToArray());
            }

        notFound:
            var circleRadius = 8f * ESP.Config.LineScale;
            canvas.DrawCircle(scrPos, circleRadius, SKPaints.PaintGrenadeESP);
        }


        public void DrawCustomImage(ref string bitMap, SKCanvas canvas, SKPoint point)
        {
            using var bitmap = SKBitmap.Decode(new MemoryStream(Convert.FromBase64String(bitMap)));
            using var image = SKImage.FromBitmap(bitmap);
            var paint = new SKPaint
            {
                IsAntialias = true,
                FilterQuality = SKFilterQuality.High
            };
            canvas.DrawImage(image, point, paint);
        }

        #endregion
    }
}
