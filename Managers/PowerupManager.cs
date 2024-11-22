using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RetroShooter.Entities.Powerups;
using RetroShooter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Manages the spawning, updating, and effects of power-ups in the game.
 * ## Generate power-ups at intervals or based on events (e.g., after an enemy is destroyed).
 * ## Detect when the player collects a power-up and apply the corresponding effect.
 * ## Track the duration of time-limited power-ups and handle expiration.
 */

namespace RetroShooter.Managers
{
    internal class PowerupManager
    {
        private List<Powerup> _powerups;
        private Random _random;
        private Texture2D _powerupHealthTexture;
        private Texture2D _powerupLaserTexture;
        private float _spawnInterval;
        private float _lastSpawnTime;

        public PowerupManager(Texture2D powerupHealthTexture, Texture2D powerupLaserTexture, float spawnInterval = 10f)
        {
            _powerups = new List<Powerup>();
            _random = new Random();
            _powerupHealthTexture = powerupHealthTexture;
            _powerupLaserTexture = powerupLaserTexture;
            _spawnInterval = spawnInterval;
            _lastSpawnTime = 0f;
        }

        public void Update(GameTime gameTime, Player player)
        {
            // Spawn power-ups at intervals
            if (gameTime.TotalGameTime.TotalSeconds - _lastSpawnTime > _spawnInterval)
            {
                SpawnPowerup();
                _lastSpawnTime = (float)gameTime.TotalGameTime.TotalSeconds;
            }

            // Update power-ups and check for collisions with the player
            for (int i = _powerups.Count - 1; i >= 0; i--)
            {
                var powerup = _powerups[i];
                if (player.Bounds.Intersects(new Rectangle((int)powerup.Position.X, (int)powerup.Position.Y, 32, 32)))
                {
                    powerup.ApplyEffect(player);
                    _powerups.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var powerup in _powerups)
            {
                Texture2D texture = powerup is PowerupHealth ? _powerupHealthTexture : _powerupLaserTexture;
                spriteBatch.Draw(texture, powerup.Position, Color.White);
            }
        }

        private void SpawnPowerup()
        {
            Vector2 position = new Vector2(_random.Next(0, 768), _random.Next(0, 1024));
            Powerup powerup;
            if (_random.Next(2) == 0)
            {
                powerup = new PowerupHealth(position);
            }
            else
            {
                powerup = new PowerupLaser(position);
            }
            _powerups.Add(powerup);
        }
    }
}