using System;
using System.Collections.Generic;
using System.Text;

namespace Keyboard_clicker.Models
{
    class GameData
    {
        public List<Upgrade> Upgrades { get; set; } = new();
        public List<Automation> Automations { get; set; } = new();

    }
}
