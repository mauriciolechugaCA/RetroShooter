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
    public abstract class Powerup
    {
        public Vector2 Position { get; private set; }
        public Powerup(Vector2 position)
        {
            Position = position;
        }
        public abstract void ApplyEffect(Player player);
    }
}