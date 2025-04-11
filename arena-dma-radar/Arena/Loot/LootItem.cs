using arena_dma_radar.Arena.ArenaPlayer;
using arena_dma_radar.UI.ESP;
using arena_dma_radar.UI.Radar;
using arena_dma_radar.UI.Misc;
using eft_dma_shared.Common.Misc;
using eft_dma_shared.Common.Unity;
using eft_dma_shared.Common.Maps;
using eft_dma_shared.Common.Players;
using eft_dma_shared.Common.Misc.Data;
using eft_dma_shared.Common.DMA.ScatterAPI;
using eft_dma_shared.Common.Unity.LowLevel;
using eft_dma_shared.Common.Misc.Commercial;
using arena_dma_radar.UI.LootFilters;
using arena_dma_radar.Arena.Loot;
using arena_dma_radar.Tarkov.Loot;

namespace arena_dma_radar.Arena.Loot
{
    public class LootItem : IMouseoverEntity, IMapEntity, IWorldEntity, IESPEntity
    {
        //private static Config Config { get; } = Program.Config;
        private readonly TarkovMarketItem _item;
        public LootItem(TarkovMarketItem item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));
            _item = item;
        }

        public LootItem(string id, string name)
        {
            ArgumentNullException.ThrowIfNull(id, nameof(id));
            ArgumentNullException.ThrowIfNull(name, nameof(name));
            _item = new TarkovMarketItem
            {
                Name = name,
                ShortName = name,
                FleaPrice = -1,
                TraderPrice = -1,
                BsgId = id
            };
        }

        /// <summary>
        /// Item's BSG ID.
        /// </summary>
        public virtual string ID => _item.BsgId;

        /// <summary>
        /// Item's Long Name.
        /// </summary>
        public virtual string Name => _item.Name;

        /// <summary>
        /// Item's Short Name.
        /// </summary>
        public string ShortName => _item.ShortName;

        /// <summary>
        /// Number of grid spaces this item takes up.
        /// </summary>
        public int GridCount => _item.Slots == 0 ? 1 : _item.Slots;

        /// <summary>
        /// Custom filter for this item (if set).
        /// </summary>
        public LootFilterEntry CustomFilter => _item.CustomFilter;

        public ChamsManager.ChamsMode ChamsMode { get; private set; }

        /// <summary>
        /// True if the item is important via the UI.
        /// </summary>
        public bool Important => CustomFilter?.Important ?? false;

        /// <summary>
        /// True if the item is blacklisted via the UI.
        /// </summary>
        public bool Blacklisted => CustomFilter?.Blacklisted ?? false;
        public bool IsWeapon => _item.IsWeapon;
        public bool IsCurrency => _item.IsCurrency;

        public bool IsBackpack
        {
            get
            {
                if (this is LootContainer container)
                {
                    return container.Loot.Any(x => x.IsBackpack);
                }
                return _item.IsBackpack;
            }
        }

        public bool ContainsSearchPredicate(Predicate<LootItem> predicate)
        {
            if (this is LootContainer container)
            {
                return container.Loot.Any(x => x.ContainsSearchPredicate(predicate));
            }
            return predicate(this);
        }

        /// <summary>
        /// Checks if an item exceeds regular loot price threshold.
        /// </summary>
        public virtual void DrawESP(SKCanvas canvas, LocalPlayer localPlayer)
        {
            var dist = Vector3.Distance(localPlayer.Position, Position);
            if (!CameraManagerBase.WorldToScreen(ref _position, out var scrPos))
                return;
            var boxHalf = 3.5f * ESP.Config.FontScale;
            var label = GetUILabel(false);
            var paints = GetESPPaints();
            var boxPt = new SKRect(scrPos.X - boxHalf, scrPos.Y + boxHalf,
                scrPos.X + boxHalf, scrPos.Y - boxHalf);
            var textPt = new SKPoint(scrPos.X,
                scrPos.Y + 16f * ESP.Config.FontScale);
            canvas.DrawRect(boxPt, paints.Item1);
            textPt.DrawESPText(canvas, this, localPlayer, true, paints.Item2, label);
        }

        private Vector3 _position;
        public ref Vector3 Position => ref _position;
        public Vector2 MouseoverPosition { get; set; }

        public virtual void Draw(SKCanvas canvas, LoneMapParams mapParams, ILocalPlayer localPlayer)
        {
            var label = GetUILabel(false);
            var paints = GetESPPaints();
            var heightDiff = Position.Y - localPlayer.Position.Y;
            var point = Position.ToMapPos(mapParams.Map).ToZoomedPos(mapParams);
            MouseoverPosition = new Vector2(point.X, point.Y);
            SKPaints.ShapeOutline.StrokeWidth = 2f;
            point.Offset(7 * MainForm.UIScale, 3 * MainForm.UIScale);
            canvas.DrawText(label, point, SKPaints.TextOutline); // Draw outline
            canvas.DrawText(label, point, paints.Item2);
        }

        public virtual void DrawMouseover(SKCanvas canvas, LoneMapParams mapParams, LocalPlayer localPlayer)
        {

        }

        /// <summary>
        /// Gets a UI Friendly Label.
        /// </summary>
        /// <param name="showPrice">Show price in label.</param>
        /// <param name="showImportant">Show Important !! in label.</param>
        /// <param name="showQuest">Show Quest tag in label.</param>
        /// <returns>Item Label string cleaned up for UI usage.</returns>
        public string GetUILabel(bool showQuest = false)
        {
            var label = "";
            if (this is LootContainer container)
            {
                var backpack = container.Loot.Any(x => x.IsBackpack);
                var loot = container.FilteredLoot;
                label = container.Name;
            }
            if (string.IsNullOrEmpty(label))
                label = "Item";
            return label;
        }

        public ValueTuple<SKPaint, SKPaint> GetESPPaints()
        {
            return new(SKPaints.PaintBackpackESP, SKPaints.TextBackpackESP);
            
        }

        public ValueTuple<SKPaint, SKPaint> GetPaints()
        {
            return new(SKPaints.PaintBackpacks, SKPaints.TextBackpacks);

        }

    }
    public static class LootItemExtensions
    {
        /// <summary>
        /// Order loot (important first, then by price).
        /// </summary>
        /// <param name="loot"></param>
        /// <returns>Ordered loot.</returns>
        public static IEnumerable<LootItem> OrderLoot(this IEnumerable<LootItem> loot)
        {
            return loot
                .OrderByDescending(x => x.IsBackpack);
        }
    }
}