using eft_dma_radar.Tarkov.EFTPlayer;
using eft_dma_radar.UI.ESP;
using eft_dma_radar.UI.Misc;
using eft_dma_radar.UI.Radar;
using eft_dma_shared.Common.ESP;
using eft_dma_shared.Common.Maps;
using eft_dma_shared.Common.Misc;
using eft_dma_shared.Common.Misc.Commercial;
using eft_dma_shared.Common.Misc.Data;
using eft_dma_shared.Common.Players;
using eft_dma_shared.Common.Unity;
using eft_dma_shared.Common.Unity.Collections;
using Microsoft.AspNetCore.DataProtection;

namespace eft_dma_radar.Tarkov.GameWorld.Exits
{
    public sealed class Exfil : IExitPoint, IWorldEntity, IMapEntity, IMouseoverEntity, IESPEntity
    {
        public static implicit operator ulong(Exfil x) => x._addr;
        private static readonly uint[] _transformInternalChain =
{
            ObjectClass.MonoBehaviourOffset, MonoBehaviour.GameObjectOffset, GameObject.ComponentsOffset, 0x8
        };

        private readonly bool _isPMC;
        private HashSet<string> PmcEntries { get; } = new(StringComparer.OrdinalIgnoreCase);
        private HashSet<string> ScavIds { get; } = new(StringComparer.OrdinalIgnoreCase);

        public Exfil(ulong baseAddr, bool isPMC)
        {
            _addr = baseAddr;
            _isPMC = isPMC;
            var transformInternal = Memory.ReadPtrChain(baseAddr, _transformInternalChain, false);
            var namePtr = Memory.ReadPtrChain(baseAddr, new[] { Offsets.Exfil.Settings, Offsets.ExfilSettings.Name });
            Name = Memory.ReadUnityString(namePtr)?.Trim();
            if (string.IsNullOrEmpty(Name))
                Name = "default";
            // Lookup real map name (if possible)
            if (GameData.ExfilNames.TryGetValue(Memory.MapID, out var mapExfils)
                && mapExfils.TryGetValue(Name, out var exfilName))
                Name = exfilName;
            #region Secret Name Filtering
            switch(Name.ToLower())
            {
                /// Customs
                case "customs_secret_voron_boat":
                    Name = "Smugglers' Boat (Secret)";
                    break;
                case "customs_secret_voron_bunker":
                    Name = "Smugglers' Bunker (ZB-1012) (Secret)";
                    break;
                case "customs_sniper_exit":
                    Name = "Railroad Passage (Flare)";
                    break;
                case "custom_scav_pmc":
                    Name = "Boiler Room Basement (Co-op)";
                    break;
                /// Streets
                case "streets_secret_onyx":
                    Name = "Smugglers' Basement (Secret)";
                    break;
                /// Shoreline
                case "shoreline_secret_heartbeat":
                    Name = "Mountain Bunker (Secret)";
                    break;
                /// Factory
                case "factory_secret_ark":
                    Name = "Smugglers' Passage (Secret)";
                    break;
                /// Ground Zero
                case "groundzero_secret_adaptation":
                    Name = "Tartowers Sales Office (Secret)";
                    break;
                /// Woods
                case "wood_sniper_exit":
                    Name = "Power Line Passage (Flare)";
                    break;
                case "woods_secret_minefield":
                    Name = "Railway Bridge to Tarkov (Secret)";
                    break;
                /// Reserve
                case "reserve_secret_minefield":
                    Name = "Exit to Woods (Secret)";
                    break;
                /// Lighthouse
                case "lighthouse_secret_minefield":
                    Name = "Passage by the Lake (Secret)";
                    break;
                /// The Labyrinth
                case "labyrinth_secret_tagilla_key":
                    Name = "Ariadne's Path (Secret)";
                    break;

            }
            #endregion
            _position = new UnityTransform(transformInternal, false).UpdatePosition();
        }

        private readonly ulong _addr;
        public string Name { get; }
        public EStatus Status { get; private set; } = EStatus.Closed;

