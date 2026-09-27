using Keyboard_clicker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Keyboard_clicker.Services
{
    class GameDataService
    {
        private readonly string _gameDataPath;

        public GameDataService()
        {
            _gameDataPath = Path.Combine(
                AppContext.BaseDirectory,
                "Data",
                "GameData.json"
                );
        }

        public GameData Load()
        {
            string jsonString = File.ReadAllText(_gameDataPath);

            GameData? gameData = JsonSerializer.Deserialize<GameData>(jsonString);

            return gameData ?? new GameData();
        }
    }
}
