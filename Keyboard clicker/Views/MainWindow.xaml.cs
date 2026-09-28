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
using Microsoft.UI.Dispatching;
using Windows.Storage;

namespace Keyboard_clicker
{
    public sealed partial class MainWindow : Window
    {
        private MainViewModel _MainViewModel;
        private DispatcherQueueTimer _gameTimer;

        public MainWindow()
        {
            InitializeComponent();

            SaveService saveService = new SaveService();

            GameDataService gameDataService = new GameDataService();
            GameData gameData = gameDataService.Load();

            GameState gameState;
            string savePath = Path.Combine(
            ApplicationData.Current.LocalFolder.Path,
            "ManualSave.json");

            if (File.Exists(savePath))
            {
                gameState = saveService.Load(savePath);
            }
            else
            {
                gameState = new GameState()
                {
                    Upgrades = gameData.Upgrades,
                    Automations = gameData.Automations
                };
            }

            GameService gameService = new GameService(gameState);
            UpgradeService upgradeService = new UpgradeService(gameState);
            upgradeService.RecalculateIncomePerSecond();
            AutomationService automationService = new AutomationService(gameState, gameService);
            LogService logService = new LogService();


            _MainViewModel = new MainViewModel(
                gameState, gameService, saveService, upgradeService, automationService, logService);

            grid.DataContext = _MainViewModel;

            _gameTimer = DispatcherQueue.GetForCurrentThread().CreateTimer();
            _gameTimer.Interval = TimeSpan.FromMilliseconds(100);
            _gameTimer.Tick += GameTimer_Tick;
            _gameTimer.Start();
        }

        private void Keyboard_Click(object sender, RoutedEventArgs e)
        {
            _MainViewModel.Click();
        }
        private void GameTimer_Tick(DispatcherQueueTimer sender, object args)
        {
            _MainViewModel.Update(0.1);
        }

        private void UpgradesButton_Click(object sender, RoutedEventArgs e)
        {
            UpgradeList.Visibility = Visibility.Visible;
            AutomationsList.Visibility = Visibility.Collapsed;
        }

        private void AutomationsButton_Click(object sender, RoutedEventArgs e)
        {
            UpgradeList.Visibility = Visibility.Collapsed;
            AutomationsList.Visibility = Visibility.Visible;
        }

        private void BuyUpgrade_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Upgrade upgrade = (Upgrade)button.DataContext;
            _MainViewModel.BuyUpgrade(upgrade);

            UpgradeList.ItemsSource = null;
            UpgradeList.ItemsSource = _MainViewModel.Upgrades;

            LogList.ItemsSource = null;
            LogList.ItemsSource = _MainViewModel.Logs;
        }

        private void BuyAutomation_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Automation automation = (Automation)button.DataContext;
            _MainViewModel.BuyAutomation(automation);

            LogList.ItemsSource = null;
            LogList.ItemsSource = _MainViewModel.Logs;
        }

        private void SaveData_Click(object sender, RoutedEventArgs e)
        {
            _MainViewModel.Save();
        }
    }
}
