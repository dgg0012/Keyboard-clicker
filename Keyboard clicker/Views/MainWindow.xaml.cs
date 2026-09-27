using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Keyboard_clicker.Models;
using Keyboard_clicker.Services;
using Keyboard_clicker.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace Keyboard_clicker
{
    public sealed partial class MainWindow : Window
    {
        private MainViewModel _MainViewModel;
        public MainWindow()
        {
            InitializeComponent();

            SaveService saveService = new SaveService();

            GameDataService gameDataService = new GameDataService();
            GameData gameData = gameDataService.Load();

            GameState gameState = new GameState()
            {
                Upgrades = gameData.Upgrades,
                Automations = gameData.Automations
            };

            GameService gameService = new GameService(gameState);
            UpgradeService upgradeService = new UpgradeService(gameState);
            AutomationService automationService = new AutomationService(gameState, gameService);
            LogService logService = new LogService();

            _MainViewModel = new MainViewModel(
                gameState, gameService, saveService, upgradeService, automationService, logService);

            grid.DataContext = _MainViewModel;
        }

        private void Keyboard_Click(object sender, RoutedEventArgs e)
        {
            _MainViewModel.Click();

            CurrencyText.Text = $"{_MainViewModel.Currency} keys";
            IncomePerSecondText.Text = $"{_MainViewModel.IncomePerSecond} keys/s";
        }

        private void UpgradesButton_Click(object sender, RoutedEventArgs e)
        {
            UpgradeList.Opacity = 1;
            AutomationsList.Opacity = 0;
        }

        private void AutomationsButton_Click(object sender, RoutedEventArgs e)
        {
            UpgradeList.Opacity = 0;
            AutomationsList.Opacity = 1;
        }

        private void BuyUpgrade_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Upgrade upgrade = (Upgrade)button.DataContext;
            System.Diagnostics.Debug.WriteLine(
        $"Clicked: {upgrade.Name}");

            _MainViewModel.BuyUpgrade(upgrade);
        }

        private void BuyAutomation_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Automation automation = (Automation)button.DataContext;
            _MainViewModel.BuyAutomation(automation);
        }
    }
}
