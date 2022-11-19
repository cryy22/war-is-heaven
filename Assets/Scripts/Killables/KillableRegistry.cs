using System;
using System.Collections.Generic;
using Singletons;

namespace WarIsHeaven.Killables
{
    public class KillableRegistry : SingletonBehaviour<KillableRegistry>
    {
        private readonly List<Killable> _killables = new();

        public event EventHandler Hovered;
        public event EventHandler Unhovered;
        public event EventHandler Clicked;
        public event EventHandler<ChangedEventArgs> Changed;
        public event EventHandler Killed;

        private void OnEnable()
        {
            foreach (Killable killable in _killables) Subscribe(killable);
        }

        private void OnDisable()
        {
            foreach (Killable killable in _killables) Unsubscribe(killable);
        }

        public void Register(Killable killable)
        {
            _killables.Add(killable);
            Subscribe(killable);
        }

        public void DisplayIndicators(bool display)
        {
            foreach (Killable killable in _killables) killable.SetIndicatorActive(display);
        }

        private void HoveredEventHandler(object sender, EventArgs e) { Hovered?.Invoke(sender: sender, e: e); }
        private void UnhoveredEventHandler(object sender, EventArgs e) { Unhovered?.Invoke(sender: sender, e: e); }
        private void ClickedEventHandler(object sender, EventArgs e) { Clicked?.Invoke(sender: sender, e: e); }
        private void ChangedEventHandler(object sender, ChangedEventArgs e) { Changed?.Invoke(sender: sender, e: e); }
        private void KilledEventHandler(object sender, EventArgs e) { Killed?.Invoke(sender: sender, e: e); }

        private void Subscribe(Killable killable)
        {
            killable.Hovered += HoveredEventHandler;
            killable.Unhovered += UnhoveredEventHandler;
            killable.Clicked += ClickedEventHandler;
            killable.Changed += ChangedEventHandler;
            killable.Killed += KilledEventHandler;
        }

        private void Unsubscribe(Killable killable)
        {
            killable.Hovered -= HoveredEventHandler;
            killable.Unhovered -= UnhoveredEventHandler;
            killable.Clicked -= ClickedEventHandler;
            killable.Changed -= ChangedEventHandler;
            killable.Killed -= KilledEventHandler;
        }
    }
}
