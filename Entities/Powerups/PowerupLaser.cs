using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

// Represents a power-up that gives the player a power laser that does double damage

namespace RetroShooter.Entities.Powerups
{
    internal class PowerupLaser : Powerup
    {
        public PowerupLaser(Vector2 position) : base(position) { }

        public override void ApplyEffect(Player player)
        {
            player.SetIsPowerLaserActive(true);
        }
    }
}