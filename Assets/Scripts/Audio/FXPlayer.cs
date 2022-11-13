using UnityEngine;

namespace WarIsHeaven.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class FXPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip GunshotClip;
        [SerializeField] private AudioClip MonsterAttackClip;

        [Range(min: 0f, max: 1f)]
        [SerializeField]
        private float PitchVariance = 0.25f;

        private AudioSource _audioSource;

        public static FXPlayer Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayGunshot() { PlayClip(GunshotClip); }

        public void PlayMonsterAttack() { PlayClip(MonsterAttackClip); }

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
