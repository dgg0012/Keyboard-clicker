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

            GameState gameState = new GameState();

            GameService gameService = new GameService(gameState);
            UpgradeService upgradeService = new UpgradeService(gameState);
            AutomationService automationService = new AutomationService(gameState, gameService);
            LogService logService = new LogService();

            _MainViewModel = new MainViewModel(
                gameState, gameService, saveService, upgradeService, automationService, logService);
        }

        private void Keyboard_Click(object sender, RoutedEventArgs e)
        {
            _MainViewModel.Click();

            CurrencyText.Text = $"{_MainViewModel.Currency} keys";
            IncomePerSecondText.Text = $"{_MainViewModel.IncomePerSecond} keys/s";
        }
    }
}
