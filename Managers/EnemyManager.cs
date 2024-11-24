using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RetroShooter.Entities.Enemies;
using RetroShooter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroShooter.Entities.Projectiles;
using System.Diagnostics;

/*
 * Handles enemy spawning, behavior updates, and interactions with the player.
 * ## Create, manage, and update enemies based on the current level.
 * ## Control enemy behavior and coordinate different attack patterns or projectiles.
 * ## Detect collisions with the player’s projectiles and update health/status.
 */

namespace RetroShooter.Managers
{
    public class EnemyManager
    {
        private List<Enemy> enemies;
        private Texture2D enemyTexture;
        private Texture2D enemyBulletTexture;
        private float basicEnemySpawnTimer;
        private float shooterEnemySpawnTimer;
        private float basicEnemySpawnInterval;
        private float shooterEnemySpawnInterval;
        private List<Projectile> projectiles;

        public EnemyManager(Texture2D enemyTexture, Texture2D enemyBulletTexture, List<Projectile> projectiles)
        {
            this.enemyTexture = enemyTexture;
            this.enemyBulletTexture = enemyBulletTexture;
            this.projectiles = projectiles;
            enemies = new List<Enemy>();
            basicEnemySpawnInterval = 10.0f;
            shooterEnemySpawnInterval = 20.0f;
            basicEnemySpawnTimer = 0f;
            shooterEnemySpawnTimer = 0f;
        }
        public List<Enemy> Enemies => enemies;

        public void SpawnEnemy(Vector2 position, string enemyType)
        {
            switch (enemyType)
            {
                case "Basic":
                    enemies.Add(new EnemyBasic(position, enemyTexture));
                    break;
                case "Shooter":
                    enemies.Add(new EnemyShooter(position, enemyTexture, enemyBulletTexture, projectiles));
                    break;
                default:
                    throw new ArgumentException("Unknown enemy type");
            }
        }

        public void Update(GameTime gameTime, Player player)
        {
            AdjustSpawnInterval(gameTime.TotalGameTime.TotalSeconds); // Adjusts spawn rate based on game time
            basicEnemySpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            shooterEnemySpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (basicEnemySpawnTimer >= basicEnemySpawnInterval)
            {
                Random random = new Random();
                float xPosition = (float)random.NextDouble() * (768 - enemyTexture.Width);
                SpawnEnemy(new Vector2(xPosition, 0), "Basic");
                basicEnemySpawnTimer = 0f;
                Debug.WriteLine("Basic enemy spawned");
            }

            if (shooterEnemySpawnTimer >= shooterEnemySpawnInterval)
            {
                Random random = new Random();
                float xPosition = (float)random.NextDouble() * (768 - enemyTexture.Width);
                SpawnEnemy(new Vector2(xPosition, 0), "Shooter");
                shooterEnemySpawnTimer = 0f;
                Debug.WriteLine("Shooter enemy spawned");
            }

            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime, player);
            }

            // Remove dead enemies
            enemies.RemoveAll(e => !e.IsAlive);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }

        private void AdjustSpawnInterval(double totalMinutes)
        {
            if (totalMinutes < 1)
            {
                basicEnemySpawnInterval = 10.0f;
                shooterEnemySpawnInterval = 20.0f;
            }
            else if (totalMinutes < 2)
            {
                basicEnemySpawnInterval = 8.0f;
                shooterEnemySpawnInterval = 16.0f;
            }
            else if (totalMinutes < 3)
            {
                basicEnemySpawnInterval = 6.0f;
                shooterEnemySpawnInterval = 12.0f;
            }
            else if (totalMinutes < 4)
            {
                basicEnemySpawnInterval = 4.0f;
                shooterEnemySpawnInterval = 2.0f;
            }
            else
            {
                basicEnemySpawnInterval = 2.0f;
                shooterEnemySpawnInterval = 4.0f;
            }
        }
    }
}