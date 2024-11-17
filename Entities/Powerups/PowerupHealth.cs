using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

// Represents a health power-up that resotres one healt point to the player.

namespace RetroShooter.Entities.Powerups
{
    internal class PowerupHealth
    {
        public Vector2 Position { get; private set; }
        public float Duration { get; private set; }
        public PowerupHealth(Vector2 position, float duration = 0)
        {
            Position = position;
            Duration = duration;
        }
        public void ApplyEffect(Player player)
        {
            player.SetHealth(player.Health + 1);
        }
    }
}
