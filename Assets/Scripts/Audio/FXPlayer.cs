using Crysc.Common;
using UnityEngine;

namespace WarIsHeaven.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class FXPlayer : SingletonBehaviour<FXPlayer>
    {
        [SerializeField] private AudioClip GunshotClip;
        [SerializeField] private AudioClip MonsterAttackClip;
        [SerializeField] private AudioClip HealSoundClip;

        [Range(min: 0f, max: 1f)]
        [SerializeField]
        private float PitchVariance = 0.25f;

        private AudioSource _audioSource;

        protected override void Awake()
        {
            base.Awake();
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayGunshot() { PlayClip(GunshotClip); }
        public void PlayMonsterAttack() { PlayClip(MonsterAttackClip); }
        public void PlayHealSound() { PlayClip(HealSoundClip); }

        private void PlayClip(AudioClip clip)
        {
            _audioSource.pitch = Random.Range(
                minInclusive: 1 - (PitchVariance / 2),
                maxInclusive: 1 + (PitchVariance / 2)
            );
            _audioSource.PlayOneShot(clip);
        }
    }
}
