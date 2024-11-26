using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace RetroShooter.Managers
{
    public static class SaveManager
    {
        private static readonly string SaveFilePath = "savegame.json";

        public static void SaveGame(SaveData saveData)
        {
            var json = JsonSerializer.Serialize(saveData);
            File.WriteAllText(SaveFilePath, json);
        }

        public static SaveData LoadGame()
        {
            if (!File.Exists(SaveFilePath))
                return null;

            var json = File.ReadAllText(SaveFilePath);
            return JsonSerializer.Deserialize<SaveData>(json);
        }
    }
}