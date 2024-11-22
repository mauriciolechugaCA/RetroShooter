using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RetroShooter.Entities.Enemies;
using RetroShooter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public EnemyManager(Texture2D enemyTexture, Texture2D enemyBulletTexture)
        {
            this.enemyTexture = enemyTexture;
            this.enemyBulletTexture = enemyBulletTexture;
            enemies = new List<Enemy>();
        }

        public void SpawnEnemy(Vector2 position, string enemyType)
        {
            switch (enemyType)
            {
                case "Basic":
                    enemies.Add(new EnemyBasic(position, enemyTexture));
                    break;
                case "Shooter":
                    enemies.Add(new EnemyShooter(position, enemyBulletTexture));
                    break;
                default:
                    throw new ArgumentException("Unknown enemy type");
            }
        }

        public void Update(GameTime gameTime, Player player)
        {
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