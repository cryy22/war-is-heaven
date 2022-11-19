using Crysc.Initialization;
using UnityEngine;

namespace WarIsHeaven.Intents
{
    [CreateAssetMenu(fileName = "IntentFactory", menuName = "Factories/Intent")]
    public class IntentFactory : InitializationFactory<Intent, IntentConfig>
    { }
}
