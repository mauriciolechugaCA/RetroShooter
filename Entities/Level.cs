using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroShooter.Entities.Enemies;

/*
 * Level class that can mange enemies, spawning and progression logic.
 */

namespace RetroShooter.Entities
{
    internal class Level
    {
        public int LevelNumber { get; private set; }
        private List<Enemy> enemies;
        private float spawnRate;
        private float lastSpawnTime;
        private Texture2D enemyBulletTexture;

        public Level(int levelNumber, float spawnRate, Texture2D enemyBulletTexture)
        {
            LevelNumber = levelNumber;
            this.spawnRate = spawnRate;
            this.enemyBulletTexture = enemyBulletTexture;
            enemies = new List<Enemy>();
        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalSeconds - lastSpawnTime > spawnRate)
            {
                SpawnEnemy();
                lastSpawnTime = (float)gameTime.TotalGameTime.TotalSeconds;
            }

            foreach (var enemy in enemies)
            {
                enemy.Move();

                // Add logic for enemy attack
                enemy.Attack();

                if (enemy.IsDead())
                {
                    enemies.Remove(enemy);
                }

                // Add logic for enemy collision with player
            }
        }

        private void SpawnEnemy()
        {
            // Randomly choose a position to spawn the enemy
            Random random = new Random();
            int x = random.Next(0, 800); // TODO: check actual boundaries
            int y = random.Next(0, 600); // TODO: check actual boundaries
            Vector2 position = new Vector2(x, y);

            // Randomly choose an enemy type to spawn depending on the level number
            Enemy enemy = null;
            switch (LevelNumber)
            {
                case 1:
                    enemy = new EnemyBasic(position, enemyBulletTexture);
                    break;
                case 2:
                    enemy = new EnemyShooter(position, enemyBulletTexture);
                    break;
            }

            enemies.Add(enemy);

            // TODO: Add logic for enemy collision with player
        }
    }
}
