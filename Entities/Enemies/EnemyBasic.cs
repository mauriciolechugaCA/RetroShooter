using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*
 * Represents a basic enemy that only moves horizontally, vertically and diagonally randomly.
 */

namespace RetroShooter.Entities.Enemies
{
    public class EnemyBasic : Enemy
    {
        public EnemyBasic(Vector2 startPosition, Texture2D texture) : base(startPosition, 100, 1.0f, 50, texture)
        {
        }

        public override void Move(Player player)
        {
            // Basic movement logic (e.g., move towards the player)
            Vector2 direction = player.Position - position;
            direction.Normalize();
            position += direction * speed;
        }
    }
}