using eft_dma_radar.Tarkov.Features;
using eft_dma_radar.Tarkov.GameWorld;
using eft_dma_shared.Common.DMA.ScatterAPI;
using eft_dma_shared.Common.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonesEFTRadar.Tarkov.Features.MemoryWrites
{
    public sealed class NightVision : MemWriteFeature<NightVision>
    {
        public override bool Enabled
        {
            get => MemWrites.Config.Nightvision;
            set => MemWrites.Config.Nightvision = value;
        }

        protected override TimeSpan Delay => TimeSpan.FromSeconds(1000);

        public override void TryApply(ScatterWriteHandle writes)
        {
            try
            {
                if (!Memory.InRaid)
                    return;
                ulong nightVision = CameraManager.GetNightVision();
                if (nightVision != 0)
                {
                    writes.AddValueEntry(nightVision + Offsets.NightVision._on, Enabled);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
