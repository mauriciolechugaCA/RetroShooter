using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
 * Represents a base collectible power-ups (e.g., shield, health regeneration, power laser).
 */

namespace RetroShooter.Entities.Powerups
{
    internal enum PowerUpType
    {
        Shield,
        HealthRegeneration,
        PowerLaser
    }

    internal class Powerup
    {
        public Vector2 Position { get; private set; }
        public PowerUpType Type { get; private set; }
        public float Duration { get; private set; }


        public Powerup(Vector2 position, PowerUpType type, float duration = 0)
        {
            Position = position;
            Type = type;
            Duration = duration;
        }

        public void ApplyEffect(Player player)
        {
            switch (Type)
            {
                case PowerUpType.Shield:
                    player.SetHasShield(true);
                    break;
                case PowerUpType.HealthRegeneration:
                    player.SetHealth(player.Health + 50); // Assuming 50 is the health increment
                    break;
                case PowerUpType.PowerLaser:
                    player.SetIsPowerLaserActive(true);
                    break;
            }
        }
    }
}
