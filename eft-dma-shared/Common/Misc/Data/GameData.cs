using OpenTK.Graphics.OpenGL;
using System.Collections.Frozen;
using System.Numerics;

namespace eft_dma_shared.Common.Misc.Data
{
    /// <summary>
    /// Contains Static Game Data.
    /// </summary>
    public static class GameData
    {
        /// <summary>
        /// All Map Names by their Map ID.
        /// </summary>
        public static FrozenDictionary<string, string> MapNames { get; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["default"] = "default",
            ["Labyrinth"] = "The Labyrinth",
            ["woods"] = "Woods",
            ["shoreline"] = "Shoreline",
            ["rezervbase"] = "Reserve",
            ["laboratory"] = "Labs",
            ["interchange"] = "Interchange",
            ["factory4_day"] = "Factory",
            ["factory4_night"] = "Factory",
            ["bigmap"] = "Customs",
            ["lighthouse"] = "Lighthouse",
            ["tarkovstreets"] = "Streets",
            ["Sandbox"] = "Ground Zero",
            ["Sandbox_high"] = "Ground Zero",
            ["Arena_RailwayStation"] = "Skybridge",
            ["Arena_AirPit"] = "Air pit",
            ["Arena_equator_TDM_02"] = "Equator",
            ["Arena_Bowl"] = "Bowl",
            ["Arena_saw"] = "Sawmill",
            ["Arena_Bay5"] = "Bay 5",
            ["Arena_AutoService"] = "Chop Shop",
            ["Arena_Yard"] = "Yard",
            ["Arena_Prison"] = "Fort",
            ["Arena_Iceberg"] = "Iceberg"
        }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);

