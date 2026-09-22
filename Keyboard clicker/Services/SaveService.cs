using Keyboard_clicker.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Keyboard_clicker.Services
{
    class SaveService
    {
        private readonly string _manualSavePath;
        private readonly string _autoSavePath;

        public SaveService()
        {
            _manualSavePath = Path.Combine(AppContext.BaseDirectory, "Data", "ManualSave.json");
            _autoSavePath = Path.Combine(AppContext.BaseDirectory, "Data", "AutoSave.json");
        }

        public void ManualSave(GameState gameState)
        {
            SaveData saveData = new SaveData
            {
                TimeStamp = DateTime.Now,
                Currency = gameState.Currency,
                ClickValue = gameState.ClickValue,
                Upgrades = gameState.Upgrades,
                Automations = gameState.Automations,
                Timers = gameState.Timers,
            };
            string jsonString = JsonSerializer.Serialize(saveData);
            File.WriteAllText(_manualSavePath, jsonString);
        }

        public void AutoSave(GameState gameState)
        {
            SaveData saveData = new SaveData
            {
                TimeStamp = DateTime.Now,
                Currency = gameState.Currency,
                ClickValue = gameState.ClickValue,
                Upgrades = gameState.Upgrades,
                Automations = gameState.Automations,
                Timers = gameState.Timers,
            };
            string jsonString = JsonSerializer.Serialize(saveData);
            File.WriteAllText(_autoSavePath, jsonString);
        }

        public GameState Load(string savePath)
        {
            try
            {
                string jsonString = File.ReadAllText(savePath);
                GameState? gameState = JsonSerializer.Deserialize<GameState>(jsonString);
                return gameState ?? new GameState();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new GameState();
            }
        }
    }
}
