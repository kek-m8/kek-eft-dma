using SkiaSharp;

namespace eft_dma_shared.Common.Misc
{
    /// <summary>
    /// 中文文本渲染辅助类
    /// </summary>
    public static class ChineseTextHelper
    {
        /// <summary>
        /// 创建一个支持中文的SKPaint对象
        /// </summary>
        /// <param name="originalPaint">原始的SKPaint对象</param>
        /// <returns>支持中文的SKPaint对象</returns>
        public static SKPaint CreateChinesePaint(SKPaint originalPaint)
        {
            // 克隆原始Paint对象
            var chinesePaint = new SKPaint
            {
                SubpixelText = originalPaint.SubpixelText,
                Color = originalPaint.Color,
                IsStroke = originalPaint.IsStroke,
                TextSize = originalPaint.TextSize,
                TextAlign = originalPaint.TextAlign,
                TextEncoding = SKTextEncoding.Utf8,
                IsAntialias = originalPaint.IsAntialias,
                FilterQuality = originalPaint.FilterQuality,
                StrokeWidth = originalPaint.StrokeWidth,
                Style = originalPaint.Style
            };
            
            // 使用中文字体
            chinesePaint.Typeface = CustomFonts.GetChineseTypeface();
            
            return chinesePaint;
        }
        
        /// <summary>
        /// 使用支持中文的字体绘制文本
        /// </summary>
        /// <param name="canvas">画布</param>
        /// <param name="text">要绘制的文本</param>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="paint">原始的Paint对象</param>
        public static void DrawChineseText(SKCanvas canvas, string text, float x, float y, SKPaint paint)
        {
            // 检查文本是否包含中文字符
            if (ContainsChineseCharacters(text))
            {
                // 使用中文字体绘制
                using var chinesePaint = CreateChinesePaint(paint);
                canvas.DrawText(text, x, y, chinesePaint);
            }
            else
            {
                // 使用原始字体绘制
                canvas.DrawText(text, x, y, paint);
            }
        }
        
        /// <summary>
        /// 检查文本是否包含中文字符
        /// </summary>
        /// <param name="text">要检查的文本</param>
        /// <returns>是否包含中文字符</returns>
        public static bool ContainsChineseCharacters(string text)
        {
            if (string.IsNullOrEmpty(text))
                return false;
                
            // 检查是否包含中文字符（基本汉字范围：0x4E00-0x9FFF）
            foreach (char c in text)
            {
                if (c >= 0x4E00 && c <= 0x9FFF)
                    return true;
            }
            
            return false;
        }
        
        /// <summary>
        /// 测量中文文本的宽度
        /// </summary>
        /// <param name="text">要测量的文本</param>
        /// <param name="paint">原始的Paint对象</param>
        /// <returns>文本宽度</returns>
        public static float MeasureChineseText(string text, SKPaint paint)
        {
            if (ContainsChineseCharacters(text))
            {
                // 使用中文字体测量
                using var chinesePaint = CreateChinesePaint(paint);
                return chinesePaint.MeasureText(text);
            }
            else
            {
                // 使用原始字体测量
                return paint.MeasureText(text);
            }
        }
    }
} 