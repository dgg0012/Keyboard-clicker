using Keyboard_clicker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Keyboard_clicker.Services
{
    class GameService
    {
        private readonly GameState _gameState;

        public GameService(GameState gameState){
            _gameState = gameState;
        }

        public void Click()
        {
            _gameState.Currency += _gameState.ClickValue;
        }

        public void AddPassiveIncome(double deltaTime)
        {
            _gameState.Currency += _gameState.IncomePerSecond * deltaTime;
        }

    }
}
