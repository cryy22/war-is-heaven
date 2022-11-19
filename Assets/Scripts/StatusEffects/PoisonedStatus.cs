using System;
using System.Collections;
using Crysc.Initialization;
using UnityEngine;
using WarIsHeaven.Killables;

namespace WarIsHeaven.StatusEffects
{
    [RequireComponent(typeof(Killable))]
    public class PoisonedStatus : InitializationBehaviour<StatusEffectConfig>
    {
        public Killable ValueProvider { get; private set; }
        private Killable Target => InitParams.Target;

        private void Awake() { ValueProvider = GetComponent<Killable>(); }
        private void OnEnable() { ValueProvider.Killed += KilledEventHandler; }
        private void OnDisable() { ValueProvider.Killed -= KilledEventHandler; }

        public override void Initialize(StatusEffectConfig initParams)
        {
            base.Initialize(initParams);
            ValueProvider.Initialize(initParams.InitialValue);
        }

        public void Invoke() { Target.ChangeValue(-ValueProvider.Value); }

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
