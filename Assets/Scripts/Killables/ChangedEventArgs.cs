using System;

namespace WarIsHeaven.Killables
{
    public class ChangedEventArgs : EventArgs
    {
        public ChangedEventArgs(int delta) { Delta = delta; }
        public int Delta { get; }
    }
}
