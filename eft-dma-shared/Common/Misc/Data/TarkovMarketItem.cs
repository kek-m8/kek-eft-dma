using System.Text.Json.Serialization;

namespace eft_dma_shared.Common.Misc.Data
{
    /// <summary>
    /// Class JSON Representation of Tarkov Market Data.
    /// </summary>
    public class TarkovMarketItem
    {
        /// <summary>
        /// Item ID.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("bsgID")]
        public string BsgId { get; init; } = "NULL";
        /// <summary>
        /// Item Full Name.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("name")]
        public string Name { get; init; } = "NULL";
        /// <summary>
        /// Item Short Name.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("shortName")]
        public string ShortName { get; init; } = "NULL";
        /// <summary>
        /// Highest Vendor Price.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("price")]
        public long TraderPrice { get; init; } = 0;
        /// <summary>
        /// Optimal Flea Market Price.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("fleaPrice")]
        public long FleaPrice { get; init; } = 0;
        /// <summary>
        /// Number of slots taken up in the inventory.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("slots")]
        public int Slots { get; init; } = 1;
        [JsonInclude]
        [JsonPropertyName("categories")]
        public IReadOnlyList<string> Tags { get; init; } = new List<string>();
        /// <summary>
        /// True if this item is Important via the Filters.
        /// </summary>
        [JsonIgnore]
        public bool Important => CustomFilter?.Important ?? false;
        /// <summary>
        /// True if this item is Blacklisted via the Filters.
        /// </summary>
        [JsonIgnore]
        public bool Blacklisted => CustomFilter?.Blacklisted ?? false;
        /// <summary>
        /// Is a Medical Item.
        /// </summary>
        [JsonIgnore]
        public bool IsMed => Tags.Contains("药品");
        /// <summary>
        /// Is a Food Item.
        /// </summary>
        [JsonIgnore]
        public bool IsFood => Tags.Contains("食物和饮料");
        /// <summary>
        /// Is a backpack.
        /// </summary>
        [JsonIgnore]
        public bool IsBackpack => Tags.Contains("背包");
        /// <summary>
        /// Is a Weapon Item.
        /// </summary>
        [JsonIgnore]
        public bool IsWeapon => Tags.Contains("武器");
        /// <summary>
        /// Is gear equipment
        /// </summary>
        public bool IsGear => Tags.Contains("装备") || IsPlateCarrier;
        /// <summary>
        /// Is a Weapon Mod.
        /// </summary>
        [JsonIgnore]
        public bool IsWeaponMod => Tags.Contains("武器配件");
        /// <summary>
        /// Is Currency (Roubles,etc.)
        /// </summary>
        [JsonIgnore]
        public bool IsCurrency => Tags.Contains("钱");
        [JsonIgnore]
        public bool IsBullet => Tags.Contains("弹药");
        [JsonIgnore]
        public bool IsAmmo => Tags.Contains("弹药箱");
        [JsonIgnore]
        public bool IsContainer => Tags.Contains("普通储藏箱");
        [JsonIgnore]
        public bool IsThrowable => Tags.Contains("投掷武器");
        [JsonIgnore]
        public bool IsKey => Tags.Contains("钥匙");
        [JsonIgnore]
        public bool IsHeadset => Tags.Contains("耳机");
        [JsonIgnore]
        public bool IsRig => Tags.Contains("胸挂");
        [JsonIgnore]
        public bool IsArmband => Tags.Contains("Arm Band");
        [JsonIgnore]
        public bool IsGlasses => Tags.Contains("观测装置");
        [JsonIgnore]
        public bool IsMelee => Tags.Contains("刀");
        [JsonIgnore]
        public bool IsArmorPlate => Tags.Contains("Armor Plate");
        [JsonIgnore]
        public bool IsBodyArmor => Tags.Contains("护甲");
        [JsonIgnore]
        public bool IsArmoredEquipment => Tags.Contains("护甲装备");
        [JsonIgnore]
        public bool IsPlateCarrier
        {
            get
            {
                var plate_carrier = new List<string> {
                    "70726573657400000000003e",
                    "67c87145e52edc36aa069ae6",
                    "67c870e5da2a209b2a0ed126",
                    "67c87094d05729369306ce76",
                    "67ab4b2d6f7ae4aa550bbcf6",
                    "67ab49aab9c7a1e18c095686",
                    "67ab2eecfe82855dcc0f2af6",
                    "66b9c65b96edb969cd4f5d7f",
                    "66b9c5db19a3ab39b7175e2b",
                    "66b6296d7994640992013b17",
                    "66b6295a8ca68c6461709efa",
                    "66b6295178bbc0200425f995",
                    "657b351d306ad0bf99008208",
                    "65766a20234b9f6e050a4306",
                    "6576683d303700411c0242d2",
                    "657667f686f11bca4106d383",
                    "657667b5234b9f6e050a42e4",
                    "6576676d86f11bca4106d37b",
                    "65766738234b9f6e050a42d8",
                    "6576670586f11bca4106d36f",
                    "657666ca303700411c0242c6",
                    "6576667d526e320fbe035806",
                    "65766527303700411c0242a6",
                    "657664ec526e320fbe0357fe",
                    "657664ae303700411c02428c",
                    "657662c8234b9f6e050a42b2",
                    "657661ad234b9f6e050a42a2",
                    "6576616086f11bca4106d35f",
                    "657660eb86f11bca4106d34f",
                    "657660a1526e320fbe0357c1",
                    "6576604f86f11bca4106d33d",
                    "65719d367a553968340d88b8",
                    "6571960bacb85662e7024c23",
                    "6571952aacb85662e7024c01",
                    "657194c0289dc422160e08d1",
                    "65719408289dc422160e08c4",
                    "65719339acb85662e7024be2",
                    "64a5366719bab53bd203bf33",
                    "64a536392d2c4e6e970f4121",
                    "639343fce101f4caa40a4ef3",
                    "63737f448b28897f2802b874",
                    "628dc750b910320f4c27a732",
                    "628d0618d1ba6e4fa07ce5a4",
                    "628cd624459354321c4b7fa2",
                    "628b9c7d45122232a872358f",
                    "628b9784bcf6e2659e09b8a2",
                    "61bcc89aef0f505f0c6cd0fc",
                    "61bc85697113f767765c7fe7",
                    "60a3c70cde5f453f634816a3",
                    "60a3c68c37ea821725773ef5",
                    "609e860ebd219504d8507525",
                    "6038b4ca92ec1c3103795a0d",
                    "6038b4b292ec1c3103795a0b",
                    "5fd4c474dd870108a754b241",
                    "5e4ac41886f77406a511c9a8",
                    "5e4abb5086f77406975c9342",
                    "5d5d87f786f77427997cfaef",
                    "5c0e746986f7741453628fe5",
                    "5c0e722886f7740458316a57",
                    "5b44cad286f77402a54ae7e5",
                    "5ab8dced86f774646209ec87",
                    "544a5caa4bdc2d1a388b4568"
                };
                return plate_carrier.Contains(BsgId);
            }
        }
        [JsonIgnore]
        public bool IsArmoredRig => (IsRig && IsPlateCarrier);
        [JsonIgnore]
        public bool IsSpecialItem => Tags.Contains("特殊物品");
        [JsonIgnore]
        public bool IsRocket => Tags.Contains("Rocket") && Tags.Contains("弹药");
        [JsonIgnore]
        public bool IsRocketLauncher => Tags.Contains("Rocket Launcher") && Tags.Contains("武器");

        [JsonIgnore]
        public bool IsPoster => Tags.Contains("Flyer");

        /// <summary>
        /// This field is set if this item has a special filter.
        /// </summary>
        [JsonIgnore]
        public LootFilterEntry CustomFilter { get; private set; }

        /// <summary>
        /// Set the Custom Filter for this item.
        /// </summary>
        public void SetFilter(LootFilterEntry filter)
        {
            if (filter?.Enabled ?? false)
                CustomFilter = filter;
            else
                CustomFilter = null;
        }

        public override string ToString() => Name;

        /// <summary>
        /// Format price numeral as a string.
        /// </summary>
        /// <param name="price">Price to convert to string format.</param>
        public static string FormatPrice(int price)
        {
            if (price >= 1000000000)
                return (price / 1000000000D).ToString("0.##") + "B";
            if (price >= 1000000)
                return (price / 1000000D).ToString("0.##") + "M";
            if (price >= 1000)
                return (price / 1000D).ToString("0") + "K";

            return price.ToString();
        }
    }
}
