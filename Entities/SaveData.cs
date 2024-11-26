using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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