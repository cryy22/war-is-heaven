using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "New Card Config", menuName = "Scriptable Objects/Configs/Card")]
    public class CardConfig : ScriptableObject
    {
        public string Title;
        [TextArea] public string Description;
    }
}
