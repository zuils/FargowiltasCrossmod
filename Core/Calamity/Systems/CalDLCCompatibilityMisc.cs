using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Enums;
using CalamityMod.Events;
using CalamityMod.Items.Tools;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.NPCs;
using CalamityMod.NPCs.AquaticScourge;
using CalamityMod.NPCs.AstrumAureus;
using CalamityMod.NPCs.AstrumDeus;
using CalamityMod.NPCs.BrimstoneElemental;
using CalamityMod.NPCs.Bumblebirb;
using CalamityMod.NPCs.CalClone;
using CalamityMod.NPCs.CeaselessVoid;
using CalamityMod.NPCs.Crabulon;
using CalamityMod.NPCs.Cryogen;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.ExoMechs;
using CalamityMod.NPCs.ExoMechs.Apollo;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Artemis;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalamityMod.NPCs.HiveMind;
using CalamityMod.NPCs.Leviathan;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.OldDuke;
using CalamityMod.NPCs.Perforator;
using CalamityMod.NPCs.PlaguebringerGoliath;
using CalamityMod.NPCs.Polterghast;
using CalamityMod.NPCs.ProfanedGuardians;
using CalamityMod.NPCs.Providence;
using CalamityMod.NPCs.Ravager;
using CalamityMod.NPCs.Signus;
using CalamityMod.NPCs.SlimeGod;
using CalamityMod.NPCs.StormWeaver;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.NPCs.TownNPCs;
using CalamityMod.NPCs.Yharon;
using CalamityMod.Systems.Collections;
using CalamityMod.Projectiles;
using CalamityMod.Systems;
using CalamityMod.UI.DraedonSummoning;
using CalamityMod.World;
using Fargowiltas.Items.CaughtNPCs;
using Fargowiltas.NPCs;
using FargowiltasCrossmod.Content.Calamity.Bosses.Crabulon;
using FargowiltasCrossmod.Content.Calamity.Bosses.Cryogen;
using FargowiltasCrossmod.Content.Calamity.Bosses.Perforators;
using FargowiltasCrossmod.Content.Calamity.NPCs;
using FargowiltasCrossmod.Content.Calamity.Toggles;
using FargowiltasSouls;
using FargowiltasSouls.Content.Bosses.AbomBoss;
using FargowiltasSouls.Content.Bosses.BanishedBaron;
using FargowiltasSouls.Content.Bosses.Champions.Cosmos;
using FargowiltasSouls.Content.Bosses.Champions.Earth;
using FargowiltasSouls.Content.Bosses.Champions.Life;
using FargowiltasSouls.Content.Bosses.Champions.Nature;
using FargowiltasSouls.Content.Bosses.Champions.Shadow;
using FargowiltasSouls.Content.Bosses.Champions.Spirit;
using FargowiltasSouls.Content.Bosses.Champions.Terra;
using FargowiltasSouls.Content.Bosses.Champions.Timber;
using FargowiltasSouls.Content.Bosses.Champions.Will;
using FargowiltasSouls.Content.Bosses.DeviBoss;
using FargowiltasSouls.Content.Bosses.Lifelight;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Content.Bosses.TrojanSquirrel;
using FargowiltasSouls.Content.Buffs;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Items;
using FargowiltasSouls.Content.NPCs.EternityModeNPCs;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static CalamityMod.Events.BossRushEvent;
using static FargowiltasCrossmod.Core.Common.Globals.DevianttGlobalNPC;

