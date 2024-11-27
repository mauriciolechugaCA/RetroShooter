using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroShooter.Entities.Projectiles;
using RetroShooter.Managers;

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
        private Vector2 direction;
        private Random random;
        private SoundManager _soundManager;

        // Constructor (edit starting values if needed)
        public EnemyShooter(Vector2 startPosition, Texture2D texture, Texture2D projectileTexture, List<Projectile> projectiles, SoundManager soundManager) : base(startPosition, 40, 1.25f, 25, texture, 25)
        {
            this.projectileTexture = projectileTexture;
            this.projectiles = projectiles;
            shootCooldown = 2.0f; // Cooldown between shots
            lastShottime = 0;
            random = new Random();
            direction = GetRandomDownwardDirection();
            _soundManager = soundManager;
        }

        public override void Move(Player player)
        {
            position += direction * speed;

            if (position.X <= 0 || position.X + texture.Width >= 768)
            {
                direction.X = -direction.X;
            }

            if (position.Y > 1024)
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
                    12.5f, // Speed of the projectile
                    10, // Damage of the projectile
                    projectileTexture,
                    ProjectileOwner.Enemy
                );

                projectiles.Add(projectile);
                lastShottime = (float)gameTime.TotalGameTime.TotalSeconds;

                _soundManager.PlaySoundEffect("enemyLaser");
            }
        }

        private Vector2 GetRandomDownwardDirection()
        {
            float x = (float)random.NextDouble() * 2f - 1f;
            float y = ((float)random.NextDouble() * 0.5f) + 0.5f;
            Vector2 randomDirection = new Vector2(x, y);
            randomDirection.Normalize();
            return randomDirection;
        }

        public override void Update(GameTime gameTime, Player player)
        {
            base.Update(gameTime, player);
            Shoot(gameTime, player);
        }

        public override int GetPoints()
        {
            return 30;
        }
    }
}