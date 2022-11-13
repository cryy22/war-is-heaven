using UnityEngine;

namespace WarIsHeaven.Intents
{
    [CreateAssetMenu(fileName = "New IntentFactory", menuName = "Scriptable Objects/Factories/Intent")]
    public class IntentFactory : ScriptableObject
    {
        [SerializeField] private Intent IntentPrefab;

        public Intent Create() { return Instantiate(IntentPrefab); }
    }
}
