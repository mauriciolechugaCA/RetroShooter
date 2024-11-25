using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// Represents a health power-up that resotres one healt point to the player.

namespace RetroShooter.Entities.Powerups
{
    internal class PowerupHealth : Powerup
    {
        public float SpawnTime { get; set; }

        public PowerupHealth(Vector2 position, Texture2D texture, float spawnTime) : base(position, texture)
        {
            SpawnTime = spawnTime;
        }

        public override void ApplyEffect(Player player, GameTime gameTime)
        {
            player.SetHealth(player.Health + 30);
        }
    }
}