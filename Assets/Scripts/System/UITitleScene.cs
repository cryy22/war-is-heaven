using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WarIsHeaven.Constants;

namespace WarIsHeaven.System
{
    public class UITitleScene : MonoBehaviour
    {
        [SerializeField] private GameObject WarText;
        [SerializeField] private GameObject IsText;
        [SerializeField] private GameObject HeavenText;
        [SerializeField] private Button BeginButton;

        private void Awake()
        {
            WarText.SetActive(false);
            IsText.SetActive(false);
            HeavenText.SetActive(false);

            BeginButton.gameObject.SetActive(false);
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.5f);

            WarText.SetActive(true);
            yield return new WaitForSeconds(0.5f);

            IsText.SetActive(true);
            yield return new WaitForSeconds(0.5f);

            HeavenText.SetActive(true);
            yield return new WaitForSeconds(1f);

            BeginButton.gameObject.SetActive(true);
        }

        private void OnEnable() { BeginButton.onClick.AddListener(OnBeginButtonClicked); }
        private void OnDisable() { BeginButton.onClick.RemoveListener(OnBeginButtonClicked); }

        private static void OnBeginButtonClicked() { SceneManager.LoadScene(Scenes.CombatIndex); }
    }
}
