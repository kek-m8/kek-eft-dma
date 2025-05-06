using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eft_dma_radar.Tarkov.EFTPlayer;
using eft_dma_radar.Tarkov.Loot;
using eft_dma_radar.UI.Radar;
using eft_dma_shared.Common.Misc;
using eft_dma_shared.Common.Misc.Data;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Windows.Forms;

namespace eft_dma_radar.UI.SKWidgetControl
{
    public enum LootSortColumn
    {
        Name,
        Price
    }

    public enum SortOrder
    {
        Ascending,
        Descending
    }

    public sealed class LootInfoWidget : SKWidget
    {
        private const float FixedHeight = 400;
        private const float FixedWidth = 310;
        private int scrollOffset = 0;
        private const int ScrollStep = 3;
        private float mouseWheelDelta = 0;
        private bool isMouseOver = false;
        private bool isDraggingScrollbar = false;
        private float scrollbarY = 0;
        private SKPoint lastMousePosition;


        private static LootSortColumn currentSortColumn = LootSortColumn.Price;
        private static SortOrder currentSortOrder = SortOrder.Descending;


        public LootInfoWidget(SKGLControl parent, SKRect location, bool minimized, float scale)
            : base(parent, "Loot Info", new SKPoint(location.Left, location.Top),
                new SKSize(FixedWidth, FixedHeight), scale, true)
        {
            Minimized = minimized;
            SetScaleFactor(scale);

            parent.MouseWheel += OnMouseWheel;
            parent.MouseMove += OnMouseMove;
            parent.MouseDown += OnMouseDown;
            parent.MouseUp += OnMouseUp;
            parent.KeyDown += OnKeyDown; // Added KeyDown event for sorting
        }

        public bool IsMouseOver()
        {
            return isMouseOver;
        }

        private static IEnumerable<(LootItem, int)> Loot =>
            (Memory.Loot?.FilteredLoot
                ?.Where(item => item is not LootCorpse && item is not QuestItem)
                .GroupBy(item => item.Position)
                .Select(group => (group.First(), group.Count())) ??
                Enumerable.Empty<(LootItem, int)>())
            .OrderBy(entry => currentSortColumn == LootSortColumn.Name ? (currentSortOrder == SortOrder.Ascending ? entry.Item1.ShortName : "") : null)
            .ThenByDescending(entry => currentSortColumn == LootSortColumn.Name ? (currentSortOrder == SortOrder.Descending ? entry.Item1.ShortName : "") : null)
            .ThenBy(entry => currentSortColumn == LootSortColumn.Price ? (currentSortOrder == SortOrder.Ascending ? entry.Item1.Price : (int?)null) : null)
            .ThenByDescending(entry => currentSortColumn == LootSortColumn.Price ? (currentSortOrder == SortOrder.Descending ? entry.Item1.Price : (int?)null) : null);

        private LootItem selectedLoot;

        public void SelectLoot(LootItem item)
        {
            selectedLoot = item;
        }

        public LootItem GetSelectedLoot()
        {
            return selectedLoot;
        }

        internal static SKPaint TextPlayersOverlay { get; } = new()
        {
            SubpixelText = true,
            Color = SKColors.White,
            IsStroke = false,
            TextSize = 18,
            TextEncoding = SKTextEncoding.Utf8,
            IsAntialias = true,
            Typeface = CustomFonts.GetChineseTypeface(),
            FilterQuality = SKFilterQuality.High
        };

        public void Draw(SKCanvas canvas, Player localPlayer, SKPoint mousePosition, bool mouseClicked)
        {
            if (Minimized)
            {
                Draw(canvas);
                return;
            }

            var lootItems = Loot.ToList();
            var lootCount = lootItems.Count;
            var sb = new StringBuilder();

            string nameSortIndicator = currentSortColumn == LootSortColumn.Name ? (currentSortOrder == SortOrder.Ascending ? "↑" : "↓") : "";
            string priceSortIndicator = currentSortColumn == LootSortColumn.Price ? (currentSortOrder == SortOrder.Ascending ? "↑" : "↓") : "";

            sb.AppendFormat("{0}{1,-17} {2,-8} {3}{4,-8} {5,-8}",
                currentSortColumn == LootSortColumn.Name ? (currentSortOrder == SortOrder.Ascending ? "↑" : "↓") : "", "Name",
                "Qty",
                currentSortColumn == LootSortColumn.Price ? (currentSortOrder == SortOrder.Ascending ? "↑" : "↓") : "", "Price",
                "Dist").AppendLine();


            float textHeight = TextPlayersOverlay.TextSize * 1.2f;
            int maxVisibleItems = (int)(FixedHeight / textHeight) - 2;

            if (isMouseOver && mouseWheelDelta != 0)
            {
                int direction = mouseWheelDelta > 0 ? -ScrollStep : ScrollStep;
                scrollOffset = Math.Clamp(scrollOffset + direction, 0, Math.Max(0, lootItems.Count - maxVisibleItems));
                mouseWheelDelta = 0;
            }

            var drawPt = new SKPoint(ClientRectangle.Left + 0, ClientRectangle.Top + 27);
            LootItem hoveredItem = null;

            foreach (var (item, count) in lootItems.Skip(scrollOffset).Take(maxVisibleItems))
            {
                string name = item.ShortName;
                string quantity = count.ToString();
                string price = item.Price.ToString("N0");
                float dist = Vector3.Distance(item.Position, localPlayer.Position);

                sb.AppendFormat("{0,-18} {1,-8} {2,-8} {3,-8:F1}", name, quantity, price, dist).AppendLine();

                var itemRect = new SKRect(drawPt.X, drawPt.Y, drawPt.X + 290, drawPt.Y + textHeight);

                if (itemRect.Contains(mousePosition))
                {
                    hoveredItem = item;

                    // Draw yellow background for hovered row
                    using (var highlightPaint = new SKPaint { Color = SKColors.Yellow, IsAntialias = true })
                    {
                        canvas.DrawRect(itemRect, highlightPaint);
                    }

                    if (mouseClicked)
                    {
                        selectedLoot = selectedLoot == item ? null : item;
                    }
                }


                drawPt.Y += textHeight;
            }

            var data = sb.ToString().Split(Environment.NewLine);
            var pad = 2.5f * ScaleFactor;

            Size = new SKSize(ClientRectangle.Width, FixedHeight);
            Location = Location;
            Draw(canvas);

            drawPt = new SKPoint(ClientRectangle.Left + pad, ClientRectangle.Top + pad + 7);
            canvas.DrawText($"Total Loot: {lootCount}", drawPt, TextPlayersOverlay);
            drawPt.Y += textHeight;

            foreach (var line in data)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    canvas.DrawText(line, drawPt, TextPlayersOverlay);
                    drawPt.Y += textHeight;
                }
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.N)
            {
                ToggleSortColumn(LootSortColumn.Name);
            }
            else if (e.KeyCode == Keys.P)
            {
                ToggleSortColumn(LootSortColumn.Price);
            }
        }

        private void ToggleSortColumn(LootSortColumn column)
        {
            if (currentSortColumn == column)
            {
                currentSortOrder = currentSortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                currentSortColumn = column;
                currentSortOrder = SortOrder.Ascending;
            }
        }

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            if (isMouseOver)
                mouseWheelDelta = e.Delta;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            var mousePoint = new SKPoint(e.X, e.Y);
            isMouseOver = ClientRectangle.Contains(mousePoint);
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (isMouseOver)
            {
                lastMousePosition = new SKPoint(e.X, e.Y);
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            isDraggingScrollbar = false;
        }
    }

}