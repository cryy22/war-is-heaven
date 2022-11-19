using System;
using System.Collections.Generic;
using Singletons;

namespace WarIsHeaven.Intents
{
    public class IntentRegistry : SingletonBehaviour<IntentRegistry>
    {
        private readonly List<Intent> _intents = new();

        public event EventHandler Hovered;
        public event EventHandler Unhovered;

        private void OnEnable()
        {
            foreach (Intent intent in _intents) Subscribe(intent);
        }

        private void OnDisable()
        {
            foreach (Intent intent in _intents) Unsubscribe(intent);
        }

        public void Register(Intent intent)
        {
            _intents.Add(intent);
            Subscribe(intent);
        }

        private void HoveredEventHandler(object sender, EventArgs e) { Hovered?.Invoke(sender: sender, e: e); }
        private void UnhoveredEventHandler(object sender, EventArgs e) { Unhovered?.Invoke(sender: sender, e: e); }

        private void Subscribe(Intent intent)
        {
            intent.Hovered += HoveredEventHandler;
            intent.Unhovered += UnhoveredEventHandler;
        }

        private void Unsubscribe(Intent intent)
        {
            intent.Hovered -= HoveredEventHandler;
            intent.Unhovered -= UnhoveredEventHandler;
        }
    }
}
