using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroShooter.Entities.Projectiles;

/*
 * Represents a challenging enemy that moves slower but shoots homing projectiles towards the player at a higher rate.
 */

namespace RetroShooter.Entities.Enemies
{
    public class EnemyShooter : Enemy
    {
        private float shootCooldown;
        private float lastShottime;
        private Texture2D projectileTexture;
        private List<Projectile> projectiles;

        public EnemyShooter(Vector2 startPosition, Texture2D texture, Texture2D projectileTexture, List<Projectile> projectiles) : base(startPosition, 100, 1.5f, 50, texture)
        {
            this.projectileTexture = projectileTexture;
            this.projectiles = projectiles;
            shootCooldown = 2.5f; // Cooldown between shots
            lastShottime = 0;
        }

        public override void Move(Player player)
        {
            Vector2 direction = player.Position - position;
            direction.Normalize();
            position += direction * speed;

            if (position.X <= 0 || position.X + texture.Width >= 768 || position.Y > 1024)
            {
                IsAlive = false;
            }
        }
        
        public void Shoot(GameTime gameTime, Player player)
        {
            if (gameTime.TotalGameTime.TotalSeconds - lastShottime > shootCooldown)
            {
                Vector2 direction = player.Position - position;
                direction.Normalize();

                var projectile = new Projectile(
                    position, 
                    direction, 
                    8f, // Speed of the projectile
                    1, // Damage of the projectile
                    projectileTexture
                );

                projectiles.Add(projectile);
                lastShottime = (float)gameTime.TotalGameTime.TotalSeconds;
            }
        }

        public override void Update(GameTime gameTime, Player player)
        {
            base.Update(gameTime, player);
            Shoot(gameTime, player);
        }
    }
}