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
        private float spawnTimer;
        private float spawnInterval;
        private List<Projectile> projectiles;

        public EnemyManager(Texture2D enemyTexture, Texture2D enemyBulletTexture, List<Projectile> projectiles)
        {
            this.enemyTexture = enemyTexture;
            this.enemyBulletTexture = enemyBulletTexture;
            this.projectiles = projectiles;
            enemies = new List<Enemy>();
            spawnInterval = 5.0f; // Cooldown between enemy spawns in seconds
            spawnTimer = 0f;
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
            spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (spawnTimer >= spawnInterval)
            {
                Random random = new Random();
                float xPosition = (float)random.NextDouble() * (768 - enemyTexture.Width); 
                SpawnEnemy(new Vector2(xPosition, 0), "Basic"); // Change enemy type here for testing
                spawnTimer = 0f;
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
    }
}