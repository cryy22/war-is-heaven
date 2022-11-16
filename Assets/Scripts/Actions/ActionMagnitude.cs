using System;

namespace WarIsHeaven.Actions
{
    [Serializable]
    public struct ActionMagnitude
    {
        public Action Action;
        public int Magnitude;

        public void Invoke(Context context) { Action.Invoke(context: context, magnitude: Magnitude); }
    }
}
