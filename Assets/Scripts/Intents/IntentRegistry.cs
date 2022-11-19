using System;
using Crysc.Registries;

namespace WarIsHeaven.Intents
{
    public class IntentRegistry : Registry<Intent, IntentRegistry>
    {
        public event EventHandler Hovered;
        public event EventHandler Unhovered;

        protected override void SubscribeToEvents(Intent intent)
        {
            base.SubscribeToEvents(intent);

            intent.Hovered += HoveredEventHandler;
            intent.Unhovered += UnhoveredEventHandler;
        }

        protected override void UnsubscribeFromEvents(Intent intent)
        {
            base.UnsubscribeFromEvents(intent);

            intent.Hovered -= HoveredEventHandler;
            intent.Unhovered -= UnhoveredEventHandler;
        }

        private void HoveredEventHandler(object sender, EventArgs e) { Hovered?.Invoke(sender: sender, e: e); }
        private void UnhoveredEventHandler(object sender, EventArgs e) { Unhovered?.Invoke(sender: sender, e: e); }
    }
}
