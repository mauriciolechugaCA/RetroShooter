using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

// Represents a power-up that gives the player a power laser that does double damage

namespace RetroShooter.Entities.Powerups
{
    internal class PowerupLaser
    {
        public Vector2 Position { get; private set; }
        public float Duration { get; private set; }
        public PowerupLaser(Vector2 position, float duration = 0)
        {
            Position = position;
            Duration = duration;
        }
        public void ApplyEffect(Player player)
        {
            player.SetIsPowerLaserActive(true);
        }
    }
}
