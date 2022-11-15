using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using WarIsHeaven.Cards.CardActions;

namespace WarIsHeaven.Intents
{
    [CreateAssetMenu(fileName = "New IntentConfig", menuName = "Configs/Intent")]
    public class IntentConfig : ScriptableObject
    {
        public string Title;
        [TextArea] public string Description;
        public int InitialValue = 1;
        public SpriteLibraryAsset SpriteLibraryAsset;
        public List<ActionMagnitude> ActionMagnitudes;
    }
}
