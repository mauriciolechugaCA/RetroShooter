using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*
 * Represents a challenging enemy that moves slower but shoots homing projectiles towards the player at a higher rate.
 */

namespace RetroShooter.Entities.Enemies
{
    public class EnemyShooter : Enemy
    {
        public EnemyShooter(Vector2 startPosition, Texture2D enemyBulletTexture) : base(startPosition, 150, 0.75f, 100, enemyBulletTexture)
        {
        }

        public override void Move(Player player)
        {
            // Move logic to make them move towards the player's last position
            Vector2 direction = player.Position - position;
            direction.Normalize();
            position += direction * speed;
        }

        // Additional shooting logic can be added here
    }
}