using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

/*
 * Represents a shielded enemy that appears on screen, and shoots vertically and horizontally.
 * Shield blocks all damage until it is destroyed. Shield has a health value of 10.
 */

namespace RetroShooter.Entities.Enemies
{
    internal class EnemyShielded : Enemy
    {
        public int ShieldHealth { get; private set; }
        public EnemyShielded(Vector2 startPosition) : base(startPosition, 100, 1.0f, 50)
        {
            ShieldHealth = 10;
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
        public override void TakeDamage(int damage)
        {
            if (ShieldHealth > 0)
            {
                ShieldHealth -= damage;
            }
            else
            {
                Health -= damage;
            }
        }
        public override void Attack()
        {
            // Attack logic
            var projectile = new Projectile(Position, new Vector2(0, 1), 5, 1);
            projectiles.Add(projectile);
            projectile = new Projectile(Position, new Vector2(0, -1), 5, 1);
            projectiles.Add(projectile);
            projectile = new Projectile(Position, new Vector2(1, 0), 5, 1);
            projectiles.Add(projectile);
            projectile = new Projectile(Position, new Vector2(-1, 0), 5, 1);
            projectiles.Add(projectile);
        }
    }
}
