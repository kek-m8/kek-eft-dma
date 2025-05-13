using SkiaSharp;
using System.Reflection;

namespace eft_dma_shared.Common.Misc
{
    public static class CustomFonts
    {
        /// <summary>
        /// Neo Sans Std Regular
        /// </summary>
        public static SKTypeface SKFontFamilyRegular { get; }
        /// <summary>
        /// Neo Sans Std Bold
        /// </summary>
        public static SKTypeface SKFontFamilyBold { get; }
        /// <summary>
        /// Neo Sans Std Italic
        /// </summary>
        public static SKTypeface SKFontFamilyItalic { get; }
        /// <summary>
        /// Neo Sans Std Medium
        /// </summary>
        public static SKTypeface SKFontFamilyMedium { get; }

        public static SKTypeface SKFontBenderRegular { get; }
        public static SKTypeface SKFontBenderLight { get; }
        public static SKTypeface SKFontBenderBold { get; }
        public static SKTypeface SKFontBenderItalic { get; }
        public static SKTypeface SKFontBenderBlack { get; }

        static CustomFonts()
        {
            try
            {
                byte[] fontFamilyRegular, fontFamilyBold, fontFamilyItalic, fontFamilyMedium, fontBenderRegular, fontBenderLight, fontBenderBold, fontBenderItalic, fontBenderBlack;
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("eft-dma-shared.NeoSansStdRegular.otf"))
                {
                    fontFamilyRegular = new byte[stream!.Length];
                    stream.ReadExactly(fontFamilyRegular);
                }
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("eft-dma-shared.NeoSansStdBold.otf"))
                {
                    fontFamilyBold = new byte[stream!.Length];
                    stream.ReadExactly(fontFamilyBold);
                }
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("eft-dma-shared.NeoSansStdItalic.otf"))
                {
                    fontFamilyItalic = new byte[stream!.Length];
                    stream.ReadExactly(fontFamilyItalic);
                }
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("eft-dma-shared.NeoSansStdMedium.otf"))
                {
                    fontFamilyMedium = new byte[stream!.Length];
                    stream.ReadExactly(fontFamilyMedium);
                }
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("eft-dma-shared.bender.regular.otf"))
                {
                    fontBenderRegular = new byte[stream!.Length];
                    stream.ReadExactly(fontBenderRegular);
                }
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("eft-dma-shared.bender.light.otf"))
                {
                    fontBenderLight = new byte[stream!.Length];
                    stream.ReadExactly(fontBenderLight);
                }
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("eft-dma-shared.bender.bold.otf"))
                {
                    fontBenderBold = new byte[stream!.Length];
                    stream.ReadExactly(fontBenderBold);
                }
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("eft-dma-shared.bender.italic.otf"))
                {
                    fontBenderItalic = new byte[stream!.Length];
                    stream.ReadExactly(fontBenderItalic);
                }
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("eft-dma-shared.bender.black.otf"))
                {
                    fontBenderBlack = new byte[stream!.Length];
                    stream.ReadExactly(fontBenderBlack);
                }
                SKFontFamilyRegular = SKTypeface.FromStream(new MemoryStream(fontFamilyRegular, false));
                SKFontFamilyBold = SKTypeface.FromStream(new MemoryStream(fontFamilyBold, false));
                SKFontFamilyItalic = SKTypeface.FromStream(new MemoryStream(fontFamilyItalic, false));
                SKFontFamilyMedium = SKTypeface.FromStream(new MemoryStream(fontFamilyMedium, false));
                SKFontBenderRegular = SKTypeface.FromStream(new MemoryStream(fontBenderRegular, false));
                SKFontBenderLight = SKTypeface.FromStream(new MemoryStream(fontBenderLight, false));
                SKFontBenderBold = SKTypeface.FromStream(new MemoryStream(fontBenderBold, false));
                SKFontBenderItalic = SKTypeface.FromStream(new MemoryStream(fontBenderItalic, false));
                SKFontBenderBlack = SKTypeface.FromStream(new MemoryStream(fontBenderBlack, false));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("ERROR Loading Custom Fonts!", ex);
            }
        }
    }
}
