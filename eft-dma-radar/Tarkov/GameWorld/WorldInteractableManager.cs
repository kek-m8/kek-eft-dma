using eft_dma_shared.Common.Unity;
using eft_dma_shared.Common.Unity.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LonesEFTRadar.Tarkov.GameWorld.Interactive;
using eft_dma_radar.UI.Misc;
using eft_dma_radar;

namespace LonesEFTRadar.Tarkov.GameWorld
{
    public sealed class WorldInteractiveManager
    {
        private readonly ulong _localGameWorld;
        private readonly HashSet<Door> _Doors;
        private readonly HashSet<Switches> _Switches;
        private bool _kill = false;

        public IReadOnlyCollection<Door> Doors => _Doors;
        public IReadOnlyCollection<Switches> Switches => _Switches;

        public WorldInteractiveManager(ulong localGameWorld)
        {
            _localGameWorld = localGameWorld;
            _Doors = new();
            Init();
        }

        public void Init()
        {
            if (!Program.Config.ESP.ShowDoorViewer)
                return;
            try
            {
                var interactableArrayPtr = Memory.ReadPtrChain(_localGameWorld, new uint[] { 0x258, 0x30 }, false);
                using var array = MemArray<ulong>.Get(interactableArrayPtr, false);
                var set = array.Where(x => x != 0x0).ToHashSet();
                array.Dispose();

                foreach (var item in set)
                {
                    var itemName = ObjectClass.ReadName(item);
                    if (itemName.Contains("Door"))
                    {
                        _Doors.Add(new Door(item, itemName));
                    }
                }
            }
            catch {  }
        }

        public void Refresh()
        {
            if (_Doors.Count == 0)
            {
                Init();
            }
            foreach (var door in _Doors)
            {
                door.Refresh();
            }
        }
    }
}
