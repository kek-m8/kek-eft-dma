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
    public sealed class ThermalVision : MemWriteFeature<ThermalVision>
    {
        private bool _set;
        public override bool Enabled { 
            get => MemWrites.Config.ThermalVision;
            set => MemWrites.Config.ThermalVision = value; 
        }

        protected override TimeSpan Delay => TimeSpan.FromSeconds(1000);

        public override void TryApply(ScatterWriteHandle writes)
        {
            try
            {
                if (!Memory.InRaid)
                    return;
                ulong thermalVision = CameraManager.GetNightVision();
                if (thermalVision != 0)
                {
                    writes.AddValueEntry(thermalVision + Offsets.ThermalVision.On, Enabled);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
