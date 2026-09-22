using Keyboard_clicker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Keyboard_clicker.Services
{
    class LogService
    {
        private readonly List<LogEntry> _entries = new List<LogEntry>();

        public IReadOnlyList<LogEntry> Entries => _entries;

        public void Add(string message)
        {
            _entries.Add(new LogEntry
            {
                TimeStamp = DateTime.Now,
                Message = message,
            });
        }
    }
}
