using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WarIsHeaven.Constants;

namespace WarIsHeaven.SceneControls
{
    public class ResetCombat : MonoBehaviour
    {
        [SerializeField] private Button ResetButton;

        private void OnEnable() { ResetButton.onClick.AddListener(OnClick); }
        private void OnDisable() { ResetButton.onClick.RemoveListener(OnClick); }

        private static void OnClick() { SceneManager.LoadScene(Scenes.CombatIndex); }
    }
}
