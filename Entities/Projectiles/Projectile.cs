using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
 * Represents bullets or other projectiles fired by the player or enemies.
 */

namespace RetroShooter.Entities.Projectiles
{
    internal class Projectile
    {
        public Vector2 Position { get; private set; }
        public Vector2 Direction { get; private set; }
        public float Speed { get; private set; }
        public int Damage { get; private set; }

        public Projectile(Vector2 position, Vector2 direction, float speed, int damage)
        {
            Position = position;
            Direction = direction;
            Speed = speed;
            Damage = damage;
        }

        public void Update()
        {
            Position += Direction * Speed;
        }

        public bool IsOutOfBounds(Vector2 screenWidth, Vector2 screenHeight)
        {
            return Position.X < screenWidth.X || Position.X > screenHeight.X || Position.Y < screenWidth.Y || Position.Y > screenHeight.Y;
        }
    }
}
