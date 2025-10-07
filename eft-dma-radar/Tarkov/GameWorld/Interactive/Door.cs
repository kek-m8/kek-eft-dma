using eft_dma_radar;
using eft_dma_radar.Tarkov.EFTPlayer;
using eft_dma_radar.Tarkov.GameWorld;
using eft_dma_radar.UI.ESP;
using eft_dma_radar.UI.Misc;
using eft_dma_radar.UI.Radar;
using eft_dma_shared.Common.Maps;
using eft_dma_shared.Common.Misc;
using eft_dma_shared.Common.Misc.Data;
using eft_dma_shared.Common.Players;
using eft_dma_shared.Common.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static eft_dma_shared.Common.Misc.Data.EftDataManager;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LonesEFTRadar.Tarkov.GameWorld.Interactive
{
    public sealed class Door : IWorldEntity, IMapEntity, IMouseoverEntity, IESPEntity
    {
        private static readonly uint[] _transformInternalChain =
        [
            ObjectClass.MonoBehaviourOffset,
            MonoBehaviour.GameObjectOffset,
            GameObject.ComponentsOffset,
            0x8
        ];
        public ulong Base { get; set; }
        public string Id { get; set; }
        public static Config Config { get; } = Program.Config;
        public string DoorState { get; set; }

        private EDoorType _type { get; set; }
        private enum EDoorState : byte
        { 
            None,
            Locked,
            Shut,
            Open = 4,
            Interacting = 8,
            Breaching = 16
        }

        private enum EDoorType : byte
        {
            Door,
            DoorSwitch,
            KeycardDoor
        }

        private string lockBmp = "iVBORw0KGgoAAAANSUhEUgAAABIAAAASCAYAAABWzo5XAAADR0lEQVR4nGVUTYgcRRT+XlX/Tc/s9qzDZJw9iLt4kwxGXUQIiCCiHo0IOQniTTyIkENOuQriTRDxFA+CB8nBg4IHg5BVgiZeEjLBaDDJJu5mdqZ3tn+qu+pJ1c7MuqSgiqL69fe+99X3inA4iIiYmRMAGwAiAMae/y+GAQgAV4joLjPT7AzeHMQug8GgnWXZl89uvHAqbi3DaG3hbQYwMzxPYjoZY3Pz0k+tuHfq2rVfxvMEcyAbzMPhsHX8+ODEO+++Z1rLHV1kU18KdrQMA3GzWe2Nd8WtW38P/rhyuQvQCGBxBMiOPM9zVel/8yxbv3ljyJ9+8nHaCMMHlnuhisc/PHO22e+vUl3XihTpWVVuHAFKEhg/EKIRRYBW8vZff34B4Nys9HOoy4+W4qj2A0HhcshlWj4C5ISeTLi3dS9rj8YZRqktKH6i2XtxQ4tKF1s/90dphd2Jxp2tLEjTNCZBYOMEd5kkAN1otN566fngsye7urv0WIeKWmOyM0YUhfZuOC9K0V1egR9I83C8LR6OvN8v/Ba8hun97Tkjh5jn+TNnPnju2MuvFNrcy6Tw9gCvwdAMmIggYkBLGDZCrK7j4vfHTlw4fakLwAKJhUZCsDp//iZfvehjP1c4/abE2lMVGVWBjYGMSty47eHrb0M0gwrX/xnVSbcTTLanR8Vm4zffeHWVXj+5jb2xxEqbgKqGdIQ9kCKs94D3347QTALzw6+R/9V3w5MArh5hFKLht5Mcrf4O4jCEIAPW1oxWxQLMHoJQo9ffBdoBknbXNMMluV8WMIbNAsjzpaeNAvIIWhHIZ2cztnYhBRYaxMZ9k2UtjDbGp/g6z6y0ADKOQABIAkQJcOBajdzthgddJ2to4UGSz4ZAo+xBMjOlEAuNdFGG1u1awNMeSLCbsJMO9mQEfKMBWUEyaCXpJEK4Sz8sTUGprK5RKsmqADz7w5z3IpsHW75UzEUZEVdT9UhpGqb+/JtA/7i5xtOqdjLbpj94QxgMgmRGzRJh4OH+3UnFfnaHM4dEh70m2GyNmtKIjtyvDBzjOaHZXpB2QIEXId0pZJali2fEhriwMAzXyrJ8GkA9O5s/araFDonP0tql1cLl6dQ5m/4DAeKVAw9iSf4AAAAASUVORK5CYII=";

        public string? KeyId { get; set; }
        public string KeyName { get; set; }

        public string DoorName { get; set; }

        private Vector3 _position;
        public ref Vector3 Position => ref _position;
        public bool CanBeBreached { get; set; }
        public Vector2 MouseoverPosition { get; set; }

        public Door(ulong ptr, string name)
        {
            if (!Utils.IsValidVirtualAddress(ptr) || ptr == 0x0)
                return;
            Enum.GetValues<EDoorType>().ToList().ForEach(x =>
            {
                if (name.Equals(x.ToString()))
                    _type = x;
            });
            try
            {
                Base = ptr;
                var keyidPtr = Memory.ReadPtr(Base + Offsets.Interactable.KeyId, false);
                var doorIdPtr = Memory.ReadPtr(Base + Offsets.Interactable.Id, false);
                var transformInternal = Memory.ReadPtrChain(Base, _transformInternalChain, false);
                var transform = new UnityTransform(transformInternal);

                _position = transform.UpdatePosition();
                KeyId = Memory.ReadUnityString(keyidPtr);
                Id = Memory.ReadUnityString(doorIdPtr);
                if (EftDataManager.AllItems.TryGetValue(KeyId, out var item))
                {
                    DoorName = item.Name.Replace((name.Contains("keycard") ? "keycard" : "key"), "");
                    KeyName = item.Name;
                }
                else
                {
                    KeyName = "NULL";
                }
                DoorState = Enum.GetName((EDoorState)Memory.ReadValue<byte>(Base + Offsets.Interactable._doorState, false));

                CanBeBreached = Memory.ReadValue<bool>(Base + Offsets.Interactable.CanBeBreached, false);
            }
            catch { }
        }

        public void DrawESP(SKCanvas canvas, LocalPlayer localPlayer)
        {
            var doorPosition = _position;
            if ((string.IsNullOrEmpty(KeyName) || KeyName == "NULL") ||
                Config.ESP.DoorViewerBlacklist?.Contains(Id) == true ||
                !CameraManager.WorldToScreen(ref doorPosition, out var scrPos) ||
               Vector3.Distance(_position, localPlayer.Position) > Config.ESP.DrawDoorDistance)
                return;
            if (Config.ESP.DoorHeightCheck)
            {
                var heightDiff = Position.Y - localPlayer.Position.Y;
                if (heightDiff > 1.85f)
                {
                    return;
                }
                else if (heightDiff < -1.85f)
                {
                    return;
                }
            }
            var lines = new List<string> { DoorName, DoorState, Utils.GetDistPretty(Position, localPlayer.Position) };
            var basePaint = SKPaints.TextCorpseESP;
            float startY = scrPos.Y - (lines.Count * basePaint.TextSize) / 2 + basePaint.TextSize;


            var textPt = new SKPoint(scrPos.X, startY);
            textPt.DrawESPText(canvas, this, localPlayer, false, basePaint, DoorName);
            textPt.Y += basePaint.TextSize;

            using (var statePaint = new SKPaint())
            {
                statePaint.TextSize = basePaint.TextSize;
                statePaint.IsAntialias = basePaint.IsAntialias;
                statePaint.Typeface = basePaint.Typeface;
                statePaint.Style = basePaint.Style;
                statePaint.StrokeWidth = basePaint.StrokeWidth;
                statePaint.TextAlign = SKTextAlign.Center;

                switch (DoorState)
                {
                    case "Locked": statePaint.Color = SKColors.Red; break;
                    case "Open": statePaint.Color = SKColors.MediumSeaGreen; break;
                    case "Shut": statePaint.Color = SKColors.Yellow; break;
                    case "Interacting": statePaint.Color = SKColors.Orange; break;
                    case "Breaching": statePaint.Color = SKColors.Magenta; break;
                    default: statePaint.Color = SKColors.LightGray; break;
                }

                canvas.DrawText(DoorState, textPt, statePaint);

                textPt.Y += basePaint.TextSize;
                textPt.DrawESPText(canvas, this, localPlayer, false, basePaint, Utils.GetDistPretty(Position, localPlayer.Position));
            }
        }
        public void Refresh()
        {
            DoorState = Enum.GetName((EDoorState)Memory.ReadValue<byte>(Base + Offsets.Interactable._doorState, false));
        }

        public override string ToString()
        {
            // What will be shown in the CheckedListBox
            return !string.IsNullOrEmpty(KeyName) && KeyName != "NULL"
                ? $"{KeyName} ({Id})"
                : Id;
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

        public void Draw(SKCanvas canvas, LoneMapParams mapParams, ILocalPlayer localPlayer)
        {
            if(KeyName == "NULL" ||
               Config.ESP.DoorViewerBlacklist?.Contains(Id) == true ||
               Vector3.Distance(Position, localPlayer.Position) > Config.DoorDrawDistance)
                return;
            var heightDiff = Position.Y - localPlayer.Position.Y;
            var point = Position.ToMapPos(mapParams.Map).ToZoomedPos(mapParams);
            MouseoverPosition = new Vector2(point.X, point.Y);
            if (heightDiff > 1.85f)
            {
                using var path = point.GetArrow(6.5f);
                canvas.DrawPath(path, SKPaints.ShapeOutline);
                canvas.DrawPath(path, SKPaints.DoorViewerPaint);
            }
            else if (heightDiff < -1.85f)
            {
                using var path = point.GetArrow(6.5f, false);
                canvas.DrawPath(path, SKPaints.ShapeOutline);
                canvas.DrawPath(path, SKPaints.DoorViewerPaint);
            }
            else
            {
                DrawCustomImage(ref lockBmp, canvas, new SKPoint(point.X, point.Y - (20.0f * MainForm.UIScale)));
            }
        }

        public void DrawMouseover(SKCanvas canvas, LoneMapParams mapParams, LocalPlayer localPlayer)
        {
            List<string> lines = new();
            lines.Add($"Key: {KeyName}");
            lines.Add($"Door ID: {Id}");
            lines.Add($"State: {DoorState}");
            lines.Add($"Type: {_type}");
            lines.Add($"CanBeBreached: {CanBeBreached}");
            Position.ToMapPos(mapParams.Map).ToZoomedPos(mapParams).DrawMouseoverText(canvas, lines, true);
        }
    }
}
