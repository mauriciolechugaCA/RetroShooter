using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

// Represents a shield power-up that has a duration of 30 seconds.

namespace RetroShooter.Entities.Powerups
{
    internal class PowerupShield
    {
        public Vector2 Position { get; private set; }
        public float Duration { get; private set; }
        public PowerupShield(Vector2 position, float duration = 30)
        {
            Position = position;
            Duration = duration;
        }
        public void ApplyEffect(Player player)
        {
            player.SetHasShield(true);
        }
    }
}
