using System;
using CalamityMod.NPCs.CalClone;
using CalamityMod.NPCs.HiveMind;
using CalamityMod.NPCs.SlimeGod;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Terraria;
using Terraria.ModLoader;

namespace FargowiltasCrossmod.Core.Calamity
{
    internal static class CalamityProjectileDamage
    {
        private static readonly int[] SingleDamage30 = [30];
        private static readonly int[] Damage50And92 = [50, 92];
        private static readonly int[] SingleDamage16 = [16];
        private static readonly int[] SingleDamage20 = [20];

        public static int GetProjectileDamage(NPC npc, int projectileType) =>
            GetProjectileDamage(npc.type, projectileType);

        public static int GetProjectileDamage(Projectile projectile, int npcType) =>
            GetProjectileDamage(npcType, projectile.type);

        private static int GetProjectileDamage(int npcType, int projectileType)
        {
            int[]? damageValues = GetDamageValues(npcType, projectileType);
            if (damageValues is null)
                return 1;

            int[] projectileDamage = new int[4];
            if (damageValues.Length == 1)
            {
                projectileDamage[0] = damageValues[0];
                projectileDamage[1] = damageValues[0] * 2;
                projectileDamage[2] = damageValues[0] * 2;
                projectileDamage[3] = damageValues[0] * 3;
            }
            else if (damageValues.Length == 2)
            {
                projectileDamage[0] = damageValues[0];
                projectileDamage[1] = damageValues[1];
                projectileDamage[2] = damageValues[1];
                projectileDamage[3] = (int)(damageValues[1] * 1.5f);
            }
            else if (damageValues.Length == 4)
            {
                projectileDamage = damageValues;
            }
            else
            {
                return 1;
            }

            double damageAdjustment = Main.masterMode ? 6D : Main.expertMode ? 4D : 2D;
            int normalDamage = (int)Math.Round(projectileDamage[0] / damageAdjustment);
            int expertDamage = (int)Math.Round(projectileDamage[1] / damageAdjustment);
            int revengeanceDamage = (int)Math.Round(projectileDamage[2] / damageAdjustment);
            int masterDamage = (int)Math.Round(projectileDamage[3] / damageAdjustment);
            return Main.masterMode ? masterDamage : CalamityWorld.revenge ? revengeanceDamage : Main.expertMode ? expertDamage : normalDamage;
        }

        private static int[]? GetDamageValues(int npcType, int projectileType)
        {
            if (npcType == ModContent.NPCType<SlimeGodCore>())
            {
                if (projectileType == ModContent.ProjectileType<UnstableEbonianGlob>() ||
                    projectileType == ModContent.ProjectileType<UnstableCrimulanGlob>())
                    return SingleDamage30;
            }

            if (npcType == ModContent.NPCType<EbonianPaladin>() &&
                projectileType == ModContent.ProjectileType<UnstableEbonianGlob>())
                return SingleDamage30;

            if (npcType == ModContent.NPCType<CrimulanPaladin>())
            {
                if (projectileType == ModContent.ProjectileType<UnstableCrimulanGlob>())
                    return SingleDamage30;
            }

            if (npcType == ModContent.NPCType<DarkHeart>() &&
                projectileType == ModContent.ProjectileType<ShaderainHostile>())
                return SingleDamage20;

            if (npcType == ModContent.NPCType<HiveBlob>() &&
                projectileType == ModContent.ProjectileType<VileClot>())
                return SingleDamage16;

            if (npcType == ModContent.NPCType<CalamitasClone>() &&
                projectileType == ModContent.ProjectileType<BrimstoneBarrage>())
                return Damage50And92;

            return null;
        }
    }
}
