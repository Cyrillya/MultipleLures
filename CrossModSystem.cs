using Terraria;
using Fargowiltas.Projectiles;
using Terraria.ModLoader;
using System.Reflection;

namespace MultipleLures
{
    internal class CrossModSystem : ModSystem
    {
        internal static bool isFargowiltasLoaded;

        internal static void TryModifyFargowiltasLures(Projectile proj) {
            if (!proj.TryGetGlobalProjectile(out FargoGlobalProjectile projFargo)) return;
            try {
                var targetBool = projFargo.GetType().GetField("firstTick", BindingFlags.Instance | BindingFlags.NonPublic);
                targetBool.SetValue(projFargo, false);
            }
            catch { }
        }

        public override void PostSetupContent() {
            isFargowiltasLoaded = ModLoader.TryGetMod("Fargowiltas", out _);
        }

        public override void Unload() {
            isFargowiltasLoaded = false;
        }
    }
}
