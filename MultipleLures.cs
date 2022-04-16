using Terraria;
using Terraria.ModLoader;

namespace MultipleLures
{
    public class MultipleLures : Mod
    {
        public override void Load() {
            On.Terraria.Projectile.NewProjectile_float_float_float_float_int_int_float_int_float_float += NewProjectilePatch;
        }

        private int NewProjectilePatch(On.Terraria.Projectile.orig_NewProjectile_float_float_float_float_int_int_float_int_float_float orig, float X, float Y, float SpeedX, float SpeedY, int Type, int Damage, float KnockBack, int Owner, float ai0, float ai1) {
            int i = orig.Invoke(X, Y, SpeedX, SpeedY, Type, Damage, KnockBack, Owner, ai0, ai1);
            var proj = Main.projectile[i];
            if (proj.bobber && Owner == Main.myPlayer && CrossModSystem.isFargowiltasLoaded) {
                CrossModSystem.TryModifyFargowiltasLures(proj);
            }
            return i;
        }

        public override void PostSetupContent() {
            CrossModSystem.PostSetupContent();
        }

        public override void Unload() {
            CrossModSystem.Unload();
        }
    }
}