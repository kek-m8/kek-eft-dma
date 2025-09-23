using eft_dma_radar.Tarkov.EFTPlayer;
using eft_dma_radar.Tarkov.GameWorld;
using eft_dma_radar.UI.ESP;
using eft_dma_radar.UI.Misc;
using eft_dma_radar.UI.Radar;
using eft_dma_shared.Common.Misc;
using eft_dma_shared.Common.Misc.Data;
using eft_dma_shared.Common.Players;
using eft_dma_shared.Common.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static eft_dma_shared.Common.Misc.Data.EftDataManager;

namespace LonesEFTRadar.Tarkov.GameWorld.Interactive
{
    public sealed class Trunk : IESPEntity, IWorldEntity
    {
        public ref Vector3 Position => throw new NotImplementedException();
        public void DrawESP(SKCanvas canvas, LocalPlayer localPlayer)
        {
            throw new NotImplementedException();
        }
    }
}
