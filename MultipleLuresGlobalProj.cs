using Microsoft.Xna.Framework;
using System;
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
                        projectile.position,
                        projectile.velocity.RotatedBy(finalR),
                        projectile.type,
                        projectile.damage,
                        projectile.knockBack,
                        projectile.owner,
                        projectile.ai[0],
                        projectile.ai[1]);
                    if (p < 1000 && Main.projectile[p] != null) {
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

        public override bool InstancePerEntity => true;
    }
}
