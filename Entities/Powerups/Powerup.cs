using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*
 * Represents a base collectible power-ups (e.g., shield, health regeneration, power laser).
 */

namespace RetroShooter.Entities.Powerups
{
    public abstract class Powerup
    {
        public Vector2 Position { get; private set; }
        public bool IsAlive { get; set; } = true;
        private Texture2D texture;
        public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);

        public Powerup(Vector2 position, Texture2D texture)
        {
            Position = position;
            this.texture = texture;
        }
        public abstract void ApplyEffect(Player player, GameTime gameTime);
    }
}