        /// <summary>
        /// Update Exfil Information/Status.
        /// </summary>
        public void Update(Enums.EExfiltrationStatus status)
        {
            /// Update Status
            switch (status)
            {
                case Enums.EExfiltrationStatus.NotPresent:
                    Status = EStatus.Closed;
                    break;
                case Enums.EExfiltrationStatus.UncompleteRequirements:
                    Status = EStatus.Pending;
                    break;
                case Enums.EExfiltrationStatus.Countdown:
                    Status = EStatus.Open;
                    break;
                case Enums.EExfiltrationStatus.RegularMode:
                    Status = EStatus.Open;
                    break;
                case Enums.EExfiltrationStatus.Pending:
                    Status = EStatus.Pending;
                    break;
                case Enums.EExfiltrationStatus.AwaitsManualActivation:
                    Status = EStatus.Pending;
                    break;
                case Enums.EExfiltrationStatus.Hidden:
                    Status = EStatus.Closed;
                    break;
            }
            /// Update Entry Points
            if (_isPMC)
            {
                var entriesArrPtr = Memory.ReadPtr(_addr + Offsets.Exfil.EligibleEntryPoints);
                using var entriesArr = MemArray<ulong>.Get(entriesArrPtr);
                foreach (var entryNamePtr in entriesArr)
                {
                    var entryName = Memory.ReadUnityString(entryNamePtr);
                    PmcEntries.Add(entryName);
                }
            }
            else // Scav Exfils
            {
                var eligibleIdsPtr = Memory.ReadPtr(_addr + Offsets.ScavExfil.EligibleIds);
                using var idsArr = MemList<ulong>.Get(eligibleIdsPtr);
                foreach (var idPtr in idsArr)
                {
                    var idName = Memory.ReadUnityString(idPtr);
                    ScavIds.Add(idName);
                }
            }
        }

        #region Interfaces

        private Vector3 _position;
        public ref Vector3 Position => ref _position;
        public Vector2 MouseoverPosition { get; set; }

        public void Draw(SKCanvas canvas, LoneMapParams mapParams, ILocalPlayer localPlayer)
        {
            var heightDiff = Position.Y - localPlayer.Position.Y;
            var paint = GetPaint();
            var point = Position.ToMapPos(mapParams.Map).ToZoomedPos(mapParams);
            MouseoverPosition = new Vector2(point.X, point.Y);
            SKPaints.ShapeOutline.StrokeWidth = 2f;
            if (MainForm.Config.MapFollowTeammate == 1)
            {
                foreach (var mate in Memory.Players)
                {
                    if (mate.GroupID == Memory.LocalPlayer.GroupID && mate.IsPmc && mate.IsFriendlyActive)
                    {
                        heightDiff = Position.Y - mate.Position.Y;
                    }
                }
            }
            if (heightDiff > 1.85f) // exfil is above player
            {
                using var path = point.GetArrow(6.5f);
                canvas.DrawPath(path, SKPaints.ShapeOutline);
                canvas.DrawPath(path, paint);
            }
            else if (heightDiff < -1.85f) // exfil is below player
            {
                using var path = point.GetArrow(6.5f, false);
                canvas.DrawPath(path, SKPaints.ShapeOutline);
                canvas.DrawPath(path, paint);
            }
            else // exfil is level with player
            {
                float size = 4.75f * MainForm.UIScale;
                canvas.DrawCircle(point, size, SKPaints.ShapeOutline);
                canvas.DrawCircle(point, size, paint);
            }
        }

        private SKPaint GetPaint()
        {
            if (Name.ToLower().Contains("secret"))
            {
                switch(Status)
                {
                    case EStatus.Open:
                        return SKPaints.PaintExfilOpen;
                    case EStatus.Pending:
                        return SKPaints.PaintExfilPending;
                    case EStatus.Closed:
                        return SKPaints.PaintExfilClosed;
                    default:
                        return SKPaints.PaintExfilClosed;
                }
            }
            var localPlayer = Memory.LocalPlayer;
            if (localPlayer is not null && localPlayer.IsPmc &&
                !PmcEntries.Contains(localPlayer.EntryPoint ?? "NULL"))
                return SKPaints.PaintExfilInactive;
            else if (localPlayer is not null && localPlayer.IsScav &&
                !ScavIds.Contains(localPlayer.ProfileId))
                return SKPaints.PaintExfilInactive;
            switch (Status)
            {
                case EStatus.Open:
                    return SKPaints.PaintExfilOpen;
                case EStatus.Pending:
                    return SKPaints.PaintExfilPending;
                case EStatus.Closed:
                    return SKPaints.PaintExfilClosed;
                default:
                    return SKPaints.PaintExfilClosed;
            }
        }

