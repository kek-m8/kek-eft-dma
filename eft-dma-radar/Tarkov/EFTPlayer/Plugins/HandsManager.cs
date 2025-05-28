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
        private string _ubgl;
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
                string at = $"{_ammo} {_thermal}{(_thermal is not null ? $" {_ubgl}" : $"{_ubgl}")}".Trim();
                var item = (_cachedItem?.ID.Equals("67b49e7335dec48e3e05e057") ?? false ? "F-1 (delayed)" : _cachedItem?.ShortName);
                if (item is null) return "--";
                if (item.Contains("127x108"))
                    return $"NSV Ulyos ({_ammo ?? "Unknown"})";
                if (item.Contains("30x29"))
                    return $"AGS-30 (VOG-30)";
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
                    bool a = false, b = false;
                    var itemTemplate = Memory.ReadPtr(itemBase + Offsets.LootItem.Template);
                    var itemIDPtr = Memory.ReadValue<Types.MongoID>(itemTemplate + Offsets.ItemTemplate._id);
                    var itemID = Memory.ReadUnityString(itemIDPtr.StringID);
                    if (EftDataManager.AllItems.TryGetValue(itemID, out var heldItem)) // Item exists in DB
                    {
                        _cachedItem = new LootItem(heldItem);
                        if (heldItem?.IsWeapon ?? false)
                        {
                            if(_parent.Gear.Equipment.TryGetValue("FirstPrimaryWeapon", out var weapon))
                                if(weapon.Id.Equals(_cachedItem.ID, StringComparison.OrdinalIgnoreCase))
                                    a = true; // Item is in primary weapon slot
                            if (_parent.Gear.Equipment.TryGetValue("SecondPrimaryWeapon", out var secondaryWeapon))
                                if (secondaryWeapon.Id.Equals(_cachedItem.ID, StringComparison.OrdinalIgnoreCase))
                                    b = true; // Item is in secondary weapon slot

                            if(a && _parent.Gear.Loot.Where(x => x._parentSlot == "FirstPrimaryWeapon").Any(x => x.IsThermalScope)) // player holding primary weapon (on sling) with thermal scope
                                _thermal = "Thermal";
                            else if (b && _parent.Gear.Loot.Where(x => x._parentSlot == "SecondPrimaryWeapon").Any(x => x.IsThermalScope)) // player holding secondary weapon (on back) with thermal scope
                                _thermal = "Thermal";
                            else
                                _thermal = null;
                            if(a && _parent.Gear.Loot.Where(x => x._parentSlot == "FirstPrimaryWeapon").Any(x => x.IsUBGL))
                                _ubgl = "UBGL";
                            else if (b && _parent.Gear.Loot.Where(x => x._parentSlot == "SecondPrimaryWeapon").Any(x => x.IsUBGL))
                                _ubgl = "UBGL";
                            else
                                _ubgl = null;
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
                    var ammoInChamber = "";
                    var ammoFromMag = "";
                    try
                    {
                        var chambers = Memory.ReadPtr(itemBase + Offsets.LootItemWeapon.Chambers);
                        var slotPtr = Memory.ReadPtr(chambers + MemList<byte>.ArrStartOffset + 0 * 0x8); // One in the chamber ;)
                        var slotItem = Memory.ReadPtr(slotPtr + Offsets.Slot.ContainedItem);
                        var ammoTemplate = Memory.ReadPtr(slotItem + Offsets.LootItem.Template);
                        var ammoIDPtr = Memory.ReadValue<Types.MongoID>(ammoTemplate + Offsets.ItemTemplate._id);
                        var ammoID = Memory.ReadUnityString(ammoIDPtr.StringID);
                        if (EftDataManager.AllItems.TryGetValue(ammoID, out var ammo))
                            _ammo = ammoInChamber = ammo?.ShortName;
                        
                    }
                    catch // gun doesnt have a chamber
                    {
                        var ammoTemplate_ = GetAmmoTemplateFromWeapon(itemBase);
                        var ammoIdPtr = Memory.ReadValue<Types.MongoID>(ammoTemplate_ + Offsets.ItemTemplate._id);
                        string ammoId = Memory.ReadUnityString(ammoIdPtr.StringID);
                        if (EftDataManager.AllItems.TryGetValue(ammoId, out var ammo))
                            _ammo = ammoFromMag = ammo?.ShortName;
                    }

                    if (ammoInChamber != ammoFromMag)
                    {
                        Dictionary<string, int[]> bulletData = new Dictionary<string, int[]>(StringComparer.OrdinalIgnoreCase);
                        var chambers = Memory.ReadPtr(itemBase + Offsets.LootItemWeapon.Chambers);
                        var slotPtr = Memory.ReadPtr(chambers + MemList<byte>.ArrStartOffset + 0 * 0x8); // One in the chamber ;)
                        var slotItem = Memory.ReadPtr(slotPtr + Offsets.Slot.ContainedItem);
                        var ammoTemplate = Memory.ReadPtr(slotItem + Offsets.LootItem.Template);
                        var magTempPtr = GetAmmoTemplateFromWeapon(itemBase);
                        var ammoIdPtr = Memory.ReadValue<Types.MongoID>(ammoTemplate + Offsets.ItemTemplate._id);
                        var magIdPtr = Memory.ReadValue<Types.MongoID>(magTempPtr + Offsets.ItemTemplate._id);
                        string chambersAmmoId = Memory.ReadUnityString(ammoIdPtr.StringID, 32);
                        string magAmmoId = Memory.ReadUnityString(magIdPtr.StringID, 32);
                        if (EftDataManager.AllItems.TryGetValue(chambersAmmoId, out var chambersAmmo) &&
                            EftDataManager.AllItems.TryGetValue(magAmmoId, out var magAmmo))
                        {
                            bulletData.TryAdd(chambersAmmo.ShortName, new int[] { Memory.ReadValue<int>(ammoTemplate + Offsets.AmmoTemplate.Damage), Memory.ReadValue<int>(ammoTemplate + Offsets.AmmoTemplate.PenetrationPower) });
                            bulletData.TryAdd(magAmmo.ShortName, new int[] { Memory.ReadValue<int>(magTempPtr + Offsets.AmmoTemplate.Damage), Memory.ReadValue<int>(magTempPtr + Offsets.AmmoTemplate.PenetrationPower) });
                        }
                        _ammo = bulletData.OrderBy(x => x.Value[1]) // Sort by Penetration Power
                            .First().Key;
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
