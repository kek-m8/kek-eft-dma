using eft_dma_radar.Tarkov.EFTPlayer;
using eft_dma_radar.Tarkov.Features;
using eft_dma_shared.Common.DMA.ScatterAPI;
using eft_dma_shared.Common.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonesEFTRadar.Tarkov.Features.MemoryWrites
{
    public sealed class InstantPlant : MemWriteFeature<InstantPlant>
    {
        public override bool Enabled
        {
            get => MemWrites.Config.InstaPlant;
            set => MemWrites.Config.InstaPlant = value;
        }

        protected override TimeSpan Delay => TimeSpan.FromSeconds(1000);

        public override void TryApply(ScatterWriteHandle writes)
        {
            try
            {
                if (!Enabled) return;
                LocalPlayer localPlayer = Memory.LocalPlayer;
                ulong currentState = Memory.ReadPtr(localPlayer.MovementContext + Offsets.MovementContext.CurrentState, false);
                writes.AddValueEntry(currentState + Offsets.MovementState.PlantTime, 1f);
            }
            catch
            {

            }
            
        }
    }
}
