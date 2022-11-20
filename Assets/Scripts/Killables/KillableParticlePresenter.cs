using System;
using System.Collections;
using UnityEngine;

namespace WarIsHeaven.Killables
{
    public class KillableParticlePresenter : MonoBehaviour
    {
        [SerializeField] private ParticleSystem DamagedParticleSystemPrefab;
        [SerializeField] private ParticleSystem HealedParticleSystemPrefab;
        [SerializeField] private KillableRegistry KillableRegistry;

        private static readonly Vector3 _overlayModifier = new(x: 0, y: 0, z: -1);

        private void OnEnable() { KillableRegistry.Changed += ChangedEventHandler; }
        private void OnDisable() { KillableRegistry.Changed -= ChangedEventHandler; }

        private static IEnumerator DisplayParticles(ParticleSystem particles)
        {
            particles.Play();
            yield return new WaitUntil(() => particles.isPlaying == false);
            Destroy(particles.gameObject);
        }

        private void ChangedEventHandler(object sender, ChangedEventArgs e)
        {
            if (e.Delta == 0) return;
            ParticleSystem prefab = e.Delta switch
            {
                > 0 => HealedParticleSystemPrefab,
                < 0 => DamagedParticleSystemPrefab,
                _   => throw new ArgumentOutOfRangeException(),
            };

            var killable = (Killable) sender;
            ParticleSystem particles = Instantiate(
                original: prefab,
                position: killable.transform.position + _overlayModifier,
                rotation: Quaternion.identity,
                parent: transform
            );

            StartCoroutine(DisplayParticles(particles));
        }
    }
}
