using eft_dma_radar;
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
    public sealed class Switches : IESPEntity, IWorldEntity
    {

        private static readonly uint[] _transformInternalChain =
        [
            ObjectClass.MonoBehaviourOffset,
            MonoBehaviour.GameObjectOffset,
            GameObject.ComponentsOffset,
            0x8
        ];
        public ulong Base { get; set; }

        public string Id { get; set; }
        public static Config Config { get; } = Program.Config;
        public Door Door { get; set; }

        private Vector3 _position;
        public ref Vector3 Position => ref _position;

        public Switches(ulong ptr)
        {
            try
            {
                Base = ptr;
                var transformInternal = Memory.ReadPtrChain(Base, _transformInternalChain, false);
                var transform = new UnityTransform(transformInternal);

                _position = transform.UpdatePosition();
                Id = Memory.ReadUnityString(Memory.ReadPtr(Base + Offsets.Interactable.Id, false));

                var doorPtr = Memory.ReadPtr(Base + 0x178, false);
                if (doorPtr != 0x0)
                    Door = new Door(doorPtr, "Door");
            }
            catch(Exception e) { MessageBox.Show($"ERROR: Couldn't find switch\n\n{e}"); }

        }

        public void DrawESP(SKCanvas canvas, LocalPlayer localPlayer)
        {
            var switchPos = _position;
            
            if(!CameraManager.WorldToScreen(ref switchPos, out var scrPos))
                return;

            scrPos.DrawESPText(canvas, this, localPlayer, false, SKPaints.TextBackpackESP, $"Switch\n{Utils.GetDistPretty(Position, localPlayer.Position)}" );
            if(Door != null)
            {
                scrPos.Y += SKPaints.TextBackpackESP.TextSize;
                scrPos.DrawESPText(canvas, this, localPlayer, false, SKPaints.TextCorpseESP, $"Door: {Door.KeyName}");
            }
                
        }
    }
}
