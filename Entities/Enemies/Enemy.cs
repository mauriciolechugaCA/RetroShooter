using Microsoft.Xna.Framework.Graphics;
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
        public int CollisionDamage { get; set; }
        public bool IsAlive
        {
            get => health > 0;
            set
            {
                if (!value)
                {
                    health = 0;
                }
            }
        }

        public Rectangle Bounds => new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

        protected Enemy(Vector2 startPosition, int health, float speed, int damage, Texture2D texture, int collisionDamage)
        {
            this.position = startPosition;
            this.health = health;
            this.speed = speed;
            this.damage = damage;
            this.texture = texture;
            IsAlive = true;
            CollisionDamage = collisionDamage;
        }

        public abstract void Move(Player player);

        public virtual void Update(GameTime gameTime, Player player)
        {
            Move(player);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void TakeDamage(int amount, Player player)
        {
            health -= amount;

            if (health <= 0)
            {
                IsAlive = false;
                player.AddScore(GetPoints());
            }
        }

        public abstract int GetPoints();
    }
}