using System;
using System.Collections.Generic;
using System.Text;

namespace Keyboard_clicker.Models
{
    class Automation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }
        public int Quantity { get; set; }

        public int Interval { get; set; }
        public int Elapsed { get; set; }

        public string AutomationType { get; set; }
    }
}
