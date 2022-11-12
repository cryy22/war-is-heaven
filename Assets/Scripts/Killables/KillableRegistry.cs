using System;
using System.Collections.Generic;
using UnityEngine;

namespace WarIsHeaven.Killables
{
    public class KillableRegistry : MonoBehaviour
    {
        private readonly List<Killable> _killables = new();

        public event EventHandler Clicked;
        public event EventHandler Killed;

        public static KillableRegistry Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

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
            foreach (Killable killable in _killables) killable.DisplayIndicator(display);
        }

        private void ClickedEventHandler(object sender, EventArgs e) { Clicked?.Invoke(sender: sender, e: e); }
        private void KilledEventHandler(object sender, EventArgs e) { Killed?.Invoke(sender: sender, e: e); }

        private void Subscribe(Killable killable)
        {
            killable.Clicked += ClickedEventHandler;
            killable.Killed += KilledEventHandler;
        }

        private void Unsubscribe(Killable killable)
        {
            killable.Clicked -= ClickedEventHandler;
            killable.Killed -= KilledEventHandler;
        }
    }
}
