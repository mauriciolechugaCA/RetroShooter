using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using RetroShooter.Entities.Projectiles;
using Microsoft.Xna.Framework;

/*
 * Represents a general enemy, with position, health, speed and points attributes.
 */

namespace RetroShooter.Entities.Enemies
{
    public abstract class Enemy
    {
        protected Vector2 position;
        protected int health;
        protected float speed;
        protected int damage;
        protected Texture2D texture;

        public bool IsAlive => health > 0;

        public Rectangle Bounds => new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

        protected Enemy(Vector2 startPosition, int health, float speed, int damage, Texture2D texture)
        {
            this.position = startPosition;
            this.health = health;
            this.speed = speed;
            this.damage = damage;
            this.texture = texture;
        }

        public abstract void Move(Player player);

        public void Update(GameTime gameTime, Player player)
        {
            Move(player);
            // Additional update logic (e.g., shooting, collision detection)
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void TakeDamage(int amount)
        {
            health -= amount;
        }
    }
}