using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// Represents a power-up that gives the player a power laser that does double damage

namespace RetroShooter.Entities.Powerups
{
    internal class PowerupLaser : Powerup
    {
        public float SpawnTime { get; set; }

        public PowerupLaser(Vector2 position, Texture2D texture, float spawnTime) : base(position, texture)
        {
            SpawnTime = spawnTime;
        }

        public override void ApplyEffect(Player player, GameTime gameTime)
        {
            player.SetIsPowerLaserActive(true);
            player.SetLaserEffectStartTime((float)gameTime.TotalGameTime.TotalSeconds);
        }
    }
}