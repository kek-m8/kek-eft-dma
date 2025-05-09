using eft_dma_shared.Common.Misc.Data;
using eft_dma_shared.Common.Unity.Collections;
using arena_dma_radar.Arena.Loot;

namespace arena_dma_radar.Arena.ArenaPlayer.Plugins
{
    public sealed class HandsManager
    {
        private readonly ArenaObservedPlayer _parent;

        private volatile string _ammo;
        private volatile string _thermal;
        private volatile TarkovMarketItem _cachedItem;
        private ulong _cached = 0x0;

        /// <summary>
        /// Entity currently in Player's Hands.
        /// </summary>
        public string InHands
        {
            get
            {
                var at = $"{_ammo} {_thermal}".Trim();
                var item = _cachedItem?.ShortName;
                if (item is null) return "--";
                if (at != string.Empty)
                    return $"{item} ({at})";
                else
                    return item;
            }
        }

        public HandsManager(ArenaObservedPlayer player)
        {
            _parent = player;
        }

        /// <summary>
        /// Refresh hands data.
        /// </summary>
        public void Refresh()
        {
            try
            {
                var handsController = Memory.ReadPtr(_parent.HandsControllerAddr); // or FirearmController
                var itemBase = Memory.ReadPtr(handsController + Offsets.ObservedHandsController.ItemInHands);
                if (itemBase != _cached)
                {
                    _cachedItem = null;
                    _ammo = null;
                    _thermal = null;
                    var itemTemplate = Memory.ReadPtr(itemBase + Offsets.LootItem.Template);
                    var itemIDPtr = Memory.ReadValue<Types.MongoID>(itemTemplate + Offsets.ItemTemplate._id);
                    var itemID = Memory.ReadUnityString(itemIDPtr.StringID);
                    if (EftDataManager.AllItems.TryGetValue(itemID, out var heldItem)) // Item exists in DB
                        _cachedItem = heldItem;
                    _cached = itemBase;
                }
                if (_cachedItem?.IsWeapon ?? false)
                {
                    try
                    {
                        var chambers = Memory.ReadPtr(itemBase + Offsets.LootItemWeapon.Chambers);
                        var slotPtr = Memory.ReadPtr(chambers + MemList<byte>.ArrStartOffset + 0 * 0x8); // One in the chamber ;)
                        var slotItem = Memory.ReadPtr(slotPtr + Offsets.Slot.ContainedItem);
                        var ammoTemplate = Memory.ReadPtr(slotItem + Offsets.LootItem.Template);
                        var idPtr = Memory.ReadValue<Types.MongoID>(ammoTemplate + Offsets.ItemTemplate._id);
                        string id = Memory.ReadUnityString(idPtr.StringID);
                        if (EftDataManager.AllItems.TryGetValue(id, out var ammo))
                            _ammo = ammo?.ShortName;
                    }
                    catch 
                    {
                        var ammoTemplate_ = GetAmmoTemplateFromWeapon(itemBase);
                        var ammoIdPtr = Memory.ReadValue<Types.MongoID>(ammoTemplate_ + Offsets.ItemTemplate._id);
                        string ammoId = Memory.ReadUnityString(ammoIdPtr.StringID);
                        if (EftDataManager.AllItems.TryGetValue(ammoId, out var ammo))
                            _ammo = ammo?.ShortName;

                    }
                }
            }
            catch
            {
                _cached = 0x0;
            }
        }


        /// <summary>
        /// Wrapper defining a Chamber Structure.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private readonly struct Chamber
        {
            public static implicit operator ulong(Chamber x) => x._base;
            private readonly ulong _base;

            public readonly bool HasBullet(bool useCache = false)
            {
                if (_base == 0x0)
                    return false;
                return Memory.ReadValue<ulong>(_base + Offsets.Slot.ContainedItem, useCache) != 0x0;
            }
        }
        public static ulong GetAmmoTemplateFromWeapon(ulong lootItemBase)
        {
            var chambersPtr = Memory.ReadValue<ulong>(lootItemBase + Offsets.LootItemWeapon.Chambers);
            ulong firstRound;
            MemArray<Chamber> chambers = null;
            MemArray<Chamber> magChambers = null;
            MemList<ulong> magStack = null;
            try
            {
                if (chambersPtr != 0x0 && (chambers = MemArray<Chamber>.Get(chambersPtr)).Count > 0) // Single chamber, or for some shotguns, multiple chambers
                    firstRound = Memory.ReadPtr(chambers.First(x => x.HasBullet(true)) + Offsets.Slot.ContainedItem);
                else
                {
                    var magSlot = Memory.ReadPtr(lootItemBase + Offsets.LootItemWeapon._magSlotCache);
                    var magItemPtr = Memory.ReadPtr(magSlot + Offsets.Slot.ContainedItem);
                    var magChambersPtr = Memory.ReadPtr(magItemPtr + Offsets.LootItemMod.Slots);
                    magChambers = MemArray<Chamber>.Get(magChambersPtr);
                    if (magChambers.Count > 0) // Revolvers, etc.
                        firstRound = Memory.ReadPtr(magChambers.First(x => x.HasBullet(true)) + Offsets.Slot.ContainedItem);
                    else // Regular magazines
                    {
                        var cartridges = Memory.ReadPtr(magItemPtr + Offsets.LootItemMagazine.Cartridges);
                        var magStackPtr = Memory.ReadPtr(cartridges + Offsets.StackSlot._items);
                        magStack = MemList<ulong>.Get(magStackPtr);
                        firstRound = magStack[0];
                    }
                }
                return Memory.ReadPtr(firstRound + Offsets.LootItem.Template);
            }
            finally
            {
                chambers?.Dispose();
                magChambers?.Dispose();
                magStack?.Dispose();
            }
        }
    }
}
