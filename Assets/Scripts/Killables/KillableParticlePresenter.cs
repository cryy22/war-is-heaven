using System;
using UnityEngine;

namespace WarIsHeaven.Killables
{
    public class KillableParticlePresenter : MonoBehaviour
    {
        [SerializeField] private ParticleSystem DamagedParticleSystem;
        [SerializeField] private ParticleSystem HealedParticleSystem;

        private static readonly Vector3 _overlayModifier = new(x: 0, y: 0, z: -1);

        private void OnEnable() { KillableRegistry.Instance.Changed += ChangedEventHandler; }
        private void OnDisable() { KillableRegistry.Instance.Changed -= ChangedEventHandler; }

        private void ChangedEventHandler(object sender, ChangedEventArgs e)
        {
            if (e.Delta == 0) return;
            ParticleSystem activeParticleSystem = e.Delta switch
            {
                > 0 => HealedParticleSystem,
                < 0 => DamagedParticleSystem,
                _   => throw new ArgumentOutOfRangeException(),
            };

            var killable = (Killable) sender;
            activeParticleSystem.transform.position = killable.transform.position + _overlayModifier;
            activeParticleSystem.Play();
        }
    }
}
