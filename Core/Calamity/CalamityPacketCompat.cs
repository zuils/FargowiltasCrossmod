using System;
using System.Reflection;
using CalamityMod.NPCs;

namespace FargowiltasCrossmod.Core.Calamity
{
    internal static class CalamityPacketCompat
    {
        public static void SendExoMechSelection()
        {
            Type packetType = typeof(CalamityGlobalNPC).Assembly.GetType("CalamityMod.Packets.ExoMechSelectionPacket", true)
                ?? throw new TypeLoadException("Calamity's ExoMechSelectionPacket type could not be found.");
            MethodInfo sendMethod = packetType.GetMethod("Send", BindingFlags.Public | BindingFlags.Static)
                ?? throw new MissingMethodException(packetType.FullName, "Send");
            sendMethod.Invoke(null, new object[] { -1, -1 });
        }
    }
}
