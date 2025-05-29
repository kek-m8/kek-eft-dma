using eft_dma_shared.Common.Misc.Commercial;
using eft_dma_shared.Common.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonesArenaRadar.Arena.GameWorld.Interactive
{
    public sealed class ArenaPresetRefillContainer
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
        public string? KeyId { get; set; }
        public Vector3 Position { get; set; }

        public ArenaPresetRefillContainer(ulong ptr)
        {
            try
            {
                //Setting the values that aren't going to change. That way we can just check the state on refresh
                Base = ptr;
                var keyidPtr = Memory.ReadPtr(Base + Offsets.WorldInteractiveObject.KeyId, false);
                var doorIdPtr = Memory.ReadPtr(Base + Offsets.WorldInteractiveObject.Id, false);
                var transformInternal = Memory.ReadPtrChain(Base, _transformInternalChain, false);
                var transform = new UnityTransform(transformInternal);

                Position = transform.UpdatePosition();
            }
            catch { }
        }

        public void Refresh()
        {
            
        }
    }
}
