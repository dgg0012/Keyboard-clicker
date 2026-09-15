using System;
using System.Collections.Generic;
using System.Text;

namespace Keyboard_clicker.Models
{
    class TimerState
    {
        public string Id { get; set; }

        public int Interval { get; set; }

        public int Elapsed { get; set; }

        public bool IsRunning { get; set; }
    }
}
