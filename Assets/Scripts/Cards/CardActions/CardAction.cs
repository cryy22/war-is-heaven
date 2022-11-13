using UnityEngine;

namespace WarIsHeaven.Cards.CardActions
{
    public abstract class CardAction : ScriptableObject
    {
        public abstract void Invoke(Context context, int magnitude = 1);
    }
}
