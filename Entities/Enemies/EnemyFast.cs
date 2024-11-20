﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*
 * Represents a fast enemy that moves towards the player's last positon. No shooting behavior.
 */

namespace RetroShooter.Entities.Enemies
{
    internal class EnemyFast : Enemy
    {
        public EnemyFast(Vector2 startPosition, Texture2D enemyBulletTexture) : base(startPosition, 50, 3.0f, 25, enemyBulletTexture)
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
