using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

/*
 * Represents bullets or other projectiles fired by the player or enemies.
 */

namespace RetroShooter.Entities.Projectiles
{
    public class Projectile
    {
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public int Damage { get; set; }

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

        public bool IsOutOfBounds(int screenWidth, int screenHeight)
        {
            return Position.X < 0 ||
                   Position.X > screenWidth ||
                   Position.Y < 0 ||
                   Position.Y > screenHeight;
        }
    }
}