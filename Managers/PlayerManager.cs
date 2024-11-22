using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroShooter.Entities;
using RetroShooter.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroShooter.Entities.Projectiles;

/*
 * Manages the player’s attributes, movement, shooting, health, and interaction with power-ups.
 * ## Store and update player stats like health, score, and power-up effects.
 * ## Handle player-specific logic (movement, firing, collision detection).
 * ## Communicate with other managers, such as PowerUpManager or EnemyManager.
 */

namespace RetroShooter.Managers
{
    public class PlayerManager
    {
        public Player Player { get; private set; }

        public PlayerManager(Vector2 startPosition, int health, Texture2D playerTexture, float scale)
        {
            Player = new Player(startPosition, health, playerTexture, scale);
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