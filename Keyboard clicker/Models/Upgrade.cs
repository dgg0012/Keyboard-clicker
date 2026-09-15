using System;
using System.Collections.Generic;
using System.Text;

namespace Keyboard_clicker.Models
{
    class Upgrade
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int BasePrice { get; set; }

        public double PriceMultiplier { get; set; }

        public double IncomePerSecond { get; set; }

        public double Requirements { get; set; }

        public int Quantity { get; set; }


    }
}
