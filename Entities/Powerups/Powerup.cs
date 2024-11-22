using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;

/*
 * Represents a base collectible power-ups (e.g., shield, health regeneration, power laser).
 */

namespace RetroShooter.Entities.Powerups
{
    public enum PowerUpType
    {
        HealthRegeneration,
        PowerLaser
    }

    public class Powerup
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
                case PowerUpType.HealthRegeneration:
                    player.SetHealth(player.Health + 1); // Assuming 1 is the health increment
                    break;
                case PowerUpType.PowerLaser:
                    player.SetIsPowerLaserActive(true);
                    break;
            }
        }
    }
}
