using System;
using UnityEngine;

namespace WarIsHeaven.Killables
{
    public class KillableParticlePresenter : MonoBehaviour
    {
        [SerializeField] private ParticleSystem ParticleSystem;

        private static readonly Vector3 _overlayModifier = new(x: 0, y: 0, z: -1);

        private void OnEnable() { KillableRegistry.Instance.Damaged += DamagedEventHandler; }
        private void OnDisable() { KillableRegistry.Instance.Damaged -= DamagedEventHandler; }

        private void DamagedEventHandler(object sender, EventArgs _)
        {
            var killable = (Killable) sender;

            ParticleSystem.transform.position = killable.transform.position + _overlayModifier;
            ParticleSystem.Play();
        }
    }
}
