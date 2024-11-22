using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

// Represents a health power-up that resotres one healt point to the player.

namespace RetroShooter.Entities.Powerups
{
    internal class PowerupHealth : Powerup
    {
        public PowerupHealth(Vector2 position) : base(position) { }

        public override void ApplyEffect(Player player)
        {
            player.SetHealth(player.Health + 1);
        }
    }
}