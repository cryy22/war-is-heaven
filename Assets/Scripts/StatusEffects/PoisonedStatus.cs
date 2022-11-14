using System;
using System.Collections;
using UnityEngine;
using WarIsHeaven.Killables;

namespace WarIsHeaven.StatusEffects
{
    [RequireComponent(typeof(Killable))]
    public class PoisonedStatus : MonoBehaviour
    {
        private Killable _target;

        private bool _isInitialized;

        public Killable ValueProvider { get; private set; }

        private void Awake() { ValueProvider = GetComponent<Killable>(); }
        private void OnEnable() { ValueProvider.Killed += KilledEventHandler; }
        private void OnDisable() { ValueProvider.Killed -= KilledEventHandler; }

        public void Initialize(Killable target, int initialValue = 1)
        {
            if (_isInitialized) throw new InvalidOperationException("PoisonedStatus already initialized");

            ValueProvider.Initialize(initialValue);
            _target = target;

            _isInitialized = true;
        }

        public void Invoke() { _target.ChangeValue(-ValueProvider.Value); }

        public IEnumerator Animate(float duration = 0.25f)
        {
            Vector3 initialScale = transform.localScale;
            transform.localScale *= 1.125f;

            yield return new WaitForSeconds(duration);

            transform.localScale = initialScale;
        }

        private void KilledEventHandler(object sender, EventArgs _) { Destroy(gameObject); }
    }
}
