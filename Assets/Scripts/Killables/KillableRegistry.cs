using System;
using Crysc.Registries;
using UnityEngine;

namespace WarIsHeaven.Killables
{
    [CreateAssetMenu(fileName = "KillableRegistry", menuName = "Registries/Killable")]
    public class KillableRegistry : Registry<Killable>
    {
        public event EventHandler Hovered;
        public event EventHandler Unhovered;
        public event EventHandler Clicked;
        public event EventHandler<ChangedEventArgs> Changed;
        public event EventHandler Killed;

        public void DisplayIndicators(bool display)
        {
            foreach (Killable killable in Members) killable.SetIndicatorActive(display);
        }

        protected override void SubscribeToEvents(Killable killable)
        {
            base.SubscribeToEvents(killable);

            killable.Hovered += HoveredEventHandler;
            killable.Unhovered += UnhoveredEventHandler;
            killable.Clicked += ClickedEventHandler;
            killable.Changed += ChangedEventHandler;
            killable.Killed += KilledEventHandler;
        }

        protected override void UnsubscribeFromEvents(Killable killable)
        {
            base.UnsubscribeFromEvents(killable);

            killable.Hovered -= HoveredEventHandler;
            killable.Unhovered -= UnhoveredEventHandler;
            killable.Clicked -= ClickedEventHandler;
            killable.Changed -= ChangedEventHandler;
            killable.Killed -= KilledEventHandler;
        }

        private void HoveredEventHandler(object sender, EventArgs e) { Hovered?.Invoke(sender: sender, e: e); }
        private void UnhoveredEventHandler(object sender, EventArgs e) { Unhovered?.Invoke(sender: sender, e: e); }
        private void ClickedEventHandler(object sender, EventArgs e) { Clicked?.Invoke(sender: sender, e: e); }
        private void ChangedEventHandler(object sender, ChangedEventArgs e) { Changed?.Invoke(sender: sender, e: e); }
        private void KilledEventHandler(object sender, EventArgs e) { Killed?.Invoke(sender: sender, e: e); }
    }
}
