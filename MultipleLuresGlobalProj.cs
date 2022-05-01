using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace MultipleLures
{
    internal class MultipleLuresGlobalProj : GlobalProjectile
    {
        internal static Configuration configuration;
        private bool firstTick = true;

        public override bool PreAI(Projectile projectile) {
            if (projectile.bobber && projectile.owner == Main.myPlayer && projectile.active && firstTick) {
                firstTick = false;
                var player = Main.player[projectile.owner];
                float maxR = MathHelper.ToRadians((float)Math.Sqrt(configuration.LuresAmount) * 4f);
                float increament = maxR / configuration.LuresAmount;
                maxR -= increament;
                for (float r = 0f; r <= maxR; r += increament) {
                    float finalR = r - maxR / 2f;
                    int p = Projectile.NewProjectile(
                        player.GetSource_ItemUse(player.HeldItem),
                        projectile.position,
                        projectile.velocity.RotatedBy(finalR),
                        projectile.type,
                        projectile.damage,
                        projectile.knockBack,
                        projectile.owner,
                        projectile.ai[0],
                        projectile.ai[1]);
                    if (p < 1000 && Main.projectile[p] is not null) {
                        var proj = Main.projectile[p];
                        proj.friendly = true;
                        proj.GetGlobalProjectile<MultipleLuresGlobalProj>().firstTick = false;
                        if (CrossModSystem.isFargowiltasLoaded) {
                            CrossModSystem.TryModifyFargowiltasLures(proj);
                        }
                    }
                }
                projectile.active = false;
            }
            return base.PreAI(projectile);
        }

        public override void Load() {
            On.Terraria.Projectile.NewProjectile_IEntitySource_float_float_float_float_int_int_float_int_float_float += NewProjectilePatch;
        }

        private int NewProjectilePatch(On.Terraria.Projectile.orig_NewProjectile_IEntitySource_float_float_float_float_int_int_float_int_float_float orig, Terraria.DataStructures.IEntitySource spawnSource, float X, float Y, float SpeedX, float SpeedY, int Type, int Damage, float KnockBack, int Owner, float ai0, float ai1) {
            int i = orig.Invoke(spawnSource, X, Y, SpeedX, SpeedY, Type, Damage, KnockBack, Owner, ai0, ai1);
            var proj = Main.projectile[i];
            if (proj.bobber && Owner == Main.myPlayer && CrossModSystem.isFargowiltasLoaded) {
                CrossModSystem.TryModifyFargowiltasLures(proj);
            }
            return i;
        }

        public override bool InstancePerEntity => true;
    }
}
