using arena_dma_radar.Arena.Loot;
using arena_dma_radar.Tarkov.Loot;
using arena_dma_radar.UI.Radar;

namespace arena_dma_radar.UI.LootFilters
{
    /// <summary>
    /// Enumerable Loot Filter Class.
    /// </summary>
    internal static class LootFilter
    {
        public static string SearchString;
        public static bool ShowMeds;
        public static bool ShowFood;
        public static bool ShowBackpacks;
        public static bool ShowWishlist;

        // private static bool ShowQuestItems => MainForm.Config.QuestHelper.Enabled;

        /// <summary>
        /// Creates a loot filter based on current Loot Filter settings.
        /// </summary>
        /// <returns>Loot Filter Predicate.</returns>
        public static Predicate<LootItem> Create()
        {
            Predicate<LootItem> p = (x) => // Default Predicate
            {
                return (LootFilter.ShowBackpacks && x.IsBackpack);
            };
            return (item) =>
            {
                if (p(item))
                {
                    if (item is LootContainer container)
                    {
                        container.SetFilter(p);
                    }
                    return true;
                }
                return false;
            };
        }
    }
}