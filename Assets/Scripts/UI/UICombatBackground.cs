using System.Collections;
using UnityEngine;

namespace WarIsHeaven.UI
{
    public class UICombatBackground : MonoBehaviour
    {
        [SerializeField] private Transform Sky;
        [SerializeField] private Transform Clouds;
        [SerializeField] private Transform BackHills;
        [SerializeField] private Transform MidHills;
        [SerializeField] private Transform FrontHills;

        [SerializeField] private float SineWaveDuration = 8f;
        [SerializeField] private float SineWaveAmplitude = 1f;
        [SerializeField] private float CloudLength;
        [SerializeField] private float CloudSpeed = 0.5f;

        private void Start()
        {
            StartCoroutine(AnimateClouds());
            StartCoroutine(AnimateHills());
        }

        private IEnumerator AnimateClouds()
        {
            var distance = 0f;

            while (true)
            {
                distance = (distance + (Time.deltaTime * CloudSpeed)) % CloudLength;
                Clouds.localPosition = new Vector3(x: distance, y: 0, z: 0);

                yield return null;
            }
        }

        private IEnumerator AnimateHills()
        {
            var time = 0f;

            Vector3 BackHillsStart = BackHills.localPosition;
            Vector3 MidHillsStart = MidHills.localPosition;
            Vector3 FrontHillsStart = FrontHills.localPosition;

            while (true)
            {
                time += Time.deltaTime;

                float t = Mathf.Sin((time / SineWaveDuration) * Mathf.PI * 2f) * SineWaveAmplitude;

                BackHills.localPosition = BackHillsStart + (Vector3.right * t * 1f);
                MidHills.localPosition = MidHillsStart + (Vector3.right * t * 2f);
                FrontHills.localPosition = FrontHillsStart + (Vector3.right * t * 4f);

                yield return null;
            }
        }
    }
}
