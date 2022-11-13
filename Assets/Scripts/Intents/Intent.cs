using System;
using UnityEngine;
using WarIsHeaven.Killables;

namespace WarIsHeaven.Intents
{
    [RequireComponent(typeof(Killable))]
    public class Intent : MonoBehaviour
    {
        private Killable _killable;

        private void Awake() { _killable = GetComponent<Killable>(); }
        private void OnEnable() { _killable.Killed += KilledEventHandler; }
        private void OnDisable() { _killable.Killed -= KilledEventHandler; }

        private void KilledEventHandler(object sender, EventArgs _) { Destroy(gameObject); }
    }
}
