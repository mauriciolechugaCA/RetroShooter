using System;
using Microsoft.Xna.Framework;

namespace RetroShooter.Managers
{
    [Serializable] // ##AI assisted ##
    public class SaveData
    {
        public Vector2 PlayerPosition { get; set; }
        public int PlayerHealth { get; set; }
        public int PlayerScore { get; set; }
        public GameScene CurrentScene { get; set; }
    }
}