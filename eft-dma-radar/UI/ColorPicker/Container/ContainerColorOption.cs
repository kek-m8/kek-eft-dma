using eft_dma_radar.UI.Misc;
using eft_dma_radar.UI.ESP;
using eft_dma_radar.UI.Radar;

namespace LonesEFTRadar.UI.ColorPicker.Container
{
    [Obfuscation(Exclude = true, Feature = "renaming")]
    public enum ContainerColorOption
    {
        CacheAndBody,
        Body,
        Medical,
        Tech,
        Grenade,
        Jacket,
        Bag,
        PC,
        Ammo
    }
    internal static class ContainerColorOptions
    {
        internal static void LoadColors(Config config)
        {
            config.Containers.Colors ??= new Dictionary<ContainerColorOption, string>();
            foreach (var defaultColor in GetDefaultColors())
                config.Containers.Colors.TryAdd(defaultColor.Key, defaultColor.Value);
            SetColors(config.Containers.Colors);
        }

        internal static Dictionary<ContainerColorOption, string> GetDefaultColors() =>
            new()
            {
                [ContainerColorOption.CacheAndBody] = SKColor.Parse("FFFFCC").ToString(),
                [ContainerColorOption.Body] = SKColor.Parse("FFFFCC").ToString(),
                [ContainerColorOption.Medical] = SKColor.Parse("FFFFCC").ToString(),
                [ContainerColorOption.Tech] = SKColor.Parse("FFFFCC").ToString(),
                [ContainerColorOption.Grenade] = SKColor.Parse("FFFFCC").ToString(),
                [ContainerColorOption.Jacket] = SKColor.Parse("FFFFCC").ToString(),
                [ContainerColorOption.Bag] = SKColor.Parse("FFFFCC").ToString(),
                [ContainerColorOption.PC] = SKColor.Parse("FFFFCC").ToString(),
                [ContainerColorOption.Ammo] = SKColor.Parse("FFFFCC").ToString()
            };
        internal static void SetColors(IReadOnlyDictionary<ContainerColorOption, string> colors)
        {

        }
    }
}
