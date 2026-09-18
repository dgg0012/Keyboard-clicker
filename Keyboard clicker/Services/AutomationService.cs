using Keyboard_clicker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Keyboard_clicker.Services
{
    class AutomationService
    {
        private readonly GameState _gameState;
        private readonly GameService _gameService;

        public AutomationService(GameState gameState, GameService gameService)
        {
            _gameState = gameState;
            _gameService = gameService;
        }

        public bool CanBuy(Automation automation)
        {
            return _gameState.Currency >= automation.Price;
        }

        public bool BuyAutomation(Automation automation)
        {
            if (!CanBuy(automation)){
                return false;
            }
            else
            {
                _gameState.Currency -= automation.Price;
                automation.Quantity++;
                return true;
            }
        }

        public void Update(double deltaTime)
        {
            foreach (var automation in _gameState.Automations)
            {
                if (automation.Quantity <= 0)
                {
                    continue;
                }
                automation.Elapsed += deltaTime;

                if (automation.Elapsed >= automation.Interval)
                {
                    ExecuteAutomation(automation);

                    automation.Elapsed -= automation.Interval;
                }
            }
        }

        public void ExecuteAutomation(Automation automation)
        {
            if (automation.AutomationType == "Click")
            {
                _gameService.Click();
            }
        }
    }
}
