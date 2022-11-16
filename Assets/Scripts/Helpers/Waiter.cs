using System.Collections;
using UnityEngine;

namespace WarIsHeaven.Helpers
{
    public static class Waiter
    {
        public static IEnumerator WaitForAll(params Coroutine[] coroutines) { return coroutines.GetEnumerator(); }
    }
}
