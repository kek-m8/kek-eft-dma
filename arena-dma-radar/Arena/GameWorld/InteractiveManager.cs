using eft_dma_shared.Common.Misc.Commercial;
using eft_dma_shared.Common.Unity.Collections;
using eft_dma_shared.Common.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LonesArenaRadar.Arena.GameWorld.Interactive;

namespace LonesArenaRadar.Arena.GameWorld
{
    public sealed class InteractiveManager
    {
        private readonly ulong _localGameWorld;
        private readonly HashSet<ArenaPresetRefillContainer> _refillContainers;
        private bool _kill = false;

        public IReadOnlyCollection<ArenaPresetRefillContainer> RefillContainers => _refillContainers;

        public InteractiveManager(ulong localGameWorld)
        {
            _localGameWorld = localGameWorld;
            _refillContainers = new();
            Init();
        }

        public void Init()
        {
            try
            {
                var interactableArrayPtr = Memory.ReadPtrChain(_localGameWorld, new uint[] { 0x268, 0x30 }, false);
                using var array = MemArray<ulong>.Get(interactableArrayPtr, false);
                var set = array.Where(x => x != 0x0).ToHashSet();
                array.Dispose();

                foreach (var item in set)
                {
                    var itemName = ObjectClass.ReadName(item);
                    if (itemName == "ArenaPresetRefillContainer")
                    {
                        _refillContainers.Add(new ArenaPresetRefillContainer(item));
                        _kill = false;
                    }
                }
            }
            catch { _kill = true; return; }
        }

        public void Refresh()
        {
            if (_kill)
                return;
            if (_refillContainers.Count == 0)
            {
                Init();
            }
            foreach (var refill in _refillContainers)
            {
                refill.Refresh();
            }
        }
    }
}