namespace FargowiltasCrossmod.Core.Calamity.Systems
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class CalDLCCompatibilityMisc : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod) => ModCompatibility.Calamity.Loaded;
        #region summonloadingbullshit
        public static bool DownedDS => DownedBossSystem.downedDesertScourge;
        public static bool DownedCrab => DownedBossSystem.downedCrabulon;
        public static bool DownedHM => DownedBossSystem.downedHiveMind;
        public static bool DownedPerf => DownedBossSystem.downedPerforator;
        public static bool DownedSG => DownedBossSystem.downedSlimeGod;
        public static bool DownedCryo => DownedBossSystem.downedCryogen;
        public static bool DownedAS => DownedBossSystem.downedAquaticScourge;
        public static bool DownedBE => DownedBossSystem.downedBrimstoneElemental;
        public static bool DownedCalClone => DownedBossSystem.downedCalamitasClone;
        public static bool DownedAA => DownedBossSystem.downedAstrumAureus;
        public static bool DownedLevi => DownedBossSystem.downedLeviathan;
        public static bool DownedPBG => DownedBossSystem.downedPlaguebringer;
        public static bool DownedRav => DownedBossSystem.downedRavager;
        public static bool DownedDeus => DownedBossSystem.downedAstrumDeus;
        public static bool DownedFuck => DownedBossSystem.downedDragonfolly;
        public static bool DownedGuards => DownedBossSystem.downedGuardians;
        public static bool DownedProvi => DownedBossSystem.downedProvidence;
        public static bool DownedCV => DownedBossSystem.downedCeaselessVoid;
        public static bool DownedSignus => DownedBossSystem.downedSignus;
        public static bool DownedSW => DownedBossSystem.downedStormWeaver;
        public static bool DownedPolter => DownedBossSystem.downedPolterghast;
        public static bool DownedOD => DownedBossSystem.downedBoomerDuke;
        public static bool DownedDoG => DownedBossSystem.downedDoG;
        public static bool DownedYharon => DownedBossSystem.downedYharon;
        public static bool DownedExos => DownedBossSystem.downedExoMechs;
        public static bool DownedScal => DownedBossSystem.downedCalamitas;
        public static bool DownedCragmaw => DownedBossSystem.downedCragmawMire;
        public static bool DownedMauler => DownedBossSystem.downedMauler;
        public static bool DownedNuclear => DownedBossSystem.downedNuclearTerror;
        public static bool DownedGSS => DownedBossSystem.downedGSS;
        public static bool DownedClam => DownedBossSystem.downedCLAM;
        #endregion summonloadingbullshit
        public override void Load()
        {
            Add("Archmage", ModContent.NPCType<Archmage>());
            Add("SeaKing", ModContent.NPCType<SeaKing>());
            Add("Bandit", ModContent.NPCType<Bandit>());
            Add("BrimstoneWitch", ModContent.NPCType<BrimstoneWitch>());
        }
        public static void Add(string internalName, int id)
        {
            if (FargowiltasCrossmod.Instance == null)
            {
                FargowiltasCrossmod.Instance = ModContent.GetInstance<FargowiltasCrossmod>();
            }
            CaughtNPCItem item = new(internalName, id);
            FargowiltasCrossmod.Instance.AddContent(item);
            FieldInfo info = typeof(CaughtNPCItem).GetField("CaughtTownies", LumUtils.UniversalBindingFlags);
            Dictionary<int, int> list = (Dictionary<int, int>)info.GetValue(info);
            list.Add(id, item.Type);
            info.SetValue(info, list);
        }
        public override void PostSetupContent()
        {
            if (!ModCompatibility.Calamity.Loaded)
                return;
            #region summons
            Mod mutant = ModLoader.GetMod("Fargowiltas");
            mutant.Call("AddSummon", 1.5f, "FargowiltasCrossmod", "MedallionoftheDesert",
                () => DownedDS, Item.buyPrice(gold: 6));
            mutant.Call("AddSummon", 2.5f, "FargowiltasCrossmod", "OphiocordycipitaceaeSprout",
                () => DownedCrab, Item.buyPrice(gold: 9));
            mutant.Call("AddSummon", 3.5f, "FargowiltasCrossmod", "HiveTumor",
                () => DownedHM, Item.buyPrice(gold: 11));
            mutant.Call("AddSummon", 3.5f, "FargowiltasCrossmod", "RedStainedWormFood",
                () => DownedPerf, Item.buyPrice(gold: 11));
            mutant.Call("AddSummon", 6.5f, "FargowiltasCrossmod", "MurkySludge",
                () => DownedSG, Item.buyPrice(gold: 17));
            mutant.Call("AddSummon", 8.5f, "FargowiltasCrossmod", "CryingKey",
                () => DownedCryo, Item.buyPrice(gold: 30));
            mutant.Call("AddSummon", 9.5f, "FargowiltasCrossmod", "SeeFood",
                () => DownedAS, Item.buyPrice(gold: 40));
            mutant.Call("AddSummon", 10.5f, "FargowiltasCrossmod", "FriedDoll",
                () => DownedBE, Item.buyPrice(gold: 40));
            mutant.Call("AddSummon", 11.5f, "FargowiltasCrossmod", "BlightedEye",
                () => DownedCalClone, Item.buyPrice(gold: 45));
            mutant.Call("AddSummon", 12.5f, "FargowiltasCrossmod", "SirensPearl",
                () => DownedLevi, Item.buyPrice(gold: 55));
            mutant.Call("AddSummon", 12.75f, "FargowiltasCrossmod", "ChunkyStardust",
                () => DownedAA, Item.buyPrice(gold: 55));
            mutant.Call("AddSummon", 13.5f, "FargowiltasCrossmod", "ABombInMyNation",
                () => DownedPBG, Item.buyPrice(gold: 60));
            mutant.Call("AddSummon", 13.75f, "FargowiltasCrossmod", "NoisyWhistle",
                () => DownedRav, Item.buyPrice(gold: 60));
            mutant.Call("AddSummon", 17.5f, "FargowiltasCrossmod", "AstrumCor",
                () => DownedDeus, Item.buyPrice(gold: 85));
            mutant.Call("AddSummon", 18.006f, "FargowiltasCrossmod", "BirbPheromones",
                () => DownedFuck, Item.buyPrice(platinum: 1, gold: 10));
            mutant.Call("AddSummon", 18.007f, "FargowiltasCrossmod", "DefiledShard",
                () => DownedGuards, Item.buyPrice(platinum: 1, gold: 10));
            mutant.Call("AddSummon", 18.008f, "FargowiltasCrossmod", "DefiledCore",
                () => DownedProvi, Item.buyPrice(platinum: 1, gold: 20));
            mutant.Call("AddSummon", 18.0091f, "FargowiltasCrossmod", "RiftofKos",
                () => DownedCV, Item.buyPrice(platinum: 1, gold: 30));
            mutant.Call("AddSummon", 18.0092f, "FargowiltasCrossmod", "WormFoodofKos",
                () => DownedSW, Item.buyPrice(platinum: 1, gold: 30));
            mutant.Call("AddSummon", 18.0093f, "FargowiltasCrossmod", "LetterofKos",
                () => DownedSignus, Item.buyPrice(platinum: 1, gold: 30));
            mutant.Call("AddSummon", 18.0094f, "FargowiltasCrossmod", "PolterplasmicBeacon",
                () => DownedPolter, Item.buyPrice(platinum: 1, gold: 40));
            mutant.Call("AddSummon", 18.0095f, "FargowiltasCrossmod", "BloodyWorm",
                () => DownedOD, Item.buyPrice(platinum: 1, gold: 40));
            mutant.Call("AddSummon", 18.0096f, "FargowiltasCrossmod", "SomeKindofSpaceWorm",
                () => DownedDoG, Item.buyPrice(platinum: 2));
            mutant.Call("AddSummon", 18.0097f, "FargowiltasCrossmod", "DragonEgg",
                () => DownedYharon, Item.buyPrice(platinum: 2, gold: 50));
            mutant.Call("AddSummon", 18.012f, "FargowiltasCrossmod", "PortableCodebreaker",
                () => DownedExos, Item.buyPrice(platinum: 3));
            mutant.Call("AddSummon", 18.014f, "FargowiltasCrossmod", "EyeofExtinction",
                () => DownedScal, Item.buyPrice(platinum: 3));
            #endregion summons
            #region bossrush
            //EXPLANATION OF TUPLES
            //int: npc id
            //int: time to set to (1 = day, -1 = night, 0 = dont force time)
            //Action<int>: "boss spawning function" idfk look at cal source but dont put null though
            //int: cooldown (to spawn i think???)
            //bool: boss needs special sound effect
            //float: dimness factor to be used when boss spawned
            //int[]: list of npc ids to not delete when spawning
            //int[]: list of other npc ids that need to die to continue event

            Bosses =
                [
                new Boss(NPCID.KingSlime, spawnContext: type => {
                    NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);

                    
                },permittedNPCs: new int[] { NPCID.BlueSlime, NPCID.YellowSlime, NPCID.PurpleSlime, NPCID.RedSlime, NPCID.GreenSlime, NPCID.RedSlime,
                    NPCID.IceSlime, NPCID.UmbrellaSlime, NPCID.Pinky, NPCID.SlimeSpiked, NPCID.RainbowSlime, ModContent.NPCType<KingSlimeJewelRuby>() }),

                new Boss(NPCID.MoonLordCore, spawnContext: type =>{
                    NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
                    // When Moon Lord spawns, Boss Rush is considered to be started at least once.
                    // King Slime will then be skipped
                    DownedBossSystem.startedBossRushAtLeastOnce = true;
                }, permittedNPCs: [NPCID.MoonLordLeechBlob, NPCID.MoonLordHand, NPCID.MoonLordHead, NPCID.MoonLordFreeEye]),
                new Boss(ModContent.NPCType<Providence>(), TimeChangeContext.Day, type =>{
                    SoundEngine.PlaySound(Providence.SpawnSound, Main.player[ClosestPlayerToWorldCenter].Center);
                    int provi = NPC.NewNPC(new EntitySource_WorldEvent(), (int)(Main.player[ClosestPlayerToWorldCenter].Center.X), (int)(Main.player[ClosestPlayerToWorldCenter].Center.Y - 400), type, 1);
                    Main.npc[provi].timeLeft *= 20;
                    CalamityUtils.BossAwakenMessage(provi);
                }, usesSpecialSound: true, permittedNPCs: [ModContent.NPCType<ProvSpawnDefense>(), ModContent.NPCType<ProvSpawnHealer>(), ModContent.NPCType<ProvSpawnOffense>(),
                    ModContent.NPCType<ProfanedGuardianCommander>(), ModContent.NPCType<ProfanedGuardianDefender>(), ModContent.NPCType<ProfanedGuardianHealer>()]),
                

                new Boss(ModContent.NPCType<Polterghast>(), permittedNPCs: [ModContent.NPCType<PhantomFuckYou>(), ModContent.NPCType<PolterghastHook>(), ModContent.NPCType<PolterPhantom>()]),
                new Boss(ModContent.NPCType<OldDuke>(), spawnContext: type => {
                    int od = NPC.NewNPC(new EntitySource_WorldEvent(), (int)(Main.player[ClosestPlayerToWorldCenter].Center.X + Main.rand.Next(-100, 101)), (int)Main.player[ClosestPlayerToWorldCenter].Center.Y - 300, type, 1);
                    CalamityUtils.BossAwakenMessage(od);
                    Main.npc[od].timeLeft *= 20;
                }, permittedNPCs: [ModContent.NPCType<SulphurousSharkron>(), ModContent.NPCType<OldDukeToothBall>()]),
                new Boss(ModContent.NPCType<DevourerofGodsHead>(), spawnContext: type => {
                    SoundEngine.PlaySound(DevourerofGodsHead.SpawnSound, Main.player[ClosestPlayerToWorldCenter].Center);
                    NPC.SpawnOnPlayer(ClosestPlayerToWorldCenter, type);
                }, usesSpecialSound: true, permittedNPCs: [ModContent.NPCType<DevourerofGodsBody>(), ModContent.NPCType<DevourerofGodsTail>(),
                ModContent.NPCType<Signus>(), ModContent.NPCType<CeaselessVoid>(), ModContent.NPCType<StormWeaverHead>(), ModContent.NPCType<StormWeaverBody>(), ModContent.NPCType<StormWeaverTail>()]),
                new Boss(ModContent.NPCType<CosmosChampion>(), spawnContext: type => {
                    int erd = NPC.NewNPC(new EntitySource_WorldEvent(), (int)(Main.player[ClosestPlayerToWorldCenter].Center.X), (int)(Main.player[ClosestPlayerToWorldCenter].Center.Y - 400), type, 1);
                    Main.npc[erd].timeLeft *= 20;
                    CalamityUtils.BossAwakenMessage(erd);
                }),
                new Boss(ModContent.NPCType<Yharon>(), permittedNPCs: ModContent.NPCType<Dragonfolly>()),
                new Boss(ModContent.NPCType<AbomBoss>()),
                new Boss(ModContent.NPCType<Draedon>(), spawnContext: type =>
                {
                    if (!NPC.AnyNPCs(ModContent.NPCType<Draedon>()))
                    {
                        Player player = Main.player[ClosestPlayerToWorldCenter];

                        SoundEngine.PlaySound(CodebreakerUI.SummonSound, player.Center);
                        Vector2 spawnPos = player.Center + new Vector2(-8f, -100f);
                        int draedon = NPC.NewNPC(new EntitySource_WorldEvent("CalamityMod_BossRush"), (int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<Draedon>());
                        Main.npc[draedon].timeLeft *= 20;
                    }
                }, usesSpecialSound: true, permittedNPCs: new int[] { ModContent.NPCType<Apollo>(), ModContent.NPCType<AresBody>(), ModContent.NPCType<AresGaussNuke>(), ModContent.NPCType<AresLaserCannon>(), ModContent.NPCType<AresPlasmaFlamethrower>(), ModContent.NPCType<AresTeslaCannon>(), ModContent.NPCType<Artemis>(), ModContent.NPCType<ThanatosBody1>(), ModContent.NPCType<ThanatosBody2>(), ModContent.NPCType<ThanatosHead>(), ModContent.NPCType<ThanatosTail>() }),
                new Boss(ModContent.NPCType<SupremeCalamitas>(), spawnContext: type => {
                    SoundEngine.PlaySound(SupremeCalamitas.SpawnSound, Main.player[ClosestPlayerToWorldCenter].Center);
                    CalamityUtils.SpawnBossBetter(Main.player[ClosestPlayerToWorldCenter].Top - new Vector2(42, 84f), type);
                }, dimnessFactor: 0.5f, permittedNPCs: [ModContent.NPCType<SepulcherArm>(), ModContent.NPCType<SepulcherBody>(), ModContent.NPCType<SepulcherHead>(), ModContent.NPCType<SepulcherTail>(), ModContent.NPCType<SepulcherBodyEnergyBall>(), ModContent.NPCType<SoulSeekerSupreme>(), ModContent.NPCType<BrimstoneHeart>(), ModContent.NPCType<SupremeCataclysm>(), ModContent.NPCType<SupremeCatastrophe>()]),
                new Boss(ModContent.NPCType<MutantBoss>(), permittedNPCs: [ModContent.NPCType<MutantIllusion>()])
                ];
            
            
            BossDeathEffects.Remove(ModContent.NPCType<SupremeCalamitas>());
            BossDeathEffects.Remove(ModContent.NPCType<DevourerofGodsHead>());
            BossDeathEffects.Add(ModContent.NPCType<MutantBoss>(), npc => { BossRushDialogueSystem.StartDialogue(DownedBossSystem.downedBossRush ? BossRushDialoguePhase.EndRepeat : BossRushDialoguePhase.End); });

            ////Adding bosses to boss rush
            #endregion bossrush
            Mod cal = ModCompatibility.Calamity.Mod;
            cal.Call("RegisterModCooldowns", FargowiltasCrossmod.Instance);
            cal.Call("AddDifficultyToUI", new EternityRevDifficulty());
            cal.Call("AddDifficultyToUI", new EternityDeathDifficulty());

            #region CalDebuffListCompat
            List<int> calamityDebuffs = Enumerable.Range(BuffID.Count, BuffLoader.BuffCount - BuffID.Count)
                .Where(i => CalamityBuffSets.IsDebuff[i])
                .ToList();
            CalamityBuffSets.IsDebuff[ModContent.BuffType<AnticoagulationBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<AntisocialBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<AtrophiedBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<BerserkedBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<BloodthirstyBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<BaronsBurdenBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<ClippedWingsBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<CrippledBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<CurseoftheMoonBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<DefenselessBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<FlamesoftheUniverseBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<FlippedBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<FusedBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<GodEaterBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<GuiltyBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<HexedBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<HypothermiaBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<InfestedBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<IvyVenomBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<JammedBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<LethargicBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<LightningRodBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<LivingWastelandBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<LovestruckBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<LowGroundBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<MarkedforDeathBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<MidasBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<MutantNibbleBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<NanoInjectionBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<NeurotoxinBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<NullificationCurseBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<OceanicMaulBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<OiledBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<PurgedBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<PurifiedBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<ReverseManaFlowBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<RottingBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<SmiteBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<ShadowflameBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<SqueakyToyBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<StunnedBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<SwarmingBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<TimeFrozenBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<UnluckyBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<UnstableBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<BerserkerInstallBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<HolyPriceBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<BrainOfConfusionBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<LihzahrdCurseBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<RushJobBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<TwinsInstallBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<SnowstormCDBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<HellFireBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<LeadPoisonBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<OriPoisonBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<PungentGazeBuff>()] = true;
            CalamityBuffSets.IsDebuff[ModContent.BuffType<SolarFlareBuff>()] = true;
            FieldInfo debuffIDs = typeof(FargowiltasSouls.FargowiltasSouls).GetField("DebuffIDs", LumUtils.UniversalBindingFlags);
            List<int> newDebuffIDs = (List<int>)debuffIDs.GetValue(null);
            newDebuffIDs.AddRange(calamityDebuffs);
            debuffIDs.SetValue(null, newDebuffIDs);
            #endregion CalDebuffListCompat
            #region SwordRework
            int[] CalSwordsToApplyRework = [ModContent.ItemType<AbsoluteZero>(), ModContent.ItemType<AegisBlade>(),
            ModContent.ItemType<AnarchyBlade>(),
            ModContent.ItemType<AstralScythe>(),ModContent.ItemType<Ataraxia>(),ModContent.ItemType<Avalanche>(),
            ModContent.ItemType<BalefulHarvester>(),ModContent.ItemType<Basher>(),
            ModContent.ItemType<BlightedCleaver>(),ModContent.ItemType<Brimlash>(),ModContent.ItemType<BrimstoneSword>(),
            ModContent.ItemType<BrinyBaron>(),ModContent.ItemType<BurntSienna>(),ModContent.ItemType<Carnage>(),
            ModContent.ItemType<TrueCausticEdge>(), ModContent.ItemType<CelestialClaymore>(),
            ModContent.ItemType<CometQuasher>(),ModContent.ItemType<DarklightGreatsword>(),ModContent.ItemType<DefiledGreatsword>(),
            ModContent.ItemType<DevilsDevastation>(),ModContent.ItemType<DraconicDestruction>(),
            ModContent.ItemType<Earth>(),ModContent.ItemType<EntropicClaymore>(),ModContent.ItemType<EssenceFlayer>(),
            ModContent.ItemType<EvilSmasher>(),ModContent.ItemType<ExaltedOathblade>(),
            ModContent.ItemType<MawOfInfinity>(),ModContent.ItemType<FeralthornClaymore>(),ModContent.ItemType<FlarefrostBlade>(),
            ModContent.ItemType<Floodtide>(),ModContent.ItemType<ForbiddenOathblade>(),ModContent.ItemType<ForsakenSaber>(),
            ModContent.ItemType<GaelsGreatsword>(),ModContent.ItemType<GalactusBlade>(),ModContent.ItemType<GeliticBlade>(),
            ModContent.ItemType<GrandGuardian>(),ModContent.ItemType<GreatswordofJudgement>(),
            ModContent.ItemType<Greentide>(),ModContent.ItemType<HellfireFlamberge>(),ModContent.ItemType<Hellkite>(),
            ModContent.ItemType<HolyCollider>(),
            ModContent.ItemType<LifehuntScythe>(),ModContent.ItemType<LionHeart>(),ModContent.ItemType<MajesticGuard>(),
            ModContent.ItemType<MirrorBlade>(),ModContent.ItemType<Orderbringer>(),ModContent.ItemType<PerfectDark>(),
            ModContent.ItemType<RedSun>(),
            ModContent.ItemType<SeashineSword>(),ModContent.ItemType<SolsticeClaymore>(),ModContent.ItemType<SoulHarvester>(),
            ModContent.ItemType<StellarStriker>(),ModContent.ItemType<StormRuler>(),ModContent.ItemType<StormSaber>(),
            ModContent.ItemType<Swordsplosion>(),ModContent.ItemType<TaintedBlade>(),ModContent.ItemType<TeardropCleaver>(),
            ModContent.ItemType<TerrorBlade>(),ModContent.ItemType<TheDarkMaster>(),
            ModContent.ItemType<TheLastMourning>(),ModContent.ItemType<TheMutilator>(),ModContent.ItemType<TitanArm>(),
            ModContent.ItemType<UltimusCleaver>(),ModContent.ItemType<VeinBurster>(),ModContent.ItemType<Virulence>(),
            ModContent.ItemType<VoidEdge>(),ModContent.ItemType<WindBlade>(),];
            SwordGlobalItem.AllowedModdedSwords = SwordGlobalItem.AllowedModdedSwords.Union(CalSwordsToApplyRework).ToArray();
            #endregion
        }
        //make this a property instead of directly using it so tml doesnt shit itself trying to load it
        public ref Dictionary<int, Action<NPC>> DeathEffectsList => ref BossRushEvent.BossDeathEffects;
    }
}