        public static FrozenDictionary<string, FrozenDictionary<string, Vector3>> Switches { get; } = new Dictionary<string, FrozenDictionary<string, Vector3>>(StringComparer.OrdinalIgnoreCase)
        {
            { "bigmap", new Dictionary<string, Vector3>(StringComparer.OrdinalIgnoreCase)
            {
                 ["switch_develop_00000_Switch"] = new Vector3(113.554016f, -4.01100159f, -43.5665855f),
                 ["ZB-013 Power Switch"] = new Vector3(352.230316f, 2.61458874f, -40.8052826f),
            }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase) },
            { "Lighthouse", new Dictionary<string, Vector3>(StringComparer.OrdinalIgnoreCase)
            {
                ["Lightkeeper Switch 1"] = new Vector3(445.3035f, 33.391f, 457.5599f),
                ["Lightkeeper Switch 2"] = new Vector3(444.6317f, 33.391f, 457.6145f),
            }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase) },
        }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// Exfil Names.
        /// First Key: Map ID
        /// Second Key: Internal Extract Name
        /// </summary>
        public static FrozenDictionary<string, FrozenDictionary<string, string>> ExfilNames { get; } = new Dictionary<string, FrozenDictionary<string, string>>(StringComparer.OrdinalIgnoreCase)
        {
            { "woods", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                // PMC
                ["Factory Gate"] = "Friendship Bridge (Co-Op)",
                ["RUAF Gate"] = "RUAF Gate",
                ["ZB-016"] = "ZB-016",
                ["ZB-014"] = "ZB-014",
                ["UN Roadblock"] = "UN Roadblock",
                ["South V-Ex"] = "Bridge V-Ex",
                ["Outskirts"] = "Outskirts",
                ["un-sec"] = "Northern UN Roadblock",
                // SCAV
                ["Friendship Bridge (Co-Op)"] = "Friendship Bridge (Co-Op)",
                ["Outskirts Water"] = "Scav Bridge",
                ["Dead Man's Place"] = "Dead Man's Place",
                ["The Boat"] = "Boat",
                ["Scav House"] = "Scav House",
                ["East Gate"] = "Scav Bunker",
                ["Mountain Stash"] = "Mountain Stash",
                ["West Border"] = "Eastern Rocks",
                ["Old Station"] = "Old Railway Depot",
                ["RUAF Roadblock"] = "RUAF Roadblock",
            }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase) },
            { "shoreline", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                // PMC
                ["Shorl_V-Ex"] = "Road to North V-Ex",
                ["Road to Customs"] = "Road to Customs",
                ["Road_at_railbridge"] = "Railway Bridge",
                ["Tunnel"] = "Tunnel",
                ["Lighthouse_pass"] = "Path to Lighthouse",
                ["Smugglers_Trail_coop"] = "Smuggler's Path (Co-op)",
                ["Pier Boat"] = "Pier Boat",
                ["RedRebel_alp"] = "Climber's Trail",
                // SCAV
                ["Scav Road to Customs"] = "Road to Customs",
                ["Lighthouse"] = "Lighthouse",
                ["Wrecked Road"] = "Ruined Road",
                ["South Fence Passage"] = "Old Bunker",
                ["RWing Gym Entrance"] = "East Wing Gym Entrance",
                ["Adm Basement"] = "Admin Basement",
                ["Smuggler's Path (Co-op)"] = "Smuggler's Path (Co-op)",
            }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase) },
            { "rezervbase", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                // PMC
                ["EXFIL_Bunker_D2"] = "D-2",
                ["EXFIL_Bunker"] = "Bunker Hermetic Door",
                ["Alpinist"] = "Cliff Descent",
                ["EXFIL_ScavCooperation"] = "Scav Lands (Co-op)",
                ["EXFIL_vent"] = "Sewer Manhole",
                ["EXFIL_Train"] = "Armored Train",
                // SCAV
                ["Bunker Hermetic Door"] = "Depot Hermetic Door",
                ["Scav Lands (Co-Op)"] = "Scav Lands (Co-Op)",
                ["Sewer Manhole"] = "Sewer Manhole",
                ["Exit1"] = "Hole in the Wall by the Mountains",
                ["Exit2"] = "Heating Pipe",
                ["Exit3"] = "??",
                ["Exit4"] = "Checkpoint Fence",
                ["Armored Train"] = "Armored Train",
            }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase) },
            { "Labyrinth", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["labir_exit"] = "The Way Up",
            }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase) },
            { "laboratory", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                // PMC
                ["lab_Elevator_Cargo"] = "Cargo Elevator",
                ["lab_Elevator_Main"] = "Main Elevator",
                ["lab_Vent"] = "Ventilation Shaft",
                ["lab_Elevator_Med"] = "Medical Block Elevator",
                ["lab_Under_Storage_Collector"] = "Sewage Conduit",
                ["lab_Parking_Gate"] = "Parking Gate",
                ["lab_Hangar_Gate"] = "Hangar Gate"
                // No Scav Exfils

            }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase) },
            { "interchange", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                // PMC
                ["SE Exfil"] = "Emercom Checkpoint",
                ["NW Exfil"] = "Railway Exfil",
                ["PP Exfil"] = "Power Station V-Ex",
                ["Interchange Cooperation"] = "Scav Camp (Co-Op)",
                ["Hole Exfill"] = "Hole in the Fence",
                ["Saferoom Exfil"] = "Saferoom Exfil",
                // SCAV
                ["Emercom Checkpoint"] = "Emercom Checkpoint",
                ["Railway Exfil"] = "Railway Exfil",
                ["Scav Camp (Co-Op)"] = "Scav Camp (Co-Op)",
            }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase) },
            { "factory4_day", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                // PMC
                ["Cellars"] = "Cellars",
                ["Gate 3"] = "Gate 3",
                ["Gate 0"] = "Gate 0",
                ["Gate m"] = "Med Tent Gate",
                ["Gate_o"] = "Courtyard Gate",
                // SCAV
                ["Camera Bunker Door"] = "Camera Bunker Door",
                ["Office Window"] = "Office Window",
            }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase) },
            { "factory4_night", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                // PMC
                ["Cellars"] = "Cellars",
                ["Gate 3"] = "Gate 3",
                ["Gate 0"] = "Gate 0",
                ["Gate m"] = "Med Tent Gate",
                ["Gate_o"] = "Courtyard Gate",
                // SCAV
                ["Camera Bunker Door"] = "Camera Bunker Door",
                ["Office Window"] = "Office Window",
            }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase) },
            { "bigmap", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                // PMC
                ["EXFIL_ZB013"] = "ZB-013",
                ["Dorms V-Ex"] = "Dorms V-Ex",
                ["ZB-1011"] = "ZB-1011",
                ["Crossroads"] = "Crossroads",
                ["Old Gas Station"] = "Old Gas Station",
                ["Trailer Park"] = "Trailer Park",
                ["RUAF Roadblock"] = "RUAF Roadblock",
                ["Smuggler's Boat"] = "Smuggler's Boat",
                ["ZB-1012"] = "ZB-1012",
                ["customs_secret_voron_boat"] = "(Secret) Smugglers' Boat",
                ["customs_secret_voron_bunker"] = "(Secret) Smugglers' Bunker (ZB-1012)",
                ["Customs_scav_pmc"] = "Boiler Room Basement (Co-op)",
                ["customs_sniper_exit"] = "Railroad Passage (Flare)",
                // SCAV
                ["Shack"] = "Military Base CP",
                ["Beyond Fuel Tank"] = "Passage Between Rocks",
                ["Railroad To Military Base"] = "Railroad to Military Base",
                ["Old Road Gate"] = "Old Road Gate",
                ["Sniper Roadblock"] = "Sniper Roadblock",
                ["Railroad To Port"] = "Railroad To Port",
                ["Trailer Park Workers Shack"] = "Trailer Park Workers Shack",
                ["Railroad To Tarkov"] = "Railroad To Tarkov",
                ["RUAF Roadblock_scav"] = "RUAF Roadblock",
                ["Warehouse 17"] = "Warehouse 17",
                ["Factory Shacks"] = "Factory Shacks",
                ["Warehouse 4"] = "Warehouse 4",
                ["Old Azs Gate"] = "Old Gas Station",
                ["Factory Far Corner"] = "Factory Far Corner",
                ["Administration Gate"] = "Administration Gate",
                ["Military Checkpoint"] = "Scav Checkpoint",
                ["Customs_scav_pmc"] = "Boiler Room Basement (Co-op)",
            }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase) },
            { "lighthouse", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                // PMC
                ["V-Ex_light"] = "Road to Military Base V-Ex",
                ["tunnel_shared"] = "Side Tunnel (Co-Op)",
                ["Alpinist_light"] = "Mountain Pass",
                ["Shorl_free"] = "Path to Shoreline",
                ["Nothern_Checkpoint"] = "Northern Checkpoint",
                ["Coastal_South_Road"] = "Southern Road",
                ["EXFIL_Train"] = "Armored Train",
                // SCAV
                ["Side Tunnel (Co-Op)"] = "Side Tunnel (Co-Op)",
                ["Shorl_free_scav"] = "Path to Shoreline",
                ["Scav_Coastal_South"] = "Southern Road",
                ["Scav_Underboat_Hideout"] = "Hideout Under the Landing Stage",
                ["Scav_Hideout_at_the_grotto"] = "Scav Hideout at the Grotto",
                ["Scav_Industrial_zone"] = "Industrial Zone Gates",
                ["Armored Train"] = "Armored Train",
            }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase) },
            { "tarkovstreets", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                // PMC
                ["E8_yard"] = "Courtyard",
                ["E7_car"] = "Primorsky Ave Taxi V-Ex",
                ["E1"] = "Stylobate Building Elevator",
                ["E4"] = "Crash Site",
                ["E2"] = "Sewer River",
                ["E3"] = "Damaged House",
                ["E5"] = "Collapsed Crane",
                ["E6"] = "??",
                ["E9_sniper"] = "Klimov Street",
                ["Exit_E10_coop"] = "Pinewood Basement (Co-Op)",
                ["E7"] = "Expo Checkpoint",
                // SCAV
                ["scav_e1"] = "Basement Descent",
                ["scav_e2"] = "Entrance to Catacombs",
                ["scav_e3"] = "Ventilation Shaft",
                ["scav_e4"] = "Sewer Manhole",
                ["scav_e5"] = "Near Kamchatskaya Arch",
                ["scav_e7"] = "Cardinal Apartment Complex Parking",
                ["scav_e8"] = "Klimov Shopping Mall Exfil",
                ["scav_e6"] = "Pinewood Basement (Co-Op)",
            }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase) },
            { "Sandbox", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                // PMC
                ["Sandbox_VExit"] = "Police Cordon V-Ex",
                ["Unity_free_exit"] = "Emercom Checkpoint",
                ["Scav_coop_exit"] = "Scav Checkpoint (Co-Op)",
                ["Nakatani_stairs_free_exit"] = "Nakatani Basement Stairs",
                ["Sniper_exit"] = "Mira Ave",
                // SCAV
                ["Scav Checkpoint (Co-Op)"] = "Scav Checkpoint (Co-Op)",
                ["Emercom Checkpoint"] = "Emercom Checkpoint",
                ["Nakatani Basement Stairs"] = "Nakatani Basement Stairs",
            }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase) },
            { "Sandbox_high", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                // PMC
                ["Sandbox_VExit"] = "Police Cordon V-Ex",
                ["Unity_free_exit"] = "Emercom Checkpoint",
                ["Scav_coop_exit"] = "Scav Checkpoint (Co-Op)",
                ["Nakatani_stairs_free_exit"] = "Nakatani Basement Stairs",
                ["Sniper_exit"] = "Mira Ave",
                // SCAV
                ["Scav Checkpoint (Co-Op)"] = "Scav Checkpoint (Co-Op)",
                ["Emercom Checkpoint"] = "Emercom Checkpoint",
                ["Nakatani Basement Stairs"] = "Nakatani Basement Stairs",
            }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase) },
        }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);

        public static FrozenDictionary<string, Memory<Vector3>> Mines { get; } = new Dictionary<string, Memory<Vector3>>(StringComparer.OrdinalIgnoreCase)
        {
            ["shoreline"] = new Vector3[]
            {
                new Vector3(499.9635f, -39.92999f, -72.69531f),
                new Vector3(-699.439f, -29.50998f, -273.5012f),
                new Vector3(-691.5459f, -4.029968f, -375.4694f),
                new Vector3(-599.7345f, -4.029968f, -372.1453f),
                new Vector3(-744.8279f, -29.50998f, -241.6526f),
                new Vector3(105.4431f, -27.02997f, -403.9787f),
                new Vector3(-635.5005f, -4.029968f, -357.414f),
                new Vector3(498.1739f, -39.92999f, 136.2064f),
                new Vector3(-787.1689f, -24.60001f, -164.4056f),
                new Vector3(239.4597f, -27.02997f, -342.4171f),
                new Vector3(494.5426f, -39.92999f, 50.37054f),
                new Vector3(-648.1046f, -4.029968f, -373.7787f),
                new Vector3(-737.3292f, -15.22998f, -308.6106f),
                new Vector3(-554.8488f, -4.029968f, -417.788f),
                new Vector3(-765.0375f, -24.60001f, -275.8228f),
                new Vector3(-931.86f, -55.2f, 417.52f),
                new Vector3(-890.94f, -55.2f, 455.55f)
            },
            ["woods"] = new Vector3[]
            {
                new Vector3(-645.4855f, 19.97001f, -155.1335f),
                new Vector3(463.9968f, 21.35f, -422.8937f),
                new Vector3(563.6989f, -5.529996f, 31.74424f),
                new Vector3(324.341f, 21.35f, -459.362f),
                new Vector3(275.7999f, -10.45f, 421.865f),
                new Vector3(577.9504f, -10.45f, 308.2255f),
                new Vector3(452.8595f, 21.35f, -710.1129f),
                new Vector3(468.3384f, 21.35f, -574.8652f),
                new Vector3(508.127f, 13.86176f, -250.842f),
                new Vector3(330.9692f, 21.35f, -507.4193f),
                new Vector3(480.4347f, -10.45f, 411.8292f)
            },
            ["lighthouse"] = new Vector3[]
            {
                new Vector3(-227.9976f, 13.94f, -520.8323f),
                new Vector3(-5.122513f, 7.400002f, -424.614f),
                new Vector3(-500.6f, 43.8f, -515.35f),
                new Vector3(-147.3805f, 5.399998f, -482.0828f),
                new Vector3(-139.1587f, 7.400002f, -450.7613f),
                new Vector3(-242.2297f, 18.1f, -478.8835f),
                new Vector3(52.59197f, 7.400002f, -468.8979f),
                new Vector3(49.40002f, 21.505f, -1004.75f),
                new Vector3(-551.0999f, 43.8f, -393.55f),
                new Vector3(40.47018f, 7.400002f, -404.0539f),
                new Vector3(-105.1f, 21.505f, -1007.75f),
                new Vector3(75.58275f, 11.61f, -419.8874f),
                new Vector3(-331.39f, 33.6f, -523.8537f),
                new Vector3(57.73361f, 7.400002f, -522.3037f),
                new Vector3(-330.2926f, 39.67f, -490.8548f),
                new Vector3(-448.7f, 22.8f, -785.8f),
                new Vector3(30.35561f, 12.3f, -750.6716f),
                new Vector3(243.467f, 0.06f, 534.935f),
                new Vector3(218.545f, 0.296f, 510.691f),
                new Vector3(305.809f, 1.946f, 502.395f),
                new Vector3(212.385f, 0.191f, 509.581f),
                new Vector3(234.907f, 1.876f, 518.332f),
                new Vector3(245.038f, 0.106f, 532.528f),
                new Vector3(267.026f, 1.9413f, 523.865f),
                new Vector3(313.064f, 1.819f, 510.908f),
                new Vector3(-254.375f, 27.914f, -566.485f),
                new Vector3(241.506f, 0.291f, 529.897f),
                new Vector3(-261.801f, 30.935f, -564.372f),
                new Vector3(216.867f, 0.218f, 529.055f),
                new Vector3(204.669f, 0.256f, 509.298f),
                new Vector3(227.034f, 0.17f, 526.597f),
                new Vector3(213.289f, 0.283f, 513.858f),
                new Vector3(-241.043f, 20.353f, -551.301f),
                new Vector3(-304.48f, 36.018f, -590.176f),
                new Vector3(-302.0087f, 34.9046f, -601.7734f),
                new Vector3(243.095f, 0.239f, 512.974f),
                new Vector3(-226.5704f, 17.129f, -568.9507f),
                new Vector3(209.8172f, 1.8455f, 519.1801f),
                new Vector3(287.989f, 1.831f, 511.113f),
                new Vector3(240.522f, 0.261f, 529.29f),
                new Vector3(281.0385f, 1.9916f, 509.6941f),
                new Vector3(225.505f, 0.2f, 508.035f),
                new Vector3(261.784f, 1.81f, 524.219f),
                new Vector3(215.506f, 2.212f, 523.973f),
                new Vector3(314.293f, 2.02f, 512.347f),
                new Vector3(-299.326f, 36.809f, -591.756f),
                new Vector3(242.773f, 1.805f, 514.362f),
                new Vector3(204.56f, 0.196f, 513.489f),
                new Vector3(221.26f, 0.247f, 529.405f),
                new Vector3(231.71f, 1.922f, 523.019f),
                new Vector3(188.267f, 0.22f, 512.059f),
                new Vector3(208.918f, 0.199f, 511.258f),
                new Vector3(219.691f, -0.04f, 533.976f),
                new Vector3(210.84f, 1.808f, 516.62f),
                new Vector3(224.375f, 0.203f, 530.353f),
                new Vector3(282.3838f, 1.941f, 520.9704f),
                new Vector3(292.245f, 1.851f, 511.783f),
                new Vector3(313.573f, 2.1749f, 511.481f),
                new Vector3(265.621f, 1.987f, 514.836f),
                new Vector3(311.197f, 1.807f, 502.797f),
                new Vector3(235.675f, 0.251f, 513.662f),
                new Vector3(231.517f, 0.15f, 509.4561f),
                new Vector3(-294.107f, 34.157f, -584.707f),
                new Vector3(202.114f, 0.232f, 509.03f),
                new Vector3(223.196f, 0.107f, 504.332f),
                new Vector3(228.55f, 0.148f, 508.77f),
                new Vector3(307.394f, 0.001f, 496.676f),
                new Vector3(209.24f, 0.126f, 509.643f),
                new Vector3(239.483f, 1.9675f, 516.637f),
                new Vector3(233.663f, 0.161f, 509.12f),
                new Vector3(225.16f, 0.188f, 512.872f),
                new Vector3(222.9225f, 1.846f, 520.0113f),
                new Vector3(303.735f, -0.017f, 484.437f),
                new Vector3(204.284f, 0.264f, 512.533f),
                new Vector3(252.541f, 0.103f, 511.602f),
                new Vector3(251.664f, 0.147f, 530.302f),
                new Vector3(250.874f, 0.192f, 510.51f),
                new Vector3(246.099f, 0.029f, 533.724f),
                new Vector3(242.727f, 0.194f, 532.77f),
                new Vector3(241.321f, 0.26f, 511.347f),
                new Vector3(288.9668f, 0.2685f, 503.1812f),
                new Vector3(311.813f, 2.02f, 499.782f),
                new Vector3(242.398f, 0.324f, 526.56f),
                new Vector3(249.112f, 0.289f, 528.831f),
                new Vector3(210.786f, 0.078f, 531.712f),
                new Vector3(229.978f, 0.295f, 512.848f),
                new Vector3(221.231f, 0.208f, 530.59f),
                new Vector3(250.654f, 0.117f, 531.424f),
                new Vector3(310.712f, 2.5435f, 501.1422f),
                new Vector3(312.734f, 0.009f, 516.965f),
                new Vector3(305.01f, 1.8079f, 513.95f),
                new Vector3(277.671f, 1.808f, 515.202f),
                new Vector3(214.3559f, 0.1089f, 505.0143f),
                new Vector3(-306.3855f, 33.7312f, -605.2985f),
                new Vector3(250.478f, 0.214f, 529.053f),
                new Vector3(218.194f, 0.001f, 533.442f),
                new Vector3(227.412f, 2.076f, 514.884f),
                new Vector3(-213.891f, 19.136f, -572.236f),
                new Vector3(265.093f, 1.811f, 513.836f),
                new Vector3(267.79f, 1.804f, 512.263f),
                new Vector3(276.736f, 1.795f, 513.9884f),
                new Vector3(247.271f, 0.1f, 510.983f),
                new Vector3(259.936f, 1.811f, 520.864f),
                new Vector3(242.277f, 0.251f, 513.763f),
                new Vector3(-297.62f, 35.021f, -592.334f),
                new Vector3(-302.316f, 34.99f, -601.562f),
                new Vector3(280.814f, 1.87f, 514.124f),
                new Vector3(194.924f, 0.499f, 513.597f),
                new Vector3(189.914f, 0.123f, 508.848f),
                new Vector3(216.931f, 0.131f, 507.189f),
                new Vector3(-230.42f, 18.326f, -577.458f),
                new Vector3(-285.777f, 31.734f, -583.832f),
                new Vector3(243.954f, 0.223f, 529.512f),
                new Vector3(247.851f, 1.847f, 517.421f),
                new Vector3(216.0778f, 2.139f, 517.0784f),
                new Vector3(217.797f, 2.08f, 516.992f),
                new Vector3(219.4027f, 0.337f, 510.5761f),
                new Vector3(214.897f, 2.009f, 516.243f),
                new Vector3(240.0462f, 0.238f, 530.9058f),
                new Vector3(-325.07f, 32.883f, -583.522f),
                new Vector3(251.121f, 2.001f, 524.156f),
                new Vector3(217.61f, 0.1797f, 529.814f),
                new Vector3(-250.558f, 29.142f, -557.882f),
                new Vector3(242.346f, 0.209f, 510.017f)
            },
            ["tarkovstreets"] = new Vector3[]
            {
                new Vector3(92.283f, 2.9507f, 263.573f),
                new Vector3(88.697f, 3.3873f, 371.708f),
                new Vector3(83.009f, 2.8501f, 263.2695f),
                new Vector3(104.822f, 3.418f, 308.075f),
                new Vector3(105.357f, 2.977f, 352.216f),
                new Vector3(105.35f, 3.05f, 366.73f),
                new Vector3(92.775f, 3.3893f, 374.857f),
                new Vector3(98.94f, 2.916f, 262.71f),
                new Vector3(46.501f, 2.7206f, 267.643f),
                new Vector3(37.80185f, 2.722f, 269.9368f),
                new Vector3(105.1584f, 3.722f, 287.9238f),
                new Vector3(47.919f, 2.7251f, 268.033f),
                new Vector3(45.207f, 2.7263f, 272.843f),
                new Vector3(52.22114f, 2.725f, 267.9434f),
                new Vector3(36.72f, 2.828f, 261.89f),
                new Vector3(67.935f, 2.777f, 263.191f),
                new Vector3(88.78127f, 3.4018f, 373.5036f),
                new Vector3(58.8f, 2.7403f, 263.57f),
                new Vector3(102.2167f, 3.428f, 379.8588f),
                new Vector3(105.4046f, 3.2931f, 373.3855f),
                new Vector3(48.675f, 2.67f, 269.606f),
                new Vector3(106.95f, 3.733f, 301.691f),
                new Vector3(45.242f, 2.7206f, 268.472f),
                new Vector3(105.0592f, 2.9542f, 352.677f),
                new Vector3(45.867f, 2.7263f, 271.54f),
                new Vector3(40.916f, 2.7233f, 273.72f),
                new Vector3(107.006f, 3.722f, 295.373f),
                new Vector3(38.927f, 2.7252f, 269.149f),
                new Vector3(92.785f, 3.4446f, 375.69f),
                new Vector3(42.874f, 2.7241f, 276.938f),
                new Vector3(103.822f, 2.755f, 363.753f),
                new Vector3(52.78309f, 2.7246f, 329.9346f),
                new Vector3(54.088f, 2.831f, 260.994f),
                new Vector3(45.06f, 2.8152f, 262.17f),
                new Vector3(52.38234f, 2.7343f, 272.6624f),
                new Vector3(48.372f, 2.7343f, 274.492f),
                new Vector3(85.5906f, 2.9507f, 260.9251f),
                new Vector3(92.09009f, 3.4253f, 368.8048f),
                new Vector3(85.997f, 2.9507f, 260.866f),
                new Vector3(100.5803f, 3.4782f, 373.1563f),
                new Vector3(62.1f, 2.7403f, 260.86f),
                new Vector3(35.624f, 2.7189f, 269.117f),
                new Vector3(93.70396f, 3.4114f, 374.884f),
                new Vector3(38.04f, 2.6144f, 267.708f),
                new Vector3(103.822f, 2.755f, 361.009f),
                new Vector3(53.70294f, 2.726f, 326.0333f),
                new Vector3(106.322f, 3.231f, 367.113f),
                new Vector3(53.6932f, 2.7457f, 324.678f),
                new Vector3(43.0699f, 2.6349f, 275.6749f),
                new Vector3(105.0592f, 2.987f, 351.97f),
                new Vector3(48.46088f, 2.7263f, 320.8043f),
                new Vector3(44.48053f, 2.7234f, 279.8209f),
                new Vector3(49.102f, 2.67f, 268.956f),
                new Vector3(46.433f, 2.715f, 297.83f),
                new Vector3(46.85299f, 2.7884f, 291.3539f),
                new Vector3(93.62f, 2.916f, 263.76f),
                new Vector3(45.088f, 2.67f, 270.134f),
                new Vector3(105.332f, 3.0646f, 367.954f),
                new Vector3(50.462f, 2.7343f, 274.458f),
                new Vector3(99.65798f, 3.4866f, 372.6137f)
            },
            ["Sandbox"] = new Vector3[]
            {
                new Vector3(220.8055f, 15.65f, 133.9053f),
                new Vector3(222.7634f, 15.65f, 175.4689f),
                new Vector3(60.41f, 23.578f, 170.799f)
            },
            ["Sandbox_high"] = new Vector3[]
            {
                new Vector3(220.8055f, 15.65f, 133.9053f),
                new Vector3(222.7634f, 15.65f, 175.4689f),
                new Vector3(60.41f, 23.578f, 170.799f)
            }
        }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);

        public static List<Vector3> EventTraps { get; } = new List<Vector3>()
        {
            new Vector3(-42.104f, 1.476f, -13.391f),
            new Vector3(28.8755f, -0.48149997f, 40.758f),
            new Vector3(-5.461f, 1.86186087f, -31.705f),
            new Vector3(-5.86f, 1.86186087f, -31.705f),
            new Vector3(28.9215f, 0.3685f, 40.737f),
            new Vector3(-39.11657f, 1.97947586f, -13.391f),
            new Vector3(-38.46725f, -0.315859318f, 51.03258f),
            new Vector3(1.2609992f, 0.147615388f, 16.7568f),
            new Vector3(14.5923824f, 0.145615384f, 5.888755f),
            new Vector3(-2.34400058f, 0.151615381f, 29.779f),
            new Vector3(7.86999941f, 0.14861539f, 23.1279984f),
            new Vector3(7.198999f, 0.145615384f, 14.2077541f),
            new Vector3(-19.113615f, 0.149615392f, 30.1537552f),
            new Vector3(-27.8516159f, 0.146615386f, 25.3427544f),
            new Vector3(5.24699926f, 0.147615388f, 4.20400047f),
            new Vector3(-23.26f, 0.145615384f, 14.5407562f),
            new Vector3(-12.7157f, 0.13793f, 6.25764f),
            new Vector3(-38.46725f, -0.315856934f, 55.59258f),
            new Vector3(-2.78f, -1.622f, 21.42f),
        };

        public static FrozenDictionary<string, Memory<Vector3>> EventSwitches { get; } = new Dictionary<string, Memory<Vector3>>(StringComparer.OrdinalIgnoreCase)
        {
            ["Alarm Switch"] = new Vector3[]
            {

                new Vector3(-4.279f, 1.51507258f, 55.87111f),
                new Vector3(1.05600023f, 1.50388885f, 7.16910934f),
                new Vector3(-13.3701f, 1.63888884f, 36.08191f),
                new Vector3(8.937f, 1.54792f, 28.68511f),
                new Vector3(-9.011f, 1.66788888f, 1.57610893f),
            },
            ["Sealed Door"] = new Vector3[]
            {

                new Vector3(40.1832f, 0.298618853f, 19.1903f),
                new Vector3(-49.34948f, 1.70373631f, -11.756073f),
            },
            ["Fire Trap Switch"] = new Vector3[]
            {
                new Vector3(-43.587f, 1.56588888f, -10.9208889f)
            },
            ["Toxic Pool Trap"] = new Vector3[]
            {
                new Vector3(-31.7254715f, 2.08068f, 58.2143f)
            },
            ["Shotgun Trap Switch"] = new Vector3[]
            {
                new Vector3(25.4025669f, 1.37f, 59.453f)
            },
            ["Toxic Puddle Switch"] = new Vector3[]
            {
                new Vector3(46.42f, 1.031f, 11.084f)
            },
            ["Steam Trap Switch"] = new Vector3[]
            {
                new Vector3(2.659f, 2.109861f, -31.705f)
            }

        }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);

        public static FrozenDictionary<string, int> PlateLevel { get; } = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            //Class 6
            ["Korund-VM-K ballistic plate (Back)"] = 6,
            ["Granit 4RS ballistic plates (Back)"] = 6,
            ["Granit 4RS ballistic plate (Front)"] = 6,
            ["Korund-VM-K ballistic plates (Front)"] = 6,
            ["KITECO SC-IV SA ballistic plate"] = 6,
            ["GAC 4sss2 ballistic plate"] = 6,
            ["Kiba Arms Steel ballistic plate"] = 6,
            ["ESAPI level IV ballistic plate"] = 6,
            ["NESCO 4400-SA-MC ballistic plate"] = 6,
            ["Granit Br5 ballistic plate"] = 6,
            ["Granit ballistic plate (Side)"] = 6,
            ["Cult Termite ballistic plate"] = 6,
            ["ESBI level IV ballistic plate (Side)"] = 6,
            ["Granit ballistic plate (Side)"] = 6, // 14
            //Class 5
            ["Korund-VM ballistic plate (Back)"] = 5,
            ["Granit 4 ballistic plates (Back)"] = 5,
            ["Granit 4 ballistic plate (Front)"] = 5,
            ["Korund-VM ballistic plates (Front)"] = 5,
            ["GAC 3s15m ballistic plate"] = 5,
            ["SAPI level III+ ballistic plate"] = 5,
            ["TallCom Guardian ballistic plate"] = 5,
            ["Granit Br4 ballistic plate"] = 5,
            ["Cult Locust ballistic plate"] = 5,
            ["SSAPI level III+ ballistic plate (Side)"] = 5,
            ["Korund-VM ballistic plate (Side)"] = 5, // 25
            //Class 4
            ["6B23-2 ballistic plate (Back)"] = 4,
            ["6B13 custom ballistic plates (Back)"] = 4,
            ["6B33 ballistic plate (Front)"] = 4,
            ["Monoclete level III PE ballistic plate"] = 4,
            ["Global Armor’s Steel ballistic plate"] = 4,
            ["NewSphereTech level III ballistic plate"] = 4,
            ["SPRTN Elaphros ballistic plate"] = 4,
            ["SPRTN Omega ballistic plate"] = 4,
            ["Kiba Arms Titan ballistic plate"] = 4, // 34
            // Class 3
            ["Zhuk-3 ballistic plate (Front)"] = 3,
            ["6B12 ballistic plates (Front)"] = 3,
            ["PRTCTR Lightweight ballistic plate"] = 3, // 37

        }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);
        public static FrozenDictionary<string, string> GrenadeData { get; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // explosive
            ["F-1"] = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAAXNSR0IArs4c6QAAAxZJREFUSEvNlU1oHGUYx38zszOzszuZ2U12sxFiPooKrSAq9aRELWJViISg0TbYeq8Iop48efNQvAgKngSLqCCldxU8BdGYSIixSU3WJtnvnd1Jsp/zJbN4bBKXtOB7ft/n9/z/75/nEbjHR7jH9fn/AeYvvv7zjes3rnZcd8P3/fs8z/sJaB7mRF8Kzpx56N1TkxOXv//hx2/abfczwDrO4r4Ap08/cMXQB2aWl5fpOME8ULprgHQ6/WAmY74pBmJ6a2tzuNl0X/AgetcAU1NPvJdKpd/qNFuxWxu3WvlCcWyv0X0fuHoU5D9ZdPbsw6889sijr25v/z03MjJCtVpjcXGJfNEi4OgkHgvIZMzJubm5z5d+/eU5SZIYHR2l0WiwsrJKNpvHOyFAmJl+8Vomk7qYzW4iKxJJM0G361IpW6yu/Um5WsfzD1dxlAJZ17WZS/MXvnWcFu1Ok2g0yvj9Y6yvrxOJKPyxtsFvyyt0He8S8OWd/uJQQASe1I3YRy9Pv/RUpVKi3WqgaVGGhoaoVi0USWEnV2D15l80W50KkO4LIIp8aAzEP5idmZZy+dv4rodhDKBpGpZlIQYCgSCz+PsqxVIF3/fv2OyhCkT42DBj75x7ZgrDjJHb2WV8fIyJiQkWFhbAC1CiOksra2zv5HBdtz+ABK8ZifjXIaBYzHOwb5NMJlAUBVEUsa06bcelUK6Ty5dDd/oDRGFcNbSNZ889LR8c7FHYzTGcSaGqSi+moWVSRCV7O89urtS/grAlPa4unj///ON7+3Vsq8bwcKoXVcuq9gCCKLN2c5NK1SYIgv4UhIBoVPrq1OTkBc93CMMej2sIgsD+gY0syzhdj+3dEq22279F4YuIxHeJhDlrmgaGruM4nTAtBIFHLBbDsvfIbuXDcTELXO8rpv9evqxFI18kk0kGBwdxXQdJCJ0IIQG5Qgnbbh45j46dRZJIoKoypmkSj8fRFJVOt029ZlO27LD4G8C1E200AQJZltB1HVVVcd0utZqN6wVXgE9PPK7DAqLQc6V3QpjreJ/48PZxC+cfG21HKDqzxmYAAAAASUVORK5CYII=",
            ["F-1 RD"] = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAAXNSR0IArs4c6QAAAxZJREFUSEvNlU1oHGUYx38zszOzszuZ2U12sxFiPooKrSAq9aRELWJViISg0TbYeq8Iop48efNQvAgKngSLqCCldxU8BdGYSIixSU3WJtnvnd1Jsp/zJbN4bBKXtOB7ft/n9/z/75/nEbjHR7jH9fn/AeYvvv7zjes3rnZcd8P3/fs8z/sJaB7mRF8Kzpx56N1TkxOXv//hx2/abfczwDrO4r4Ap08/cMXQB2aWl5fpOME8ULprgHQ6/WAmY74pBmJ6a2tzuNl0X/AgetcAU1NPvJdKpd/qNFuxWxu3WvlCcWyv0X0fuHoU5D9ZdPbsw6889sijr25v/z03MjJCtVpjcXGJfNEi4OgkHgvIZMzJubm5z5d+/eU5SZIYHR2l0WiwsrJKNpvHOyFAmJl+8Vomk7qYzW4iKxJJM0G361IpW6yu/Um5WsfzD1dxlAJZ17WZS/MXvnWcFu1Ok2g0yvj9Y6yvrxOJKPyxtsFvyyt0He8S8OWd/uJQQASe1I3YRy9Pv/RUpVKi3WqgaVGGhoaoVi0USWEnV2D15l80W50KkO4LIIp8aAzEP5idmZZy+dv4rodhDKBpGpZlIQYCgSCz+PsqxVIF3/fv2OyhCkT42DBj75x7ZgrDjJHb2WV8fIyJiQkWFhbAC1CiOksra2zv5HBdtz+ABK8ZifjXIaBYzHOwb5NMJlAUBVEUsa06bcelUK6Ty5dDd/oDRGFcNbSNZ889LR8c7FHYzTGcSaGqSi+moWVSRCV7O89urtS/grAlPa4unj///ON7+3Vsq8bwcKoXVcuq9gCCKLN2c5NK1SYIgv4UhIBoVPrq1OTkBc93CMMej2sIgsD+gY0syzhdj+3dEq22279F4YuIxHeJhDlrmgaGruM4nTAtBIFHLBbDsvfIbuXDcTELXO8rpv9evqxFI18kk0kGBwdxXQdJCJ0IIQG5Qgnbbh45j46dRZJIoKoypmkSj8fRFJVOt029ZlO27LD4G8C1E200AQJZltB1HVVVcd0utZqN6wVXgE9PPK7DAqLQc6V3QpjreJ/48PZxC+cfG21HKDqzxmYAAAAASUVORK5CYII=",
            ["RGD-5"] = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAAXNSR0IArs4c6QAAAmVJREFUSEtjZKAxYCTF/NySzGnCPOIFDQ0Nv4jVR5IFja3N/1+/ecEwpX8q0fqIVghyMcgCdg42horicqL1Ea2wu7ub+y/Tvy8/f/xiEOTl58jLy/tJTDARbUFDQwMHBy/n90+fPjF8FhbnmExtC0Curaqv/v/p0wfaxUFJVdH/ly+fMyyeu5xonxOtMDU7KYydjXPlx0/vqGtBYHig2vdvn5dz83IbsbKyguP1y5cvjwUEBJs42NivzJm+4AS+yMbrA99wT4vfP34dFxURA5vx+89vht+/fzMICgoxiIlIMdx/eOsjDw9P+Owp83fisgSvBTYOlpeFRAV0uLi4wQb//g3JwGJiEgwqShoMT548Ynj99vWclYtWpZJlgYWN6X8FZXmw4V+/fmH4/esP2BwBQQEGRQVlhh/ffzLcuXPz+Y7Ne6TItkBaDqL39YvXcDO4eDkZBAQEGXh4eBnu37vHsHf7QZwhgTeIQD4QEOaDhD/U9SA2KxsLAzc3DwMXJxfDs2fPKLOAm5cLq+9BloDAp/effx47dIqD7CASkxQB6/316xfYFzCDQWJsbGwMb16+u37s0Ektsi0ABREoOEApCGQJyFCYhSD2x/dfjh/cc9iKLAv0jXT/S8qKgS0AAVCYw/ICzMLf339P27vrUDZZFlhYm85mY2dNYWVnBQeNjIwcw5cvnxm+ffsKDzJmJiaTXVv3nyXLApAmc2uT24yMjCogNijCQRbBUhQzI8uiXdv2xuMyHCROVGFnbW/R+vXLV09mFhZpJibGP+xsbE84uNgX7Nl+cDo+w4m2gJAhFPuAEgsAxbDuGUWwY0wAAAAASUVORK5CYII=",
            ["RGN"] = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAAXNSR0IArs4c6QAAAspJREFUSEvNlctPE1EYxU8708f0MdPngFBAAhrQmhgXJiYGwUQ2KC4MMaxMtDs27Fzq0h1/ggZZGBY2MeoCIlISrQlGYngo2kB5tvQB0ynttBQ65o4pIQSc0IbEu5ub+53fd853J1eDU16aU9bH/wHo63v0rFDAY+I2l88jk8mA0mqeDw+/fqiWgKoDn8/n0WrlFZqmYDUbYGbtsNlsyEk5TM/MxV8ODvH/gqgCOjpuLDicjkYi0tzo2QeQ77XlRUQi8eHBoVf3j4OoArq6OmUjY0ZOyqCh4SzcvBtOp1NxEA6HsRReXH33frSuLEBPz52bRZn+oGR/CCAIAqKRKEK/57dGRgOOsgDd3Z0DNjvfT4ZKALW1dThTU604SCaTiMfimJ2ZLgYmglRZgNtdt6YcrqrLBwEutws2joOQSiERT2D+5xwCE8Fjo1adQW/vvRYALaKY9hMHJUAmm1EcRCLrSb//rassBweLrnjrZKvDA47JwqCnkS06sZ1Ow0AXAiNjX9srArRfa+2X5dyA0aCDwcgoALJEMQ1RlGDgKPv4eFg4CqIaUWfbxQcaTeEFy1qh12nAMCYwJouiJWW3EUukEIsJiS9TS+6yAK3NbvlcUw2sZh1onREsy8LIsMhJogJIiRnE4pugNDrPWPDX2mGIqgOSvdvJgDgwmRjwVR5wnBOpVBKxjVVsCWmkUiK0FHX346fQmxMBrl+qtycladNmJZ0zqK7m9wEb0UUsLa8hn5OUOegZ3dXA54XJEwHI4XqPS3bZGFgtNDiORUN9LUy2JiTXvyv5k+7T27v4NrNyZBqqEXmb+aY9yCHGaFQgvNuhRJXNks7/3iIhncOPULw8AHFx4Tz/RC7KT0sQsreT31XSIOIWPeWdnIvOlnWLSkVeb03d3o48hOJuW2mP1un8Fq3WF5xd3azoR1N7tSp6cCoRJ7WqQ64U8AdBsRooWsrLLQAAAABJRU5ErkJggg==",
            ["RGO"] = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAAXNSR0IArs4c6QAAAq1JREFUSEvN1e9LE3EcB/D3ft3d3I+7uR+ebsU2Q2fIBEN8EFg02GMfVfYkCh/UH1A+Cfr1SAt8FkhPRInSQCGDIiICkdGDykSK/LU5nblp+3nTm7vNuJGGYZ6dCd3T4/N+3ft7H+4UOORLccj5+D+AS5cvvi8IQmMmk54vQjFpNWufmc3Ogc7OzpTUCUg2aG+/cNXEVDww0gwsFjNoxlDKnJ0JIR5futnd3XNnL0QSaDl1MknTDG3Q61FXdxzVx5ygaQbh+QUEAgH09T3aM0MS8Pt9myRFIcfzcLvdsNsdYGga0WgUc8E5EITW1dvbG/pTi30BW8NOpxM2qw0imE4lEVlagkpV9PX3P30jC2htbWVsNnOC4zhkOA6VLLsDiMZiyOXW2gYGhp/IAsShc21nH4bnw+3iC7ZXVcFitmw3EAGOi3cMDb3okg38HFT4/b4iy7KosNlAkhRSqVV8j6chCNlbg4PPbx8UQFNTDW/QE6RSRezI0mqNPSMjb6/IBvx+r07IFzmXsxJlOj2MBgZGI410OoV0Jom1LId4kr83PDx6fTdEcovOnG54TDPE+XITA6vNimqXB3bHEQSD01hYDGEltoJIZCXy8tW4QxZworHmk6lc7y03aWDQG8GyjlKDRDJWCo8nklj+lkDg3cyuDyvZwOt1v9ZplT6dwQhCLUBv0EKtJrC+ziPH58BxecSTG5icnJUHNDfX3lWicENbRoEilSApsnQSYjifK0LIF8BlhYkPH6cbZB2ROOSpPbppYgiIyNYWFQsbWF/jkS8oQJKalrGxz6Oygfp6V0U2yy+ThAZqjWY7R8jnQZGq++MTwWuy13Rr0OOxm9Op3CpJ/QIoguz68jXUcaDP9e/Dbre9Rq1WNU9Nhfulfjbifckt2k/IP23wt+ChN/gB8fPyGYzN/NEAAAAASUVORK5CYII=",
            ["M67"] = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAAXNSR0IArs4c6QAAA2RJREFUSEu1lVlIFHEcx39z787sumqhZmUXlmyt5UWHSFZqRVZ0U0EFQRC9BD3UYz3VWz0IvRRRED2UFFQ+SNIWLqIiqRjahQcF5t7uzszOzM5O/MZWtjJ3U/q9zPn/fn7H9z9DwH8O4j/rwz8Bzl06vpCjSAvIABKtSndvPg6kSzAjwKlzR0/LktxUWJhv0zQV4locaIaGQCA0TFPUAZIkjQd3mvtngmUEOHlyd5Z3Ugy71jtNjbgaB5qlIRKahLGxb902Qbj95FHLvTkDcGF9Y82o07mmCM+xCozJqAghfwgMAs68bG67Py/Azn1bP5aUFBcnW4RikqKYAIKiVj5/3Do8L0B9Y3Vb/qKC7SiCooLd7iUIQvf5vCTL0GdbnrpfzAuw53D9ZSKRuMGwHMQ19dHz5lcnUHDvoYaSie8THZ3tvTlzBhw8tqsqKsp3BJtQalYQCA47eK782TN3CK/LKp1eC880dLzte/c7ZFYX7T/ScF3X9SvZC7L/SA7bJEbkkQTo98a/+a5xLP26v/eD2cLU+Cug8dCOJqvNciHH7gArz5u2TFoUj7IkTQ/ZNxEEWZaDfT2DuRkB9h6r22RoRgdmnmUTgGFYE8DbBHO9FBVBFiXTpooaAzkSg3A4AmpMr+n09LSnrWDbzuqHi4sKTqC4VeDN91Gc5y1TAClmQnDDoW29AT+EAxFQFOXBm9bO02kB1bUVn1cUL1/Fc9x0e1IBqRCsBAHjX72Q0I2wp7Y7F65CIgmZcQblVa6ewqV55Va7BTh2KuvUajDz5AzEiAyaqgDOAcNCJ3Lc7l7TXRgzAjZUrr1pdwgXHQ47oO8xaI6YhmHf44phCquaBrIUg5A/DIJNyAywuaZsq2EQ7oV5OSYAhZJWxbYFI+EpRykG+Lw+kKIS+H2h8YJFeQWUQSzzeHrGZq0AH5aWrW5mWPYgOojhaMBqkoFZT1kVhy3BZDjiY0iqIis3e5QyqHUeT/f79ICNpUuigeCQIPAClo6Q1NCUOIhREVQ1DiTNugb7BwcqNro+UIZ1S1dXlz8tAF9wOlcWKar2gmZZF/tzo7EcA6qimcKaqn5hbULdUO/QyC/0lIuMfjgl64objYR+Xtf1SsIAg6TIAYalbw30fZrxC5p2H/wtm7ncz6iCuQgn1/wAbKaNKNPdQxwAAAAASUVORK5CYII=",
            ["V40"] = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAAXNSR0IArs4c6QAAAv1JREFUSEvVlE1oE0EUx9+2ibub78SmSS1tWmuafrDdln5Y8GAoVAUpRfAg0pt49KBQ9ORBBC8KeupFimjValBqxFpaaiPYQ9WTiCQRMW2hSY1t0mxJdpNsVmbihlBS4wo9OJfZmdl5v/f/v5khYI8bscfx4a8AQ0PuqlxOfUKSgKEodY0kQZIgYFmr1X3W6VTvxsYexXZLtCzg2PGjDwUhc9ZgMABJktDY2AQ8nwKKonHP8zyI2ezt8fEHF0tBygJ6etgfFovFWm2rxvvNZgvuZQD6/hoMpvV67bDH453ZCfkjwO3uNCWTREF+vaO+CEDhWEhBLLYJIInnCYK+7/F40sWQsgp6e1l/OBJ1aTU0NLucYLfXQCqVLIBkgCiJI6SKDExOPvuoCIB+PuRqPFcJFXcdjnpwNDTgjGWrEGx1dRXUKtW4RqO54/XOfFIM6OpiLuVy4i2T2QR1dXUQDATBZrcV1Gxz3PWpqemrACApqsHgoHskm82M6vT6DoEX8F6k4P3SEiBYRweL1WSlzPDkxHOvolM0MHDkipiTbqCMaVqDfUd+I2uCwQCkkik43N+P51Jp/tTEvcdTigDd3czmgdpaMyoqRVH5QL+LGwp9h0QiAc3NLrwWiaxNe56+OKkI0NfXJRUfS3kzChgOr0FiK4HtQuOA3w+zswslT+Sux5Rl2zDAaDQWEkNWoYZ8LwYgRS+9M8oBqJDyE5G/vRSuRyQSxhbJ9VkOhZQraG11bun1OoPJZAKSInHmSA2qRTQaxWOr1Yqh65HIl7m5t+2KatDS5vSQavVp9MDRGhrIfSQYjAZsTTwex7GQhYIgQFZMj75+9eamIgDDMAfT6eS3fOYGkJWsLK8Azwt4Dl02ZJWqkrbPz8+vKwKgnxm2/TLHcReqq/bXIsD2Nuff2Ii1oDV7jQ3Xh+O4J76FxTOlgqO5so/dzo0Mw5hj8Y3NpqYGUKlVP8VMhdPn8+U9K9EUA1CMzk5mWKul2MXFD9d2CyzP/xOgXNDi9f8f8Atv2C4onSYQ5AAAAABJRU5ErkJggg==",
            ["VOG-17"] = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAAXNSR0IArs4c6QAAAfNJREFUSEu1081r02AcB/AnIXRNo512F8WXwzwXSg/tSCWD9KIRD60K6tmbJycMZrEgk+kuzoNXoRc3sIoo2EL+AZtAyJO7IvjCLtohtE/yLG0eeeIl9OAy+C2X55An30++z4uAjvgRDpN/xTCYLM+hN2/fpf4u9UT+I9evNRgfjxwYkyDb7/dpmvapGzSbzUVRYF94qCBKc91udx8UaDQaC/uU/uJ7cOz4vNzpdAJQgIfxTebjx14vdfPUEw3DOCUgtMsBaTI5/940v4M10HX9KiHkw9kzp+PMHz93UT6fXzRN8+tBSKoGlUplVRTFzYVCIc77PRwixtg9y7KegwDFYrGtKMqjXC4X5xFCUBiGa47jPIUCVhVF2UwCQRA8wRg/AAHK5fLdTCbzYgbYwBi3QIBqtbokCMKnJOD7/rrneW0QQFXVC1EUfZ5p0MIYb4AAmqadkyTpm5zNxnl+EKDRaHTftu1nIEC9Xr/EGOsnAULIymAw2AIBdF2/hRDaLpycR75P4wbj8XjNsiyYY6pp2m1Jkl5xgD/DvT98iVq2bcPswfLyxRuUTl4nb3IYho8dx3kIskSqqt6MomgneYoopW3XdddBgFqtdnk6nfZm7sGW53krIECpVDrBGNuTZfnfMfV9JIriHdd1X4IAB4X87/1f9yvgGar6hGsAAAAASUVORK5CYII=",
            ["VOG-25"] = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAAXNSR0IArs4c6QAAAklJREFUSEtjZKAxYCTF/KysUB6Q+mnTVn8hVh/RFiQkJEj8+/3t2d9//68uXb5al+oWZGUlyrIzsz369u0bw8y5i4l2GNEKi9PSRL78/f4a5HKaWFCam6bMwsZ6592HTwyMjMyOM+YsOEBMMBHtg9LSLAmWf4zPf/z6w/CHgY1j8uTJP6lqAciw9OTY/zQLoqKCzKKvn7/0vn3/kWHNuk1E+5wohUX5GT7sLMybQcHz7MXLhytXb1AgJnhAaoiyIDE+6pyctIThi5evGUA+YGJimsbOydqzePHq+4QsIsqC6MiQ1yqKciK/fv5kAKWi7z9+Mrz/8Gn/5q07nCi2ICnJj/fPT45PivIyDMgWvHv/geHXz18pu/YemIvPEoI+SE6ICmVhZl4lJMDHAIqDt+/eM3z7/oPhx48fDJ8/f5t98PCRNIosiIkMLWJiYuyVlhAFBw8oDkAAZMG79x+XHzt+MooiC8JCAioF+XnbQD6AhT/MB6/fvNtw8tSZQAotCCwX5OfpgFkA8gHI9V+/fWd4+/b92UuXr5hQZEFEWGAJPy9PN8iCpy9eM4AiF2Q4CLx69ebt1WvXRSiyIDoyLIaHi30xNh88e/aC4eat23gTCsFUlBQXafnn799joGQKy2iwIKKKBXFxEYYMf/+dg1nw9PlLeBC9e/f+9sVLV9QoCiKQ5pjo0OeiQkISoDzw+Olzhs+fv4LNZGVhSj5+8sw8ii1Ii4mR/MfK4PHtxw+HFy9fc3z58pWPlYlp49ETp2ZQXFQQMoCQPAC8WjIofSyc1AAAAABJRU5ErkJggg==",
            //smoke
            ["M18"] = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAAXNSR0IArs4c6QAAArNJREFUSEu11V1IU2EYAOD3zJ39u+24dTwr84eIcg5paqAFsRW1GHQTGt5EXUZBN9110w90F0FQdFuZN0ERgdjRbhRyrv15UzITyTbUrW3nnP25nf0U3wlDDV1+4nd7+N7ne7/3e99DwB4vYo/jw46AgX7HAs00taFDrSz9iLx5O9lc64A7AgYHzz4zmRuvoaCx5ciiWqPsGBoay22H7BA4/8RkNt8oFfPwbT6yoFDIHrGs9+mugYGLjn6xIjoNBsP1BhMDqeQKRKNLIAgl0GoV9yYnZ+5uhdTM4IK792ausPrY3GAAlUYrxSnkcyCKIhTFKghCdmLK89WBDfT1Wl+rNeTAGqBSqiGbTUM+l5UAXsj4PZ7Z49iAvfPQB8qscyHASJn/ZFBchTTPSQCXyoSmP892YQM2W6uHpg29TU37oV5vkuKgIvNcAjLZAqQ4YcbrDduxAWt7S4CxGLuYxn1AmZh/gST/xeubs+0GCDIWox1lgF4QWvmsINWB49KQSAhhnz98FB840hxiDlDH1gOlUgm45IoExOP8YiA414oNtLe3+CwWY8/aFZEkuSGDeCwVDYTmD2IDVmvrFMMY+taKvBmIxVKRYGh+y5lUs9FstrYJmtaf0uvUoDdSsLkPEgk+7PPP4degu+twkKYpO0XppVeEMkA1yKST0q3EYvwoy3rc2FfkdPZ8tDDGMyh4I9MMSpUaioVVSCWXpZg/49Gx4eFxFzZw2mF/qFSRt+p1KqmTSaVGajT0TMslEUSx8urd+0+XsYGTJzpYpUp+bv0sQqPi78Arll+Osv4r2IDT2XmbIIgHqMgarQ7kJLlhmlYrlfvseOAONuB221sKher3rQBZnezqyMj0C2wAbXS5ui11ILsEMrmbkNUR5XJFC9VfolwBz7cLjvbW7IPtfof/8+03NPBGKKSOvz0AAAAASUVORK5CYII=",
            ["RDG-2B"] = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAAXNSR0IArs4c6QAAAZ5JREFUSEtjZKAxYCTW/CQ/481/fv/yYWJi/PH19/9bGgriehee/P6+edtBLnxmEG1BnKfufzUFMbBZj5+9ZWBhY2d4/pPr1bpN+8WpZoG8LMSsVy/fMMzceIEoxxGlCGQoyAfSEkIM3BwsYB/QxAKQD/h5ORnuP3rFMHX1SaIcR5QidB88f/OFQVT7JHNDA8M/QomEZAt4ebkZnj5/S30fpPsb/JeXlwI7+M27Twx8Kkeo64PsUPP/0pLCtLWAnZ2VgZ2VmeHn77+08QHIAhigSRBJivAw/PzLyPD12w+G/+x/hfvnHn9HtVQEi4PvP36BLWBl/C/fufjoI6pb8O/3T4Z3n38yMLOwSHYvOPCC6hawsTIzvH33iYH9P6Ngw4IDH6hmASwfgCz4+Pk7w88/3/m65h39TFULZKWEGTi4uMAW/GJgEOictecjzSxg/vWH+kEkJi7CwMnBxvD581eG9vkHiSrHiFIEK015ebgYPn/5xiDAxztt0spj2YSCByRPtAWgSP7FyLRu/oZzwcQYDFNDtAWkGIqsluYWAAB8o7oZWYom9AAAAABJRU5ErkJggg==",
            //flashbang
            ["M7290"] = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAAXNSR0IArs4c6QAAAiBJREFUSEu1VTFr20AY/WQheYgokpAjR3G2RsFbQ43dBBcKpjVCBAolW8liKPGP6FK69QcE0y106k9onaz1EBPPhjSlsU2J7JOaSrIs2ajcgUvJYKWVfOvdvXfv+977joIlL+pf8Gu1mjgej98gNHoRBMGvk5NTNer+nQjq9brAMDRiGBaCwCeYlmXB8fGHyPuRB+YvPDx8FcqyDKZpgu/7sLLCgW07u41G48siFXcmODh4GeZyOej1euA4LmQyElBU+PTo6H0zFsH+/vOHNzfuJ47jxO3tB3B+3gGEEMGk6dTbZvP0dSyCQqFgrq1leZZlyasNY0gIPM+D0QhBt9tdWIWFm5qmpS3L9BRFIeCiKBLwfn8Aw+EwPkG1WhV9fzLiOA7W1xWYN/ny8lsyBJVKRXZd54ckSYQgcQWa9jjjefT1xkYOeJ4HrASXyHVduLrqkVLF6sHe3hPJtsFYGoGu64LrOggTZDKrkE6zMJn4YBjXySjQ9bKA0BTNXYTT6zg2sepgMIjvonK5LMxmU4SbLAg86QMeE4nZtFQq3UulqJ9LUzAP2ubm/T8usm2bKMAl8v3gc7vdfvbfo+J2kv/uwcXFV+h0OpHDMvLAzs6j8HaJsAKcg8QI5kleigJVVcOtLZW4CGcB2zRRBfl8vp/NygoeE5gEfzZ4XJimlUyJsEOKxeJ3AGBkeTWLx/R0Ojujafpdq9X6mMinHwWyaP83dqpaKDN2Q/wAAAAASUVORK5CYII=",
            ["Zarya"] = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAAXNSR0IArs4c6QAAAqdJREFUSEtjZKAxYCTV/J6uzj0lZeUuxOojyYLly5eWKCkqdr949ETDPyzsJjGWkGRBSXHB4cioaJv3b97ku7h7TqK6Benpqa/T0tJE7t271xYaGl5NdQvS0lLepqenC12/fr0rJiaunOoWJCclPsrKzpK9dOni2sTElBCqW5Cfn/s/KjKS4fyF86syMrLDqW5BenrqfxdnZ4ZTp04xdPf0EZVAiFIEcmlFRYXg3z+/3qmrqzOcPnOGQUBASKCzs/MjIV8QbUFubi47BzvrDxkZGYZr168zcHH9E+7vn/uOahY0FBYKPf/25a2KsjLD2bPnGPj4BURnzZr1hmoW5GZlRX76+nkZBwcHw6uXrxg4ODkKly9fOYFiCxITI2Tfv/22lpGJ0ZSPnw9u3s8fPxn+/f83adWqNfn4LCEYB0EBfnd5+fmVxERFGdjZ2RlAPgCBjx8/Mrx6/Zrh14+fE5avXFWIyxK8FsTERKZzc/PMEBURYeDn52cQFBQE0yDDX7x4Aabv3LnLwMLKJrB69WqsKQqnBXFxUf6fP33dICYuxsDHyws2WEJCAuzQr1+/Mrx+/Zrh58+fYF98+/btIgsLm8vy5csxIh2nBcFBgft4eHkcQQaCggWEeXl4wBaADP71+zeY/ePHDzD969cPj/nzF+9EDyqcFsRGRxdw8XD1gwzAZgHY0N+/4RYwM7PKTZs27THRFoAUhoaE7Prz57crKHLZOSARDMIwV4PoTx8/MfDy8FYvWrKkDVtEE0xFFuZmj3l5eWV4eHjAqQgWRCD6y5cvDNw83FkbNmyaTlYqAmlyd7eV/Prl76b//xlMuLg44eZ8+/adgZ+fd+K27TsLKMoHMM1uTk52/xgYjJhZWLQYmf4fY/r1d/+2ffseUpyTCRlASB4Aqvv/GUDzkwMAAAAASUVORK5CYII=",
            //flare
            ["White"] = null,
            ["Red"] = null,
            ["Green"] = null,
            ["Blue"] = null,
            ["S-Yellow"] = null,
            ["Yellow"] = null,
            ["Firework"] = null,
        }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);
