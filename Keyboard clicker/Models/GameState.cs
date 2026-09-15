using System;
using System.Collections.Generic;
using System.Text;

namespace Keyboard_clicker.Models
{
    class GameState
    {
        public double Currency { get; set; }

        public double ClickValue { get; set; }

        public List<Upgrade> Upgrades { get; set; }

        public List<Automation> Automations { get; set; }

        public List<TimerState> Timers { get; set; }
    }
}
