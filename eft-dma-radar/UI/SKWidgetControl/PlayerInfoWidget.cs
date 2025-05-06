using eft_dma_radar.Tarkov.EFTPlayer;
using eft_dma_radar.UI.Misc;
using eft_dma_radar.UI.Radar;
using eft_dma_shared.Common.Misc;
using eft_dma_shared.Common.Misc.Commercial;
using eft_dma_shared.Common.Misc.Data;

namespace eft_dma_radar.UI.SKWidgetControl
{
    public sealed class PlayerInfoWidget : SKWidget
    {
        /// <summary>
        /// Constructs a Player Info Overlay.
        /// </summary>
        /// 
        public bool notFound_ = false;
        public PlayerInfoWidget(SKGLControl parent, SKRect location, bool minimized, float scale)
            : base(parent, "Player Info", new SKPoint(location.Left, location.Top),
                new SKSize(location.Width, location.Height), scale, false)
        {
            Minimized = minimized;
            SetScaleFactor(scale);
        }

        internal static SKPaint TextPlayersOverlay { get; } = new()
        {
            SubpixelText = true,
            Color = SKColors.White,
            IsStroke = false,
            TextSize = 12,
            TextEncoding = SKTextEncoding.Utf8,
            IsAntialias = true,
            Typeface = SKTypeface.FromFamilyName("Consolas"), // Do NOT change this font
            FilterQuality = SKFilterQuality.High
        };

        internal static SKPaint TextBorderPaint { get; } = new()
        {
            Color = SKColors.DimGray,
            StrokeWidth = 2f,
            IsStroke = true
        };

        public void Draw(SKCanvas canvas, Player localPlayer, IEnumerable<Player> players)
        {
            if (Minimized)
            {
                Draw(canvas);
                return;
            }

            float pad = 5f * ScaleFactor;
            float textOffsetX = 4f * ScaleFactor;
            float textOffsetY = 6f * ScaleFactor;

            var localPlayerPos = localPlayer.Position;
            var hostiles = players
                .Where(x => x.IsHostileActive)
                .ToList();

            var pmcCount = hostiles.Count(x => x.IsPmc);
            var hostileCount = hostiles.Count;
            var pscavCount = hostiles.Count(x => x.Type is Player.PlayerType.PScav);
            var aiCount = hostiles.Count(x => x.IsAI);
            var bossCount = hostiles.Count(x => x.Type is Player.PlayerType.AIBoss);

            var filteredPlayers = players
                .Where(x => x.IsHumanHostileActive)
                .OrderBy(x => Vector3.Distance(localPlayerPos, x.Position))
                .ToList();

            var headers = new[]
            {
        "Fac/Prestige/Lvl/Name", "Last Updated", "Acct", "K/D", "Hours", "Raids", "S/R%", "Grp", "Value", "In Hands", "Dist"
    };

            var baseColumnWidths = new float[] { 230, 160, 40, 50, 50, 50, 50, 40, 70, 180, 40 };
            var columnWidths = baseColumnWidths.Select(w => w * ScaleFactor).ToArray();

            var rowHeight = (TextPlayersOverlay.FontSpacing + 4f) * ScaleFactor;
            var origin = new SKPoint(ClientRectangle.Left + pad, ClientRectangle.Top + pad);

            float totalWidth = columnWidths.Sum();
            float totalHeight = (filteredPlayers.Count + 1) * rowHeight;

            Size = new SKSize(totalWidth + pad * 2, totalHeight + pad * 2);
            Draw(canvas);

            canvas.DrawText(
                $"Hostile Count: {hostileCount} | PMC: {pmcCount} | PScav: {pscavCount} | AI: {aiCount} | Boss: {bossCount}",
                origin.X + 560f * ScaleFactor,
                (origin.Y - rowHeight / 2f) + 2f * ScaleFactor,
                TextPlayersOverlay
            );

            float x = origin.X;
            float y = origin.Y;

            for (int i = 0; i < headers.Length; i++)
            {
                canvas.DrawText(headers[i], x + textOffsetX, y + rowHeight - textOffsetY, TextPlayersOverlay);
                x += columnWidths[i];
            }

            canvas.DrawLine(origin.X, y + rowHeight, origin.X + totalWidth, y + rowHeight, TextBorderPaint);
            y += rowHeight;

            foreach (var player in filteredPlayers)
            {
                bool profileFound = Program.Config.Cache.ProfileAPI.Profiles.ContainsKey(player.AccountID);

                var name = MainForm.Config.HideNames && player.IsHuman
                    ? "<Hidden>"
                    : (!profileFound ? "PMC" : player.Name);

                var faction = player.PlayerSide.GetDescription()[0];
                var hands = player.Hands?.CurrentItem ?? "--";

                string edition = "--", level = "0", prestige = "0", kd = "--", raidCount = "--", survivePercent = "--", hours = "--", updated = "--";

                try
                {
                    if (profileFound && player is ObservedPlayer observed)
                    {
                        updated = observed.Profile.Updated ?? "--";
                        edition = observed.Profile?.Acct ?? "--";
                        level = observed.Profile?.Level?.ToString() ?? "0";
                        prestige = observed.Profile?.Prestige.ToString() ?? "0";
                        kd = observed.Profile?.Overall_KD?.ToString("n2") ?? "--";
                        raidCount = observed.Profile?.RaidCount?.ToString() ?? "--";
                        survivePercent = observed.Profile?.SurvivedRate?.ToString("n1") ?? "--";
                        hours = observed.Profile?.Hours?.ToString() ?? "--";
                    }
                }
                catch
                {
                    LoneLogging.WriteLine($"ERROR getting {player.AccountID} profile data");
                }

                var grp = player.GroupID != -1 ? player.GroupID.ToString() : "--";
                var focused = player.IsFocused ? "*" : "";
                var value = TarkovMarketItem.FormatPrice(player.Gear?.Value ?? 0);
                var distance = ((int)Math.Round(Vector3.Distance(localPlayerPos, player.Position))).ToString();

                string nameDisplay = $"{focused}{faction}:     P{(Convert.ToInt32(prestige) < 10 ? (prestige + " ") : prestige)}|  L{(Convert.ToInt32(level) < 10 ? (level + " ") : level)}: {name}";

                var values = new[]
                {
            nameDisplay, updated, edition, kd, hours, raidCount, survivePercent, grp, value, hands.ToString(), distance
        };

                x = origin.X;
                for (int i = 0; i < values.Length; i++)
                {
                    canvas.DrawText(values[i], x + textOffsetX, y + rowHeight - textOffsetY, TextPlayersOverlay);
                    canvas.DrawLine(x, y - rowHeight, x, y + rowHeight, TextBorderPaint);
                    x += columnWidths[i];
                }

                canvas.DrawLine(x, y - rowHeight, x, y + rowHeight, TextBorderPaint);
                canvas.DrawLine(origin.X, y + rowHeight, origin.X + totalWidth, y + rowHeight, TextBorderPaint);
                y += rowHeight;
            }
        }

        public override void SetScaleFactor(float newScale)
        {
            base.SetScaleFactor(newScale);
            TextPlayersOverlay.TextSize = 12 * newScale;
        }
    }
}