        public void DrawMouseover(SKCanvas canvas, LoneMapParams mapParams, LocalPlayer localPlayer)
        {
            List<string> lines = new();
            var exfilName = Name;
            exfilName ??= "unknown";
            lines.Add($"{exfilName} ({Status.GetDescription()})");
            Position.ToMapPos(mapParams.Map).ToZoomedPos(mapParams).DrawMouseoverText(canvas, lines);
        }

        public void DrawESP(SKCanvas canvas, LocalPlayer localPlayer)
        {
            if (!CameraManagerBase.WorldToScreen(ref _position, out var scrPos))
                return;
            if (Name.ToLower().Contains("secret") && (Status is EStatus.Pending || Status is EStatus.Open)) // when we have found the secret letter, draw extract
            {
                #region Secret Name Filtering
                string newName = null;
                switch (Name.ToLower())
                {
                    /// Customs
                    case "customs_secret_voron_boat":
                        newName = "Smugglers' Boat (Secret)";
                        break;
                    case "customs_secret_voron_bunker":
                        newName = "Smugglers' Bunker (ZB-1012) (Secret)";
                        break;
                    case "customs_sniper_exit":
                        newName = "Railroad Passage (Flare)";
                        break;
                    case "custom_scav_pmc":
                        newName = "Boiler Room Basement (Co-op)";
                        break;
                    /// Streets
                    case "streets_secret_onyx":
                        newName = "Smugglers' Basement (Secret)";
                        break;
                    /// Shoreline
                    case "shoreline_secret_heartbeat":
                        newName = "Mountain Bunker (Secret)";
                        break;
                    /// Factory
                    case "factory_secret_ark":
                        newName = "Smugglers' Passage (Secret)";
                        break;
                    /// Ground Zero
                    case "groundzero_secret_adaptation":
                        newName = "Tartowers Sales Office (Secret)";
                        break;
                    /// Woods
                    case "wood_sniper_exit":
                        newName = "Power Line Passage (Flare)";
                        break;
                    case "woods_secret_minefield":
                        newName = "Railway Bridge to Tarkov (Secret)";
                        break;
                    /// Reserve
                    case "reserve_secret_minefield":
                        newName = "Exit to Woods (Secret)";
                        break;
                    /// Lighthouse
                    case "lighthouse_secret_minefield":
                        newName = "Passage by the Lake (Secret)";
                        break;
                    /// The Labyrinth
                    case "labyrinth_secret_tagilla_key":
                        newName = "Ariadne's Path (Secret)";
                        break;

                }
                #endregion
                var label1 = $"{(newName is null ? Name : newName)} ({Status.GetDescription()})";
                scrPos.DrawESPText(canvas, this, localPlayer, ESP.Config.ShowDistances, SKPaints.TextExfilESP, label1);
                return;
            }
            if (localPlayer.IsPmc &&
                !PmcEntries.Contains(localPlayer.EntryPoint))
                return;
            else if (localPlayer.IsScav &&
                !ScavIds.Contains(localPlayer.ProfileId))
                return;
            if (!localPlayer.IsPmc && Status is not EStatus.Open)
                return; // Only draw available SCAV Exfils
            if (Status is EStatus.Closed) // Only draw open/pending exfils
                return;
            var label = $"{Name} ({Status.GetDescription()})";
            scrPos.DrawESPText(canvas, this, localPlayer, ESP.Config.ShowDistances, SKPaints.TextExfilESP, label);
        }

        #endregion

        public enum EStatus
        {
            [Description(nameof(Open))] Open,
            [Description(nameof(Pending))] Pending,
            [Description(nameof(Closed))] Closed
        }
    }
}
