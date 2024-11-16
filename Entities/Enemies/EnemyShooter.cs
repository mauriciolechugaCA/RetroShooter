using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

/*
 * Represents a challenging enemy that moves slower but shoots homing projectiles towards the player at a higher rate.
 */

namespace RetroShooter.Entities.Enemies
{
    internal class EnemyShooter : Enemy
    {
        public EnemyShooter(Vector2 startPosition) : base(startPosition, 150, 0.75f, 100)
        {
        }
        public override void Move()
        {
            // Move logic to make them move towards the player's last position
            // Get the direction to the player's last position
            // Normalize the direction vector
            // Move the enemy towards the player's last position
            // If the enemy is close enough to the player's last position, stop moving
        }
    }
}
