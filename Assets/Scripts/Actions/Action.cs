using System.Collections;
using UnityEngine;

namespace WarIsHeaven.Actions
{
    public abstract class Action : ScriptableObject
    {
        public virtual bool CanBeInvoked(Context context, int magnitude = 1) { return true; }
        public abstract void Invoke(Context context, int magnitude = 1);
        public virtual IEnumerator Animate(Context context) { yield break; }
    }
}
