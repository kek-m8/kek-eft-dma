namespace SDK
{
    public readonly partial struct ClassNames
    {
        public readonly partial struct NoMalfunctions
        {
            public const uint ClassName_ClassToken = 0x2001BBB; // MDToken
            public const uint GetMalfunctionState_MethodToken = 0x600ABD8; // MDToken
            public const string ClassName = @"EFT.Player+FirearmController";
            public const string GetMalfunctionState = @"GetMalfunctionState";
        }

        public readonly partial struct FirearmController
        {
            public const uint ClassName_ClassToken = 0x2001BBB; // MDToken
            public const string ClassName = @"EFT.Player+FirearmController";
        }

        public readonly partial struct OpticCameraManagerContainer
        {
            public const uint ClassName_ClassToken = 0x200355A; // MDToken
            public const string ClassName = @"\uF221";
        }

        public readonly partial struct ProceduralWeaponAnimation
        {
            public const uint ClassName_ClassToken = 0x200288E; // MDToken
            public const uint MethodName_MethodToken = 0x600F8F0; // MDToken
            public const string ClassName = @"EFT.Animations.ProceduralWeaponAnimation";
            public const string MethodName = @"get_ShotNeedsFovAdjustments";
        }
    }

    public readonly partial struct Offsets
    {
        public readonly partial struct TarkovApplication
        {
            public const uint GameOperationSubclass = 0xF0; // -.\uEA72
        }

        public readonly partial struct GameWorld
        {
            public const uint Location = 0x90; // String
        }

        public readonly partial struct ClientLocalGameWorld
        {
            public const uint LocationId = 0x90; // String
            public const uint RegisteredPlayers = 0x140; // System.Collections.Generic.List<IPlayer>
            public const uint MainPlayer = 0x1B0; // EFT.Player
            public const uint Grenades = 0x210; // -.\uE3E3<Int32, Throwable>
            public const uint IsInRaid = 0x290; // [HUMAN] Bool
        }

        public readonly partial struct Grenade
        {
            public const uint IsDestroyed = 0x5D; // Boolean
            public const uint WeaponSource = 0x80; // -.\uF0CA
        }

        public readonly partial struct Player
        {
            public const uint MovementContext = 0x58; // EFT.MovementContext
            public const uint _playerBody = 0xC0; // EFT.PlayerBody
            public const uint ProceduralWeaponAnimation = 0x1E8; // EFT.Animations.ProceduralWeaponAnimation
            public const uint Corpse = 0x3E8; // EFT.Interactive.Corpse
            public const uint Profile = 0x620; // EFT.Profile
            public const uint _inventoryController = 0x680; // -.Player.PlayerInventoryController
            public const uint _handsController = 0x688; // -.Player.AbstractHandsController
        }

        public readonly partial struct ObservedPlayerView
        {
            public const uint NickName = 0x50; // String
            public const uint AccountId = 0x58; // String
            public const uint PlayerBody = 0x68; // EFT.PlayerBody
            public const uint ObservedPlayerController = 0x88; // -.\uED7E
            public const uint Side = 0x108; // System.Int32
            public const uint IsAI = 0x119; // Boolean
            public const uint VisibleToCameraType = 0x120; // System.Int32
        }

        public readonly partial struct ObservedPlayerController
        {
            public static readonly uint[] MovementController = new uint[] { 0x100, 0x10 }; // -.\uEDA1, -.\uEDA3
            public const uint HandsController = 0x110; // -.\uED8C
            public const uint HealthController = 0x128; // -.\uE454
            public const uint InventoryController = 0x150; // -.\uED74
        }

        public readonly partial struct ObservedMovementController
        {
            public const uint Rotation = 0x88; // UnityEngine.Vector2
            public const uint Velocity = 0x120; // UnityEngine.Vector3
            public const uint PreviousStateName = 0xD8; // System.Byte
            public const uint CurrentStateName = 0xD9; // System.Byte
        }

        public readonly partial struct ObservedHandsController
        {
            public const uint ItemInHands = 0x58; // EFT.InventoryLogic.Item
        }

        public readonly partial struct ObservedHealthController
        {
            public const uint PlayerCorpse = 0x18; // EFT.Interactive.ObservedCorpse
            public const uint HealthStatus = 0xE0; // System.Int32
            public const uint BodyState = 0xD8; // System.Collections.Generic.Dictionary<Int32, BodyPartState>
        }

        public readonly partial struct ProceduralWeaponAnimation
        {
            public const uint HandsContainer = 0x20; // EFT.Animations.PlayerSpring
            public const uint Breath = 0x30; // EFT.Animations.BreathEffector
            public const uint MotionReact = 0x40; // -.MotionEffector
            public const uint Shootingg = 0x50; // -.ShotEffector
            public const uint _optics = 0xC8; // System.Collections.Generic.List<SightNBone>
            public const uint Mask = 0x158; // System.Int32
            public const uint _isAiming = 0x1DD; // Boolean
            public const uint ShotNeedsFovAdjustments = 0x427; // Boolean
        }

        public readonly partial struct SightNBone
        {
            public const uint Mod = 0x10; // EFT.InventoryLogic.SightComponent
        }

        public readonly partial struct BreathEffector
        {
            public const uint Intensity = 0xA4; // Single
        }

        public readonly partial struct ShotEffector
        {
            public const uint NewShotRecoil = 0x18; // EFT.Animations.NewRecoil.NewRecoilShotEffect
        }

        public readonly partial struct NewShotRecoil
        {
            public const uint IntensitySeparateFactors = 0x94; // UnityEngine.Vector3
        }

        public readonly partial struct VisorEffect
        {
            public const uint Intensity = 0xC8; // Single
        }

        public readonly partial struct Profile
        {
            public const uint Id = 0x10; // String
            public const uint AccountId = 0x18; // String
            public const uint Info = 0x40; // -.\uE8AE
            public const uint QuestsData = 0x88; // System.Collections.Generic.List<\uEF71>
        }

        public readonly partial struct QuestData
        {
            public const uint Id = 0x10; // String
            public const uint StatusStartTimestamps = 0x18; // System.Collections.Generic.Dictionary<Int32, Double>
            public const uint CompletedConditions = 0x20; // System.Collections.Generic.HashSet<MongoID>
            public const uint Template = 0x28; // -.\uEF72
            public const uint StartTime = 0x30; // Int32
            public const uint Status = 0x34; // System.Int32
            public const uint AvailableAfter = 0x38; // System.Int32
        }

        public readonly partial struct QuestTemplate
        {
            public const uint Id = 0x10; // String
            public const uint LocationId = 0x18; // String
            public const uint TraderId = 0x20; // String
            public const uint Image = 0x28; // String
            public const uint TemplateId = 0x30; // String
            public const uint Rewards = 0x38; // System.Collections.Generic.Dictionary<Int32, IReadOnlyList<\uEF6E>>
            public const uint Conditions = 0x40; // EFT.Quests.ConditionsDict
            public const uint Sprite = 0x48; // UnityEngine.Sprite
            public const uint Name = 0x50; // String
            public const uint QuestSuccessMessageKey = 0x58; // String
            public const uint ChangeQuestText = 0x60; // String
            public const uint Description = 0x68; // String
            public const uint AcceptPlayerMessageKey = 0x70; // String
            public const uint DeclinePlayerMessageKey = 0x78; // String
            public const uint CompletePlayerMessageKey = 0x80; // String
            public const uint rankingModes = 0x88; // System.String[]
            public const uint gameModes = 0x90; // System.String[]
            public const uint arenaLocationIds = 0x98; // System.String[]
            public const uint Level = 0xA0; // System.Int32
            public const uint Restartable = 0xA4; // System.Boolean
            public const uint QuestType = 0xA8; // System.Int32
            public const uint QuestStatus = 0xAC; // System.Int32
            public const uint KeyQuest = 0xB0; // Boolean
            public const uint CanShowNotificationsInGame = 0xB1; // Boolean
            public const uint InstantComplete = 0xB2; // Boolean
            public const uint PlayerGroup = 0xB4; // System.Int32
            public const uint ServerOnly = 0xB8; // Boolean
            public const uint acceptanceAndFinishingSource = 0xBC; // System.Int32
            public const uint progressSource = 0xC0; // System.Int32
            public const uint theme = 0xC4; // System.Int32
        }

        public readonly partial struct QuestConditionsContainer
        {
            public const uint ConditionsList = 0x50; // System.Collections.Generic.List<Var>
        }

        public readonly partial struct QuestCondition
        {
            public const uint id = 0x10; // EFT.MongoID
        }

        public readonly partial struct QuestConditionArenaPlayerAction
        {
            public const uint actions = 0x70; // System.String[]
        }

        public readonly partial struct QuestConditionPlayerCurrentAction
        {
            public const uint target = 0x70; // System.Int32
            public const uint action = 0x74; // System.Int32
        }


        public readonly partial struct PlayerInfo
        {
            public const uint Nickname = 0x20; // String
            public const uint Settings = 0x60; // -.\uE9F0
            public const uint Side = 0xA8; // [HUMAN] Int32
            public const uint RegistrationDate = 0xAC; // Int32
            public const uint MemberCategory = 0xB8; // System.Int32
            public const uint Experience = 0xBC; // Int32
        }

        public readonly partial struct PlayerInfoSettings
        {
            public const uint Role = 0x10; // System.Int32
        }

        public readonly partial struct ItemHandsController
        {
            public const uint Item = 0x68; // EFT.InventoryLogic.Item
        }

        public readonly partial struct FirearmController
        {
            public const uint Fireport = 0xD0; // EFT.BifacialTransform
            public const uint TotalCenterOfImpact = 0x1A0; // Single
        }

        public readonly partial struct ClientFirearmController
        {
            public const uint ShotIndex = 0x420; // SByte
        }

        public readonly partial struct MovementContext
        {
            public const uint _rotation = 0x27C; // UnityEngine.Vector2
        }

        public readonly partial struct InventoryController
        {
            public const uint Inventory = 0x130; // EFT.InventoryLogic.Inventory
        }

        public readonly partial struct Inventory
        {
            public const uint Equipment = 0x10; // EFT.InventoryLogic.InventoryEquipment
        }

        public readonly partial struct Equipment
        {
            public const uint Grids = 0x90; // -.\uEFBB[]
            public const uint Slots = 0x98; // EFT.InventoryLogic.Slot[]
        }

        public readonly partial struct Slot
        {
            public const uint ContainedItem = 0x48; // EFT.InventoryLogic.Item
            public const uint ID = 0x58; // String
        }

        public readonly partial struct InteractiveLootItem
        {
            public const uint Item = 0xB8; // EFT.InventoryLogic.Item
        }

        public readonly partial struct InteractiveCorpse
        {
            public const uint PlayerBody = 0x138; // EFT.PlayerBody
        }

        public readonly partial struct DizSkinningSkeleton
        {
            public const uint _values = 0x30; // System.Collections.Generic.List<Transform>
        }

        public readonly partial struct LootableContainer
        {
            public const uint InteractingPlayer = 0xC0; // EFT.IPlayer
            public const uint ItemOwner = 0x130; // -.\uEF97
            public const uint Template = 0x138; // String
        }

        public readonly partial struct LootableContainerItemOwner
        {
            public const uint RootItem = 0xD0; // EFT.InventoryLogic.Item
        }

        public readonly partial struct LootItem
        {
            public const uint Template = 0x58; // EFT.InventoryLogic.ItemTemplate
            public const uint StackObjectsCount = 0x7C; // Int32
            public const uint Version = 0x80; // Int32
        }

        public readonly partial struct LootItemMod
        {
            public const uint Grids = 0x90; // -.\uEFBB[]
            public const uint Slots = 0x98; // EFT.InventoryLogic.Slot[]
        }

        public readonly partial struct LootItemModGrids
        {
            public const uint ItemCollection = 0x48; // -.\uEFBD
        }

        public readonly partial struct LootItemModGridsItemCollection
        {
            public const uint List = 0x18; // System.Collections.Generic.List<Item>
        }

        public readonly partial struct LootItemWeapon
        {
            public const uint FireMode = 0xB8; // EFT.InventoryLogic.FireModeComponent
            public const uint Chambers = 0xD0; // EFT.InventoryLogic.Slot[]
            public const uint _magSlotCache = 0xF0; // EFT.InventoryLogic.Slot
        }

        public readonly partial struct FireModeComponent
        {
            public const uint FireMode = 0x28; // System.Byte
        }

        public readonly partial struct LootItemMagazine
        {
            public const uint Cartridges = 0xC0; // EFT.InventoryLogic.StackSlot
        }

        public readonly partial struct MagazineClass
        {
            public const uint StackObjectsCount = 0x7C; // Int32
        }

        public readonly partial struct StackSlot
        {
            public const uint _items = 0x28; // System.Collections.Generic.List<Item>
            public const uint MaxCount = 0x50; // Int32
        }

        public readonly partial struct ItemTemplate
        {
            public const uint ShortName = 0x18; // String
            public const uint _id = 0x68; // EFT.MongoID
        }

        public readonly partial struct ModTemplate
        {
            public const uint Velocity = 0x190; // Single
        }

        public readonly partial struct AmmoTemplate
        {
            public const uint InitialSpeed = 0x1F0; // Single
            public const uint BallisticCoeficient = 0x204; // Single
            public const uint BulletMassGram = 0x28C; // Single
            public const uint BulletDiameterMilimeters = 0x290; // Single
        }

        public readonly partial struct WeaponTemplate
        {
            public const uint Velocity = 0x27C; // Single
        }

        public readonly partial struct PlayerBody
        {
            public const uint SkeletonRootJoint = 0x30; // Diz.Skinning.Skeleton
            public const uint BodySkins = 0x48; // System.Collections.Generic.Dictionary<Int32, LoddedSkin>
            public const uint _bodyRenderers = 0x58; // -.\uE453[]
            public const uint SlotViews = 0x70; // -.\uE3E3<Int32, \uE001>
        }

        public readonly partial struct PlayerBodySubclass
        {
            public const uint Dresses = 0x40; // EFT.Visual.Dress[]
        }

        public readonly partial struct Dress
        {
            public const uint Renderers = 0x30; // UnityEngine.Renderer[]
        }

        public readonly partial struct Skeleton
        {
            public const uint _values = 0x30; // System.Collections.Generic.List<Transform>
        }

        public readonly partial struct LoddedSkin
        {
            public const uint _lods = 0x20; // Diz.Skinning.AbstractSkin[]
        }

        public readonly partial struct Skin
        {
            public const uint _skinnedMeshRenderer = 0x28; // UnityEngine.SkinnedMeshRenderer
        }

        public readonly partial struct TorsoSkin
        {
            public const uint _skin = 0x28; // Diz.Skinning.Skin
        }

        public readonly partial struct SlotViewsContainer
        {
            public const uint Dict = 0x10; // System.Collections.Generic.Dictionary<Var, Var>
        }

        public readonly partial struct OpticCameraManagerContainer
        {
            public const uint Instance = 0x0; // -.\uF221
            public const uint OpticCameraManager = 0x10; // -.\uF225
            public const uint FPSCamera = 0x68; // UnityEngine.Camera
        }

        public readonly partial struct OpticCameraManager
        {
            public const uint Camera = 0x68; // UnityEngine.Camera
            public const uint CurrentOpticSight = 0x70; // EFT.CameraControl.OpticSight
        }

        public readonly partial struct OpticSight
        {
            public const uint LensRenderer = 0x20; // UnityEngine.Renderer
        }

        public readonly partial struct SightComponent
        {
            public const uint _template = 0x20; // -.\uEFB3
            public const uint ScopesSelectedModes = 0x30; // System.Int32[]
            public const uint SelectedScope = 0x38; // Int32
        }

        public readonly partial struct SightInterface
        {
            public const uint Zooms = 0x1B8; // System.Single[]
        }

        public readonly partial struct NetworkGame
        {
            public const uint NetworkGameData = 0x70; // -.\uE9EF
        }

        public readonly partial struct NetworkGameData
        {
            public const uint raidMode = 0x4C; // System.Int32
        }
    }

    public readonly partial struct Enums
    {

        public enum ETargetPlayer : int
        {
            // Token: 0x0400F97B RID: 63867
            Player,
            // Token: 0x0400F97C RID: 63868
            Enemy,
            // Token: 0x0400F97D RID: 63869
            Bot
        }

        public enum EActionPlayer : int
        {
            // Token: 0x0400F947 RID: 63815
            StandOnPoint,
            // Token: 0x0400F948 RID: 63816
            PointCapturing,
            // Token: 0x0400F949 RID: 63817
            BombActivating,
            // Token: 0x0400F94A RID: 63818
            BombDeactivating,
            // Token: 0x0400F94B RID: 63819
            WithBomb,
            // Token: 0x0400F94C RID: 63820
            ReloadingMagazine,
            // Token: 0x0400F94D RID: 63821
            InJump,
            // Token: 0x0400F94E RID: 63822
            IsAI
        }

        public enum EArenaQuestThemeByEvent // Offsets.Profile.QuestTemplate.theme
        {
            // Token: 0x0400F81F RID: 63519
            //[\uE893("none")]
		    None,
		    // Token: 0x0400F820 RID: 63520
		    //[\uE893("new_year")]
		    NewYear,
		    // Token: 0x0400F821 RID: 63521
		    //[\uE893("free_weekend")]
		    FreeWeekend,
		    // Token: 0x0400F822 RID: 63522
		    //[\uE893("battle_pass_season_0")]
		    BattlePassSeason0,
		    // Token: 0x0400F823 RID: 63523
		    //[\uE893("twitch_rivals")]
		    TwitchRivals,
		    // Token: 0x0400F824 RID: 63524
		    //[\uE893("acs3")]
		    ACS3
        }

    public enum EProfileType // Offsets.Profile.QuestTemplate.acceptanceAndFinishingSource & progressSource
        {
            // Token: 0x04003310 RID: 13072
            All = -1,
            // Token: 0x04003311 RID: 13073
            Eft,
            // Token: 0x04003312 RID: 13074
            Arena
        }

        public enum EPlayerGroup
        {
            // Token: 0x0400A70D RID: 42765
            Pmc,
            // Token: 0x0400A70E RID: 42766
            Scav
        }

        public enum EQuestStatus
        {
            // Token: 0x0400FA38 RID: 64056
            Locked,
            // Token: 0x0400FA39 RID: 64057
            AvailableForStart,
            // Token: 0x0400FA3A RID: 64058
            Started,
            // Token: 0x0400FA3B RID: 64059
            AvailableForFinish,
            // Token: 0x0400FA3C RID: 64060
            Success,
            // Token: 0x0400FA3D RID: 64061
            Fail,
            // Token: 0x0400FA3E RID: 64062
            FailRestartable,
            // Token: 0x0400FA3F RID: 64063
            MarkedAsFailed,
            // Token: 0x0400FA40 RID: 64064
            Expired,
            // Token: 0x0400FA41 RID: 64065
            AvailableAfter
        }
        public enum EQuestType
        {
            // Token: 0x0400FA70 RID: 64112
            // [\uE893("PickUp")]
		    PickUp,
		    // Token: 0x0400FA71 RID: 64113
		    //[\uE893("Elimination")]
		    Elimination,
		    // Token: 0x0400FA72 RID: 64114
		    //[\uE893("Discover")]
		    Discover,
		    // Token: 0x0400FA73 RID: 64115
		    //[\uE893("Completion")]
		    Completion,
		    // Token: 0x0400FA74 RID: 64116
		    //[\uE893("Exploration")]
		    Exploration,
		    // Token: 0x0400FA75 RID: 64117
		    //[\uE893("Levelling")]
		    Levelling,
		    // Token: 0x0400FA76 RID: 64118
		    //[\uE893("Experience")]
		    Experience,
		    // Token: 0x0400FA77 RID: 64119
		    //[\uE893("Standing")]
		    Standing,
		    // Token: 0x0400FA78 RID: 64120
		    //[\uE893("Loyalty")]
		    Loyalty,
		    // Token: 0x0400FA79 RID: 64121
		    //[\uE893("Merchant")]
		    Merchant,
		    // Token: 0x0400FA7A RID: 64122
		    //[\uE893("Skill")]
		    Skill,
		    // Token: 0x0400FA7B RID: 64123
		    //[\uE893("Multi")]
		    Multi,
		    // Token: 0x0400FA7C RID: 64124
		    //[\uE893("WeaponAssembly")]
		    WeaponAssembly,
		    // Token: 0x0400FA7D RID: 64125
		    //[\uE893("ArenaWinMatch")]
		    ArenaWinMatch,
		    // Token: 0x0400FA7E RID: 64126
		    //[\uE893("ArenaWinRound")]
		    ArenaWinRound,
		    // Token: 0x0400FA7F RID: 64127
		    //[\uE893("ArenaAction")]
		    ArenaAction
        }

    public enum EBodyPart : ulong
        {
            // Token: 0x040031C0 RID: 12736
            Head,
            // Token: 0x040031C1 RID: 12737
            Chest,
            // Token: 0x040031C2 RID: 12738
            Stomach,
            // Token: 0x040031C3 RID: 12739
            LeftArm,
            // Token: 0x040031C4 RID: 12740
            RightArm,
            // Token: 0x040031C5 RID: 12741
            LeftLeg,
            // Token: 0x040031C6 RID: 12742
            RightLeg,
            // Token: 0x040031C7 RID: 12743
            Common
        }
        public enum EPlayerState : byte
        {
            None,
            Idle,
            ProneIdle,
            ProneMove,
            Run,
            Sprint,
            Jump,
            FallDown,
            Transition,
            BreachDoor,
            Loot,
            Pickup,
            Open,
            Close,
            Unlock,
            Sidestep,
            DoorInteraction,
            Approach,
            Prone2Stand,
            Transit2Prone,
            Plant,
            Stationary,
            Roll,
            JumpLanding,
            ClimbOver,
            ClimbUp,
            VaultingFallDown,
            VaultingLanding,
            BlindFire,
            IdleWeaponMounting,
            IdleZombieState,
            MoveZombieState,
            TurnZombieState,
            StartMoveZombieState,
            EndMoveZombieState,
            DoorInteractionZombieState
        }

        public enum ERaidMode
        {
            Online = 0,
            Local = 1,
            Coop = 2,
            OverRun = 3,
            TeamFight = 4,
            LastHero = 5,
            FinalRun = 6,
            OneManArmy = 7,
            Duel = 8,
            ShootOut = 9,
            ShootOutSolo = 10,
            ShootOutDuo = 11,
            ShootOutTrio = 12,
            BlastGang = 13,
            CheckPoint = 14,
        }

        public enum EMalfunctionState
        {
            None = 0,
            Misfire = 1,
            Jam = 2,
            HardSlide = 3,
            SoftSlide = 4,
            Feed = 5,
        }

        [Flags]
        public enum EProceduralAnimationMask
        {
            Breathing = 1,
            Walking = 2,
            MotionReaction = 4,
            ForceReaction = 8,
            Shooting = 16,
            DrawDown = 32,
            Aiming = 64,
            HandShake = 128,
        }

        public enum EFireMode
        {
            fullauto = 0,
            single = 1,
            doublet = 2,
            burst = 3,
            doubleaction = 4,
            semiauto = 5,
            grenadeThrowing = 6,
            greanadePlanting = 7,
        }

        public enum ArmbandColorType
        {
            red = 1,
            fuchsia = 2,
            yellow = 3,
            green = 4,
            azure = 5,
            white = 6,
            blue = 7,
            grey = 8,
        }

        public enum ECameraType
        {
            Default = 0,
            Spectator = 1,
            UIBackground = 2,
            KillCamera = 3,
        }
    }
}
