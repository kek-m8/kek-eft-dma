using eft_dma_radar.Tarkov.Loot;
using eft_dma_shared.Common.Misc.Data;
using eft_dma_shared.Common.Unity.Collections;

namespace eft_dma_radar.Tarkov.EFTPlayer.Plugins
{
    public sealed class HandsManager
    {
        private readonly Player _parent;

        private string _ammo;
        private string _thermal;
        private LootItem _cachedItem;
        private ulong _cached = 0x0;
        /// <summary>
        /// Current ammo count in Magazine.
        /// </summary>
        public int Count { get; private set; }
        /// <summary>
        /// Maximum ammo count in Magazine.
        /// </summary>
        public int MaxCount { get; private set; }
        /// <summary>
        /// Item in hands currently (Short Name).
        /// Also contains ammo/thermal info.
        /// </summary>
        public string CurrentItem
        {
            get
            {
                string at = $"{_ammo} {_thermal}".Trim();
                var item = _cachedItem?.ShortName;
                if (item is null) return "--";
                if (at != string.Empty)
                    return $"{item} ({at})";
                else
                    return item;
            }
        }

        public HandsManager(Player player)
        {
            _parent = player;
        }

        /// <summary>
        /// Check if item in player's hands has changed.
        /// </summary>
        public void Refresh()
        {
            try
            {
                var handsController = Memory.ReadPtr(_parent.HandsControllerAddr); // or FirearmController
                var handCtrlPtr = Memory.ReadPtr(_parent.HandsControllerAddr);
                var itemBase = Memory.ReadPtr(handsController +
                    (_parent is ClientPlayer ?
                    Offsets.ItemHandsController.Item : Offsets.ObservedHandsController.ItemInHands));
                if (itemBase != _cached)
                {
                    _cachedItem = null;
                    _ammo = null;
                    _thermal = null;
                    var itemTemplate = Memory.ReadPtr(itemBase + Offsets.LootItem.Template);
                    var itemIDPtr = Memory.ReadValue<Types.MongoID>(itemTemplate + Offsets.ItemTemplate._id);
                    var itemID = Memory.ReadUnityString(itemIDPtr.StringID);
                    if (EftDataManager.AllItems.TryGetValue(itemID, out var heldItem)) // Item exists in DB
                    {
                        _cachedItem = new LootItem(heldItem);
                        if (heldItem?.IsWeapon ?? false)
                        {
                            bool hasThermal = _parent.Gear?.Loot?.Any(x =>
                                x.ID.Equals("5a1eaa87fcdbcb001865f75e", StringComparison.OrdinalIgnoreCase) || // REAP-IR
                                x.ID.Equals("5d1b5e94d7ad1a2b865a96b0", StringComparison.OrdinalIgnoreCase) || // FLIR
                                x.ID.Equals("6478641c19d732620e045e17", StringComparison.OrdinalIgnoreCase) || // ECHO
                                x.ID.Equals("63fc44e2429a8a166c7f61e6", StringComparison.OrdinalIgnoreCase) || // ZEUS
                                x.ID.Equals("67641b461c2eb66ade05dba6", StringComparison.OrdinalIgnoreCase) || // SHAKIN
                                x.ID.Equals("609bab8b455afd752b2e6138", StringComparison.OrdinalIgnoreCase) || // REFLEX
                                x.ID.Equals("606f2696f2cb2e02a42aceb1", StringComparison.OrdinalIgnoreCase))   // ULTIMA
                                ?? false;
                            _thermal = hasThermal ?
                                "Thermal" : null;
                        }
                    }
                    else // Item doesn't exist in DB , use name from game memory
                    {
                        var itemNamePtr = Memory.ReadPtr(itemTemplate + Offsets.ItemTemplate.ShortName);
                        var itemName = Memory.ReadUnityString(itemNamePtr)?.Trim();
                        if (string.IsNullOrEmpty(itemName))
                            itemName = "Item";
                        _cachedItem = new("NULL", itemName);
                    }
                    _cached = itemBase;
                }
                if (_cachedItem?.IsWeapon ?? false)
                {
                    try
                    {
                        /*string ammoInChamber = null;
                        string fireType = null;
                        int maxCount = 0;
                        int currentCount = 0;
                        var magSlotPtr = Memory.ReadPtr(itemBase + Offsets.LootItemWeapon._magSlotCache);
                        var chambersPtr = Memory.ReadValue<ulong>(itemBase + Offsets.LootItemWeapon.Chambers);
                        var magItem = Memory.ReadValue<ulong>(magSlotPtr + Offsets.Slot.ContainedItem);
                        if (chambersPtr != 0x0) // Single chamber, or for some shotguns, multiple chambers
                        {
                            using var chambers_ = MemArray<Chamber>.Get(chambersPtr);
                            currentCount += chambers_.Count(x => x.HasBullet());
                            ammoInChamber = GetLoadedAmmoName(chambers_.FirstOrDefault(x => x.HasBullet()));
                            maxCount += chambers_.Count;
                        }
                        if (magSlotPtr != 0)
                        {
                            if (magItem != 0x0)
                            {
                                var magChambersPtr = Memory.ReadPtr(magItem + Offsets.LootItemMod.Slots);

                                using var magChambers = MemArray<Chamber>.Get(magChambersPtr);
                                
                                if (magChambers.Count > 0) // Revolvers, etc.
                                {
                                    maxCount += magChambers.Count;
                                    currentCount += magChambers.Count(x => x.HasBullet());
                                    ammoInChamber = GetLoadedAmmoName(magChambers.FirstOrDefault(x => x.HasBullet()));
                                }
                                else // Regular magazines
                                {
                                    var cartridges = Memory.ReadPtr(magItem + Offsets.LootItemMagazine.Cartridges);
                                    maxCount += Memory.ReadValue<int>(cartridges + Offsets.StackSlot.MaxCount);
                                    var magStackPtr = Memory.ReadPtr(cartridges + Offsets.StackSlot._items);
                                    using var magStack = MemList<ulong>.Get(magStackPtr);
                                    foreach (var stack in magStack) // Each ammo type will be a separate stack
                                    {
                                        if (stack != 0x0)
                                            currentCount += Memory.ReadValue<int>(stack + Offsets.MagazineClass.StackObjectsCount, false);
                                    }
                                }
                            }
                        }
                        Count = currentCount;
                        MaxCount = maxCount;*/
                        var chambers = Memory.ReadPtr(itemBase + Offsets.LootItemWeapon.Chambers);
                        var slotPtr = Memory.ReadPtr(chambers + MemList<byte>.ArrStartOffset + 0 * 0x8); // One in the chamber ;)
                        var slotItem = Memory.ReadPtr(slotPtr + Offsets.Slot.ContainedItem);
                        var ammoTemplate = Memory.ReadPtr(slotItem + Offsets.LootItem.Template);
                        var ammoIDPtr = Memory.ReadValue<Types.MongoID>(ammoTemplate + Offsets.ItemTemplate._id);
                        var ammoID = Memory.ReadUnityString(ammoIDPtr.StringID);
                        if (EftDataManager.AllItems.TryGetValue(ammoID, out var ammo))
                            _ammo = ammo?.ShortName;
                    }
                    catch { }
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
        /// <summary>
        /// Gets the name of the ammo round currently loaded in this chamber, otherwise NULL.
        /// </summary>
        /// <param name="chamber">Chamber to check.</param>
        /// <returns>Short name of ammo in chamber, or null if no round loaded.</returns>
        private static string GetLoadedAmmoName(Chamber chamber)
        {
            if (chamber != 0x0)
            {
                var bulletItem = Memory.ReadValue<ulong>(chamber + Offsets.Slot.ContainedItem);
                if (bulletItem != 0x0)
                {
                    var bulletTemp = Memory.ReadPtr(bulletItem + Offsets.LootItem.Template);
                    var bulletIdPtr = Memory.ReadValue<Types.MongoID>(bulletTemp + Offsets.ItemTemplate._id);
                    var bulletId = Memory.ReadUnityString(bulletIdPtr.StringID, 32);
                    if (EftDataManager.AllItems.TryGetValue(bulletId, out var bullet))
                        return bullet?.ShortName;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the Ammo Template from a Weapon (First loaded round).
        /// </summary>
        /// <param name="lootItemBase">EFT.InventoryLogic.Weapon instance</param>
        /// <returns>Ammo Template Ptr</returns>
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
