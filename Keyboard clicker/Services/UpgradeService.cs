using Keyboard_clicker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Keyboard_clicker.Services
{
    class UpgradeService
    {
        private readonly GameState _gameState;

        public UpgradeService(GameState gameState)
        {
            _gameState = gameState;
        }

        public double GetPrice(Upgrade upgrade)
        {
            return upgrade.BasePrice * Math.Pow(upgrade.PriceMultiplier, upgrade.Quantity);
        }

        public bool CanBuy(Upgrade upgrade)
        {
            double price = GetPrice(upgrade);
            return _gameState.Currency >= price;
        }
        public bool BuyUpgrade(Upgrade upgrade)
        {
            double price = GetPrice(upgrade);

            if (!CanBuy(upgrade))
            {
                return false;
            }
            else
            {
                _gameState.Currency -= price;
                upgrade.Quantity++;
                return true;
            }
        }
    }
}
