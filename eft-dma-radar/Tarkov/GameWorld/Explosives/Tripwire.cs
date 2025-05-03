using eft_dma_radar.Tarkov.EFTPlayer;
using eft_dma_radar.UI.ESP;
using eft_dma_radar.UI.Misc;
using eft_dma_radar.UI.Radar;
using eft_dma_shared.Common.ESP;
using eft_dma_shared.Common.Maps;
using eft_dma_shared.Common.Misc.Data;
using eft_dma_shared.Common.Players;
using eft_dma_shared.Common.Unity;

namespace eft_dma_radar.Tarkov.GameWorld.Explosives
{
    /// <summary>
    /// Represents a Tripwire (with attached Grenade) in Local Game World.
    /// </summary>
    public sealed class Tripwire : IExplosiveItem, IWorldEntity, IMapEntity, IESPEntity
    {
        public static implicit operator ulong(Tripwire x) => x.Addr;

        public Config _config = Program.Config;

        /// <summary>
        /// Base Address of Grenade Object.
        /// </summary>
        public ulong Addr { get; }

        /// <summary>
        /// True if the Tripwire is in an active state.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// Name of grenade
        /// </summary>
        /// 
        public string getName()
        {
            if (IsActive)
            {
                var idPtr = Memory.ReadValue<Types.MongoID>(this + Offsets.TripwireSynchronizableObject.GrenadeTemplateId, false);
                var id = Memory.ReadUnityString(idPtr.StringID, useCache: false);
                if (EftDataManager.AllItems.TryGetValue(id, out var item))
                {
                    return item.ShortName;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public string Name;

        public Tripwire(ulong baseAddr)
        {
            Addr = baseAddr;
            this.IsActive = GetIsTripwireActive(false);
            if (this.IsActive)
            {
                _position = GetPosition(useCache:false);
                Name = getName();
            }
        }

        public void Refresh()
        {
            this.IsActive = GetIsTripwireActive();
            if (this.IsActive)
            {
                this.Position = GetPosition();
            }
        }

        private bool GetIsTripwireActive(bool useCache = true)
        {
            var status = (Enums.ETripwireState)Memory.ReadValue<int>(this + Offsets.TripwireSynchronizableObject._tripwireState, useCache);
            return status is Enums.ETripwireState.Wait || status is Enums.ETripwireState.Active;
        }
        private Vector3 GetPosition(bool toPos = true, bool useCache = true)
        {
            var x = Memory.ReadValue<Vector3>(this + (toPos ? Offsets.TripwireSynchronizableObject.ToPosition : Offsets.TripwireSynchronizableObject.FromPosition), useCache);
            x.Y += 0.175f; // Raise the grenade position a bit
            return x;
        }
        private List<SKPoint> GetTripwireLine()
        {
            Vector3 ToPosition = GetPosition(), FromPosition = GetPosition(false);
            if (!CameraManager.WorldToScreen(ref ToPosition, out var toScreenPos) || !CameraManager.WorldToScreen(ref FromPosition, out var fromScreenPos))
                return null;
            return new List<SKPoint> { toScreenPos, fromScreenPos };
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
            canvas.DrawCircle(circlePosition, size, SKPaints.ShapeOutline); // Draw outline
            canvas.DrawCircle(circlePosition, size, SKPaints.PaintExplosives); // draw LocalPlayer marker
        }

        public void DrawESP(SKCanvas canvas, LocalPlayer localPlayer)
        {
            if (!_config.ESP.ShowTripwires)
                return;
            if (!IsActive)
                return;
            if (Vector3.Distance(localPlayer.Position, Position) > ESP.Config.GrenadeDrawDistance)
                return;
            if (!CameraManagerBase.WorldToScreen(ref _position, out var scrPos))
                return;
            if(_config.ESP.ShowTripwireLine)
            {
                var tripLine = GetTripwireLine();
                if (tripLine != null)
                {
                    SKPaints.PaintGrenadeESP.StrokeWidth = 2f * ESP.Config.LineScale;
                    canvas.DrawLine(tripLine[0], tripLine[1], SKPaints.PaintGrenadeESP);
                }
            }
            if (Name is not null && _config.ESP.ShowTripwireIcon)
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
                if(_config.ESP.ShowTripwireName)
                    canvas.DrawText(Name, new SKPoint { X = scrPos.X, Y = scrPos.Y + 20f }, SKPaints.TextImpLootESP);
                return;
            }
            if (_config.ESP.ShowTripwireName)
                canvas.DrawText(Name, new SKPoint { X = scrPos.X, Y = scrPos.Y + 20f }, SKPaints.TextImpLootESP);
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
