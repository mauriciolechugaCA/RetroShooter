using System.Collections.Generic;
using RetroShooter.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroShooter.Entities.Projectiles;

/*
 * Manages the player’s attributes, movement, shooting, health, and interaction with power-ups.
 */

namespace RetroShooter.Managers
{
    public class PlayerManager
    {
        public Player Player { get; private set; }

        public PlayerManager(Vector2 startPosition, int health, Texture2D playerTexture, float scale, Game1 game, Texture2D playerDeathTexture, int frameCount, float frameSpeed)
        {
            Player = new Player(startPosition, health, playerTexture, scale, game, playerDeathTexture, frameCount, frameSpeed);
        }

        public void Update(InputManager inputManager, int screenWidth, int screenHeight, List<Projectile> projectiles, GameTime gameTime, Texture2D laserNormalTexture)
        {
            Player.Update(inputManager, screenWidth, screenHeight, projectiles, gameTime, laserNormalTexture);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Player.Draw(spriteBatch);
        }
    }
}