using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/*
 * Represents bullets or other projectiles fired by the player or enemies.
 */

namespace RetroShooter.Entities.Projectiles
{
    public enum ProjectileOwner
    {
        Player,
        Enemy
    }

    public class Projectile
    {
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public int Damage { get; set; }
        private Texture2D texture;
        public bool IsAlive { get; set; } = true;
        public ProjectileOwner Owner { get; set; }

        public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);

        public Projectile(Vector2 position, Vector2 direction, float speed, int damage, Texture2D texture, ProjectileOwner owner)
        {
            Position = position;
            Direction = direction;
            Speed = speed;
            Damage = damage;
            this.texture = texture;
            Owner = owner;
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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}