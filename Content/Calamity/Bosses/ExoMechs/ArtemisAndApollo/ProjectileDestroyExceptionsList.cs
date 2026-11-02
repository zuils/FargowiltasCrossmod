using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasCrossmod.Content.Calamity.Bosses.ExoMechs.ArtemisAndApollo
{
    internal static class ProjectileDestroyExceptionsList
    {
        private static readonly string[] CalamityProjectileNames =
        [
            "BrokenBiomeBladeHoldout",
            "AridGrandeur",
            "BitingEmbrace",
            "DecaysRetort",
            "BiomeBladeHoldout",
            "TrueAridGrandeur",
            "TrueBitingEmbrace",
            "TrueDecaysRetort",
            "TrueGrovetendersTouch",
            "HeavensMight",
            "HellbornHoldout",
            "CountermeasureMittHoldout",
            "TrueBiomeBladeHoldout",
            "LamentationsOfTheChained",
            "ChainedMeatHook",
            "SwordsmithsPride",
            "SanguineFury",
            "EarthenTides",
            "GalaxiaHoldout",
            "PhoenixsPride",
            "PolarisGaze",
            "AndromedasStride",
            "AriesWrath",
            "ArkoftheAncientsSwungBlade",
            "ArkoftheAncientsParryHoldout",
            "TrueArkoftheAncientsSwungBlade",
            "TrueArkoftheAncientsParryHoldout",
            "ArkoftheElementsSwungBlade",
            "ArkoftheElementsParryHoldout",
            "ArkoftheCosmosSwungBlade",
            "ArkoftheCosmosParryHoldout",
            "BasherHoldout",
            "OldLordClaymoreHoldout",
            "ForbiddenOathbladeHoldout",
            "ExaltedOathbladeHoldout",
            "DevilsDevastationHoldout",
            "GrandDadHoldout",
            "GrandGuardianHoldout",
            "EarthHoldout",
            "MajesticGuardHoldout",
            "HellkiteHoldout",
            "RiftburstBow",
            "CometQuasherHoldout",
            "StellarStrikerHoldout",
            "ContagionBow",
            "DaemonsFlameBow",
            "DrataliornusBow",
            "FlakKrakenHoldout",
            "BuzzkillHoldout",
            "SuperradiantSlaughtererHoldout",
            "NorfleetCannon",
            "FlurrystormCannonShooting",
            "ChickenCannonHeld",
            "PumplerHoldout",
            "ClockworkBowHoldout",
            "UltimaBowProjectile",
            "CondemnationHoldout",
            "SurgeDriverHoldout",
            "StarmageddonHeld",
            "NanoPurgeHoldout",
            "AetherfluxCannonHoldout",
            "YharimsCrystalPrism",
            "DarkSparkPrism",
            "YharimsCrystalBeam",
            "DarkSparkBeam",
            "GhastlyVisageProj",
            "ApotheosisWorm",
            "SpiritCongregation",
            "RancorLaserbeam",
            "NebulousCataclysm_Held",
            "FlakKrakenProjectile",
            "InfernadoFriendly",
            "DragonRageStaff",
            "MurasamaSlash",
            "PhaseslayerProjectile",
            "TaintedBladeSlasher",
            "PhotonRipperProjectile",
            "SpineOfThanatosProjectile",
            "FinalDawnProjectile",
            "FinalDawnThrow",
            "FinalDawnHorizontalSlash",
            "FinalDawnFireSlash",
            "BrimstoneMonster",
            "InfernadoRevenge",
            "OverlyDramaticDukeSummoner",
            "ProvidenceHolyRay",
            "OldDukeVortex",
            "BrimstoneRay",
            "AresDeathBeamStart",
            "AresGaussNukeProjectileBoom",
            "AresLaserBeamStart",
            "ArtemisSpinLaserbeam",
            "BirbAura",
            "ThanatosBeamStart"
        ];

        private static HashSet<int>? exceptionProjectileTypes;

        public static bool IsDestroyException(Projectile projectile)
        {
            if (exceptionProjectileTypes is null)
            {
                exceptionProjectileTypes =
                [
                    ProjectileID.Phantasm,
                    ProjectileID.VortexBeater,
                    ProjectileID.DD2PhoenixBow,
                    ProjectileID.LastPrism,
                    ProjectileID.LastPrismLaser,
                    ProjectileID.LaserMachinegun,
                    ProjectileID.ChargedBlasterCannon,
                    ProjectileID.MedusaHead,
                    ProjectileID.SaucerDeathray,
                    ProjectileID.PhantasmalDeathray
                ];

                foreach (string projectileName in CalamityProjectileNames)
                {
                    exceptionProjectileTypes.Add(ModContent.Find<ModProjectile>("CalamityMod", projectileName).Type);
                }
            }

            return exceptionProjectileTypes.Contains(projectile.type);
        }
    }
}
