using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace MultipleLures
{
    internal class MultipleLuresGlobalProj : GlobalProjectile
    {
        internal static Configuration configuration;

        public override void OnSpawn(Projectile projectile, IEntitySource source) {
            if (projectile.bobber && projectile.owner == Main.myPlayer && source is EntitySource_ItemUse) {
                float maxR = MathHelper.ToRadians((float)Math.Sqrt(configuration.LuresAmount) * 4f);
                float increament = maxR / configuration.LuresAmount;
                maxR -= increament;
                for (float r = 0f; r <= maxR; r += increament) {
                    float finalR = r - maxR / 2f;
                    int p = Projectile.NewProjectile(
                        projectile.GetSource_FromThis("MultipleLure"),
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
                    }
                }
                projectile.active = false;
            }
        }

        public override bool InstancePerEntity => true;
    }
}
