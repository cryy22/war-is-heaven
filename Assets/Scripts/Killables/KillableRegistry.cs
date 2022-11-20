using System;
using Crysc.Registries;
using UnityEngine;

namespace WarIsHeaven.Killables
{
    using IKillableRegistrar = IRegistrar<Killable>;

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

        protected override void SubscribeToEvents(IKillableRegistrar registrar)
        {
            base.SubscribeToEvents(registrar);

            registrar.Registrant.Hovered += HoveredEventHandler;
            registrar.Registrant.Unhovered += UnhoveredEventHandler;
            registrar.Registrant.Clicked += ClickedEventHandler;
            registrar.Registrant.Changed += ChangedEventHandler;
            registrar.Registrant.Killed += KilledEventHandler;
        }

        protected override void UnsubscribeFromEvents(IKillableRegistrar registrar)
        {
            base.UnsubscribeFromEvents(registrar);

            registrar.Registrant.Hovered -= HoveredEventHandler;
            registrar.Registrant.Unhovered -= UnhoveredEventHandler;
            registrar.Registrant.Clicked -= ClickedEventHandler;
            registrar.Registrant.Changed -= ChangedEventHandler;
            registrar.Registrant.Killed -= KilledEventHandler;
        }

        private void HoveredEventHandler(object sender, EventArgs e) { Hovered?.Invoke(sender: sender, e: e); }
        private void UnhoveredEventHandler(object sender, EventArgs e) { Unhovered?.Invoke(sender: sender, e: e); }
        private void ClickedEventHandler(object sender, EventArgs e) { Clicked?.Invoke(sender: sender, e: e); }
        private void ChangedEventHandler(object sender, ChangedEventArgs e) { Changed?.Invoke(sender: sender, e: e); }
        private void KilledEventHandler(object sender, EventArgs e) { Killed?.Invoke(sender: sender, e: e); }
    }
}
