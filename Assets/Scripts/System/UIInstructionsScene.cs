using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WarIsHeaven.Audio;
using WarIsHeaven.Constants;

namespace WarIsHeaven.System
{
    public class UIInstructionsScene : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private float SpeedModifier = 1.0f;

        [SerializeField] private GameObject YouAreNeffie;
        [SerializeField] private GameObject Agent777;
        [SerializeField] private GameObject Halo;
        [SerializeField] private GameObject Earth;
        [SerializeField] private GameObject Alliance;
        [SerializeField] private GameObject Violent;
        [SerializeField] private GameObject Enemy;
        [SerializeField] private GameObject Neutralization;
        [SerializeField] private GameObject UmAgency;
        [SerializeField] private GameObject YourMission;
        [SerializeField] private GameObject NeutralizeEnemies;
        [SerializeField] private GameObject WithoutKilling;
        [SerializeField] private GameObject RemoveAbility;
        [SerializeField] private GameObject DontLetThemDie;
        [SerializeField] private GameObject DontDie;
        [SerializeField] private GameObject GoodLuck;

        [SerializeField] private Button SkipButton;

        private Coroutine _scheduledAdvance;
        private bool _isReadyToAdvance;

        private void Awake()
        {
            YouAreNeffie.SetActive(false);
            Agent777.SetActive(false);
            Halo.SetActive(false);
            Earth.SetActive(false);
            Alliance.SetActive(false);
            Violent.SetActive(false);
            Enemy.SetActive(false);
            Neutralization.SetActive(false);
            UmAgency.SetActive(false);
            YourMission.SetActive(false);
            NeutralizeEnemies.SetActive(false);
            WithoutKilling.SetActive(false);
            RemoveAbility.SetActive(false);
            DontLetThemDie.SetActive(false);
            DontDie.SetActive(false);
            GoodLuck.SetActive(false);
        }

        private IEnumerator Start()
        {
            yield return WaitAndDisplay(duration: 0.5f, displayer: YouAreNeffie);
            yield return WaitAndDisplay(duration: 1.0f, displayer: Agent777);
            yield return WaitAndDisplay(duration: 1.0f, displayer: Halo, playGunshot: true);
            yield return WaitAndDisplay(duration: 1.0f, displayer: Earth, playGunshot: true);
            yield return WaitAndDisplay(duration: 1.0f, displayer: Alliance, playGunshot: true);
            yield return WaitAndDisplay(duration: 1.0f, displayer: Violent, playGunshot: true);
            yield return WaitAndDisplay(duration: 1.0f, displayer: Enemy, playGunshot: true);
            yield return WaitAndDisplay(duration: 1.0f, displayer: Neutralization, playGunshot: true);
            yield return WaitAndDisplay(duration: 2.0f, displayer: UmAgency);
            yield return WaitAndDisplay(duration: 2.5f, displayer: YourMission, playGunshot: true);
            yield return WaitAndDisplay(duration: 2.0f, displayer: NeutralizeEnemies);
            yield return WaitAndDisplay(duration: 1.0f, displayer: WithoutKilling);
            yield return WaitAndDisplay(duration: 2.0f, displayer: RemoveAbility);
            yield return WaitAndDisplay(duration: 2.0f, displayer: DontLetThemDie);
            yield return WaitAndDisplay(duration: 2.0f, displayer: DontDie);
            yield return WaitAndDisplay(duration: 2.0f, displayer: GoodLuck);

            yield return new WaitForSeconds(1.0f);
            SkipButton.GetComponentInChildren<TMP_Text>().text = "Mission Start";
        }

        private void OnEnable() { SkipButton.onClick.AddListener(OnSkipButtonClicked); }
        private void OnDisable() { SkipButton.onClick.RemoveListener(OnSkipButtonClicked); }

        private static void OnSkipButtonClicked() { SceneManager.LoadScene(Scenes.CombatEasyIndex); }

        private IEnumerator WaitAndDisplay(float duration, GameObject displayer, bool playGunshot = false)
        {
            duration *= SpeedModifier;

            yield return WaitOnAdvance(duration);
            displayer.SetActive(true);
            if (playGunshot) FXPlayer.Instance.PlayGunshot();
        }

        private IEnumerator WaitOnAdvance(float duration)
        {
            _isReadyToAdvance = false;
            _scheduledAdvance = StartCoroutine(ScheduleAdvance(duration));
            yield return new WaitUntil(() => _isReadyToAdvance);
        }

        private IEnumerator ScheduleAdvance(float duration)
        {
            yield return new WaitForSeconds(duration);

            _isReadyToAdvance = true;
            _scheduledAdvance = null;
        }

        public void OnPointerDown(PointerEventData _)
        {
            if (_scheduledAdvance != null) StopCoroutine(_scheduledAdvance);

            _isReadyToAdvance = true;
            _scheduledAdvance = null;
        }
    }
}
