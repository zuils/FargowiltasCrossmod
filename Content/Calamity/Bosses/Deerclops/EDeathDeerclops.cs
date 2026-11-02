using CalamityMod;
using FargowiltasCrossmod.Core;
using FargowiltasCrossmod.Core.Calamity.Globals;
using FargowiltasSouls.Content.Bosses.VanillaEternity;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasCrossmod.Content.Calamity.Bosses.EyeOfCthulhu
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    public class EDeathDeerclops : CalDLCEDeathBehavior
    {
        public override int NPCOverrideID => NPCID.Deerclops;
        public override void PostAI()
        {
            Deerclops deerclops = NPC.GetGlobalNPC<Deerclops>();
            if (deerclops.TeleportTimer > 780 && NPC.HasPlayerTarget && deerclops.EnteredPhase2)
                Main.LocalPlayer.Calamity().lastDeerclopsPosition = Main.player[NPC.target].Center;
        }
    }
}
