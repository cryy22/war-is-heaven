using System;

namespace WarIsHeaven.Cards.CardActions
{
    [Serializable]
    public struct ActionMagnitude
    {
        public CardAction Action;
        public int Magnitude;

        public void Invoke(Context context) { Action.Invoke(context: context, magnitude: Magnitude); }
    }
}