public static IReadOnlyDictionary<int, int> XPTable { get; } = new Dictionary<int, int>
        {
            {0, 1},
            {1000, 2},
            {4017, 3},
            {8432, 4},
            {14256, 5},
            {21477, 6},
            {30023, 7},
            {39936, 8},
            {51204, 9},
            {63723, 10},
            {77563, 11},
            {92713, 12},
            {111881, 13},
            {134674, 14},
            {161139, 15},
            {191417, 16},
            {225194, 17},
            {262366, 18},
            {302484, 19},
            {345751, 20},
            {391649, 21},
            {440444, 22},
            {492366, 23},
            {547896, 24},
            {609066, 25},
            {679255, 26},
            {755444, 27},
            {837672, 28},
            {925976, 29},
            {1020396, 30},
            {1120969, 31},
            {1227735, 32},
            {1344260, 33},
            {1470605, 34},
            {1606833, 35},
            {1759965, 36},
            {1923579, 37},
            {2097740, 38},
            {2282513, 39},
            {2477961, 40},
            {2684149, 41},
            {2901143, 42},
            {3132824, 43},
            {3379281, 44},
            {3640603, 45},
            {3929436, 46},
            {4233995, 47},
            {4554372, 48},
            {4890662, 49},
            {5242956, 50},
            {5611348, 51},
            {5995931, 52},
            {6402287, 53},
            {6830542, 54},
            {7280825, 55},
            {7753260, 56},
            {8247975, 57},
            {8765097, 58},
            {9304752, 59},
            {9876880, 60},
            {10512365, 61},
            {11193911, 62},
            {11929835, 63},
            {12727177, 64},
            {13615989, 65},
            {14626588, 66},
            {15864243, 67},
            {17555001, 68},
            {19926895, 69},
            {22926895, 70},
            {26526895, 71},
            {30726895, 72},
            {35526895, 73},
            {40926895, 74},
            {46926895, 75},
            {53526895, 76},
            {60726895, 77},
            {69126895, 78},
            {81126895, 79}
        };
    }
}
