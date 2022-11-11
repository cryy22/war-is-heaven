using Units;
using UnityEngine;

namespace Cards.CardActions
{
    public abstract class CardAction : ScriptableObject
    {
        public abstract void Apply(Context context);

        public struct Context
        {
            public Unit Target;
        }
    }
}
