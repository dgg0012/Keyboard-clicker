using Keyboard_clicker.Models;
using Keyboard_clicker.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Keyboard_clicker.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private readonly GameState _gameState;
        private readonly GameService _gameService;
        private readonly SaveService _saveService;
        private readonly UpgradeService _upgradeService;
        private readonly AutomationService _automationService;
        private readonly LogService _logService;

        public IReadOnlyList<LogEntry> Logs => _logService.Entries;

        public double Currency => _gameState.Currency;
        public List<Upgrade> Upgrades => _gameState.Upgrades;
        public List<Automation> Automations => _gameState.Automations;

        public double IncomePerSecond => _gameState.IncomePerSecond;

        public MainViewModel(GameState gameState, GameService gameService, SaveService saveService, UpgradeService upgradeService, AutomationService automationService, LogService logService)
        {
            _gameState = gameState;
            _gameService = gameService;
            _saveService = saveService;
            _upgradeService = upgradeService;
            _automationService = automationService;
            _logService = logService;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }

        public void Click()
        {
            _gameService.Click();
            OnPropertyChanged(nameof(Currency));
        }

        public void BuyUpgrade(Upgrade upgrade)
        {
            if (_upgradeService.BuyUpgrade(upgrade))
            {
                _logService.Add($"Bought {upgrade.Name}");
                OnPropertyChanged(nameof(Upgrades));
                OnPropertyChanged(nameof(Currency));
                OnPropertyChanged(nameof(IncomePerSecond));
                OnPropertyChanged(nameof(Logs));
            }
        }

        public void BuyAutomation(Automation automation)
        {
            if (_automationService.BuyAutomation(automation))
            {
                _logService.Add($"Bought {automation.Name}");
                OnPropertyChanged(nameof(Automations));
                OnPropertyChanged(nameof(Currency));
                OnPropertyChanged(nameof(IncomePerSecond));
                OnPropertyChanged(nameof(Logs));
            }
        }

        public void Save()
        {
            _saveService.ManualSave(_gameState);
        }

        

    }
}
