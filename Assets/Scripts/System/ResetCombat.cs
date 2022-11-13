using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace WarIsHeaven.System
{
    public class ResetCombat : MonoBehaviour
    {
        [SerializeField] private Button ResetButton;

        private void OnEnable() { ResetButton.onClick.AddListener(OnClick); }
        private void OnDisable() { ResetButton.onClick.RemoveListener(OnClick); }

        private static void OnClick() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
    }
}
