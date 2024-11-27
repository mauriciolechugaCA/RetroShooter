using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*
 * Represents a basic enemy that only moves horizontally, vertically and diagonally randomly.
 */

namespace RetroShooter.Entities.Enemies
{
    public class EnemyBasic : Enemy
    {
        private Vector2 direction;
        private Random random;

        public EnemyBasic(Vector2 startPosition, Texture2D texture) : base(startPosition, 20, 5.0f, 15, texture, 15)
        {
            random = new Random();
            direction = GetRandomDownwardDirection();
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

        private Vector2 GetRandomDownwardDirection()
        {
            float x = (float)random.NextDouble() * 2f - 1f;
            float y = ((float)random.NextDouble() * 0.5f) + 0.5f;
            Vector2 randomDirection = new Vector2(x, y);
            randomDirection.Normalize();
            return randomDirection;
        }

        public override int GetPoints()
        {
            return 15;
        }
    }
}