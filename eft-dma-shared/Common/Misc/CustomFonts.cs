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

        //public static SKTypeface SKFontFamilyNew { get; }
        /// <summary>
        /// 中文字体 - Microsoft YaHei
        /// </summary>
        public static SKTypeface SKFontFamilyChinese { get; }

        static CustomFonts()
        {
            try
            {
                byte[] fontFamilyRegular, fontFamilyBold, fontFamilyItalic, fontFamilyMedium;//, fontFamilyNew;
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
                /*using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("eft-dma-shared.K2D-Thin.ttf"))
                {
                    fontFamilyNew = new byte[stream!.Length];
                    stream.ReadExactly(fontFamilyNew);
                }*/
                SKFontFamilyRegular = SKTypeface.FromStream(new MemoryStream(fontFamilyRegular, false));
                SKFontFamilyBold = SKTypeface.FromStream(new MemoryStream(fontFamilyBold, false));
                SKFontFamilyItalic = SKTypeface.FromStream(new MemoryStream(fontFamilyItalic, false));
                SKFontFamilyMedium = SKTypeface.FromStream(new MemoryStream(fontFamilyMedium, false));
                //SKFontFamilyNew = SKTypeface.FromStream(new MemoryStream(fontFamilyNew, false));
                // 尝试加载系统中的中文字体
                SKFontFamilyChinese = LoadChineseFontFromSystem();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("ERROR Loading Custom Fonts!", ex);
            }
        }

        /// <summary>
        /// 从系统中加载中文字体
        /// </summary>
        /// <returns>中文字体的SKTypeface对象</returns>
        private static SKTypeface LoadChineseFontFromSystem()
        {
            // 尝试按优先级加载中文字体
            string[] chineseFontNames = new[]
            {
                 "Microsoft YaHei", // 微软雅黑
                 "Microsoft YaHei UI",
                 "SimHei", // 黑体
                 "SimSun", // 宋体
                 "NSimSun", // 新宋体
                 "DengXian", // 等线
                 "KaiTi", // 楷体
                 "FangSong", // 仿宋
                 "Source Han Sans CN", // 思源黑体
                 "Noto Sans CJK SC" // Noto Sans中文
             };

            foreach (var fontName in chineseFontNames)
            {
                var typeface = SKTypeface.FromFamilyName(fontName);
                if (typeface != null && !typeface.FamilyName.Equals("Segoe UI", StringComparison.OrdinalIgnoreCase))
                {
                    return typeface;
                }
            }

            // 如果找不到任何中文字体，返回默认字体
            return SKFontFamilyRegular;
        }

        /// <summary>
        /// 获取支持中文的字体
        /// </summary>
        /// <returns>支持中文的SKTypeface对象</returns>
        public static SKTypeface GetChineseTypeface()
        {
            return SKFontFamilyChinese ?? SKFontFamilyRegular;
        }
    }
}
