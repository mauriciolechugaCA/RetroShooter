using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
 * Represents a basic enemy that only moves horizontally, vertically and diagonally randomly.
 */

namespace RetroShooter.Entities.Enemies
{
    internal class EnemyBasic : Enemy
    {
        public EnemyBasic(Vector2 startPosition) : base(startPosition, 100, 1.0f, 10)
        {
        }
        public override void Move()
        {
            // Move logic to make them move horizontally, vertically and diagonally randomly

            // Randomly choose a direction to move in
            Random random = new Random();
            int direction = random.Next(0, 4);

            // Move the enemy in the chosen direction
            switch (direction)
            {
                case 0:
                    Position += new Vector2(1, 0) * Speed;
                    break;
                case 1:
                    Position += new Vector2(-1, 0) * Speed;
                    break;
                case 2:
                    Position += new Vector2(0, 1) * Speed;
                    break;
                case 3:
                    Position += new Vector2(0, -1) * Speed;
                    break;
            }

            // If the enemy is close to the edge of the screen, change direction
            if (Position.X < 0 || Position.X > 800 || Position.Y < 0 || Position.Y > 600)
            {
                Position = new Vector2(400, 300);
            }
        }
    }
}
