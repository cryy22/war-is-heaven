using UnityEngine;

namespace WarIsHeaven.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class FXPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip GunshotClip;
        [SerializeField] private AudioClip MonsterAttackClip;

        [SerializeField] private float MinPitch = 0.9f;
        [SerializeField] private float MaxPitch = 1.1f;

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
            _audioSource.pitch = Random.Range(minInclusive: MinPitch, maxInclusive: MaxPitch);
            _audioSource.PlayOneShot(clip);
        }
    }
}
