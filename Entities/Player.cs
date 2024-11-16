using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
 * Represents the player’s ship and manages all player-specific attributes and actions.
 */

namespace RetroShooter.Entities
{
    internal class Player
    {
        public Vector2 Position { get; private set; }
        public int Health { get; private set; }
        public int Score { get; private set; }
        public bool HasShield { get; private set; }
        public bool IsPowerLaserActive { get; private set; }
        private float speed;

        public Player(Vector2 startPosition, int health)
        {
            Position = startPosition;
            Health = health;
            Score = 0;
            HasShield = false;
            IsPowerLaserActive = false;
            speed = 5;
        }

        public void Move(Vector2 direction)
        {
            Position += direction * speed;
        }

        public void Shoot(List<Projectile> projectiles)
        {
            var projectile = new Projectile(Position, new Vector2(0, -1), 10, IsPowerLaserActive ? 2 : 1);
            projectiles.Add(projectile);
        }

        public void TakeDamage(int damage)
        {
            if (HasShield)
            {
                HasShield = false;
                return;
            }
            Health -= damage;
        }

        public void ApplyPowerUp(PowerUp powerUp)
        {
            switch (powerUp.Type)
            {
                case PowerUpType.Shield:
                    HasShield = true;
                    break;
                case PowerUpType.Laser:
                    IsPowerLaserActive = true;
                    break;
                case PowerUpType.Health:
                    Health += 1;
                    break;
            }
        }

        public void Update(int screenWidth, int screenHeight)
        {
            // Clamp player position to screen bounds
            Position = new Vector2(
                Math.Clamp(Position.X, 0, screenWidth),
                Math.Clamp(Position.Y, 0, screenHeight)
            );
        }
    }
}
