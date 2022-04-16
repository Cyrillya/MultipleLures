using Terraria;
using Fargowiltas.Projectiles;
using Terraria.ModLoader;
using System.Reflection;

namespace MultipleLures
{
    internal class CrossModSystem
    {
        internal static bool isFargowiltasLoaded;

        internal static void TryModifyFargowiltasLures(Projectile proj) {
            FargoGlobalProjectile projFargo = proj.GetGlobalProjectile<FargoGlobalProjectile>();
            if (projFargo == null) return;
            var targetBool = projFargo.GetType().GetField("firstTick", BindingFlags.Instance | BindingFlags.NonPublic);
            targetBool.SetValue(projFargo, false);
        }

        public static void PostSetupContent() {
            isFargowiltasLoaded = false;
            if (ModLoader.GetMod("Fargowiltas") != null) {
                isFargowiltasLoaded = true;
            }
        }

        public static void Unload() {
            isFargowiltasLoaded = false;
        }
    }
}
