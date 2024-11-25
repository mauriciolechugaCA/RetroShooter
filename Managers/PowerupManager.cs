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
        private enum PowerupType
        {
            Health,
            Laser
        }

        private List<Powerup> _powerups;
        private Random _random;
        private Texture2D _powerupHealthTexture;
        private Texture2D _powerupLaserTexture;
        private float _spawnInterval;
        private float _lastSpawnTime;
        private float _lastLaserSpawnTime;
        private int _lastScore;
        private const int SCORE_THRESHOLD = 500;
        private const float HEALTH_POWERUP_DURATION = 5f;
        private const float LASER_POWERUP_DURATION = 5f;
        private const int LASER_SCORE_THRESHOLD = 250;

        public PowerupManager(Texture2D powerupHealthTexture, Texture2D powerupLaserTexture, float spawnInterval = 30f) // Initial interval between spawns
        {
            _powerups = new List<Powerup>();
            _random = new Random();
            _powerupHealthTexture = powerupHealthTexture;
            _powerupLaserTexture = powerupLaserTexture;
            _spawnInterval = spawnInterval;
            _lastSpawnTime = 0f;
            _lastLaserSpawnTime = 0f;
            _lastScore = 0;
        }

        public List<Powerup> Powerups => _powerups;

        public void Update(GameTime gameTime, Player player)
        {
            // Spawn health power-ups at intervals
            if ((gameTime.TotalGameTime.TotalSeconds - _lastSpawnTime > _spawnInterval || player.Score - _lastScore >= SCORE_THRESHOLD) && !_powerups.OfType<PowerupHealth>().Any())
            {
                SpawnPowerup(gameTime, PowerupType.Health);
                _lastSpawnTime = (float)gameTime.TotalGameTime.TotalSeconds;
                _lastScore = player.Score;
            }

            // Spawn laser power-ups at intervals or based on score
            if ((gameTime.TotalGameTime.TotalSeconds - _lastLaserSpawnTime > _spawnInterval || player.Score - _lastScore >= LASER_SCORE_THRESHOLD) && !_powerups.OfType<PowerupLaser>().Any())
            {
                SpawnPowerup(gameTime, PowerupType.Laser);
                _lastLaserSpawnTime = (float)gameTime.TotalGameTime.TotalSeconds;
                _lastScore = player.Score;
            }

            // Update power-ups and check for collisions with the player
            for (int i = _powerups.Count - 1; i >= 0; i--)
            {
                var powerup = _powerups[i];

                if (powerup is PowerupHealth healthPowerup && gameTime.TotalGameTime.TotalSeconds - healthPowerup.SpawnTime > HEALTH_POWERUP_DURATION)
                {
                    _powerups.RemoveAt(i);
                    continue;
                }

                if (powerup is PowerupLaser laserPowerup && gameTime.TotalGameTime.TotalSeconds - laserPowerup.SpawnTime > LASER_POWERUP_DURATION)
                {
                    _powerups.RemoveAt(i);
                    continue;
                }

                if (player.Bounds.Intersects(new Rectangle((int)powerup.Position.X, (int)powerup.Position.Y, 32, 32)))
                {
                    powerup.ApplyEffect(player, gameTime);
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

        private void SpawnPowerup(GameTime gameTime, PowerupType type)
        {
            // ##AI assisted##
            // Randomly generate a position for the power-up within the game bounds (excluding the top quarter)
            Vector2 position = new(
                _random.Next(30, 768 - 30),
                _random.Next(768 / 4, 768 - 30)
            );

            Powerup powerup;
            if (type == PowerupType.Health)
            {
                powerup = new PowerupHealth(position, _powerupHealthTexture, (float)gameTime.TotalGameTime.TotalSeconds);
            }
            else
            {
                position = new(
                    _random.Next(30, 768 - 30),
                    _random.Next(768 / 4, 768 - 30)
                );
                powerup = new PowerupLaser(position, _powerupLaserTexture, (float)gameTime.TotalGameTime.TotalSeconds);
            }
            _powerups.Add(powerup);
        }
    }
}