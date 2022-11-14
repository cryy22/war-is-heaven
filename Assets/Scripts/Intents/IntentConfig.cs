using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using WarIsHeaven.Cards.CardActions;

namespace WarIsHeaven.Intents
{
    [CreateAssetMenu(fileName = "New IntentConfig", menuName = "Scriptable Objects/Configs/Intent")]
    public class IntentConfig : ScriptableObject
    {
        public string Title;
        [TextArea] public string Description;
        public SpriteLibraryAsset SpriteLibraryAsset;
        public List<ActionMagnitude> ActionMagnitudes;
    }
}
