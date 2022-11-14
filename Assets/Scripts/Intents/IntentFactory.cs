using UnityEngine;

namespace WarIsHeaven.Intents
{
    [CreateAssetMenu(fileName = "New IntentFactory", menuName = "Scriptable Objects/Factories/Intent")]
    public class IntentFactory : ScriptableObject
    {
        [SerializeField] private Intent IntentPrefab;

        public Intent Create(IntentConfig config)
        {
            Intent intent = Instantiate(IntentPrefab);
            intent.Initialize(config);

            return intent;
        }
    }
}
