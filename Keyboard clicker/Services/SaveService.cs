using Keyboard_clicker.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using Windows.Storage;

namespace Keyboard_clicker.Services
{
    class SaveService
    {
        private readonly string _manualSavePath;
        private readonly string _autoSavePath;

        public SaveService()
        {
            _manualSavePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "ManualSave.json");
            _autoSavePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "AutoSave.json");
        }

        public void ManualSave(GameState gameState)
        {
            Debug.WriteLine("ManualSave called");
            Debug.WriteLine($"Saving to: {_manualSavePath}");

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

            Debug.WriteLine("Save finished");
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

                SaveData? saveData =
                    JsonSerializer.Deserialize<SaveData>(jsonString);

                if (saveData == null)
                {
                    throw new Exception("Save file is empty or invalid.");
                }

                GameState gameState = new GameState
                {
                    Currency = saveData.Currency,
                    ClickValue = saveData.ClickValue,
                    Upgrades = saveData.Upgrades,
                    Automations = saveData.Automations,
                    Timers = saveData.Timers
                };

                return gameState;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Save file could not be loaded: {ex.Message}");

                throw;
            }
        }
    }
}
