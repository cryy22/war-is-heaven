using System.Collections;
using UnityEngine;

namespace WarIsHeaven.Helpers
{
    public static class Mover
    {
        public static IEnumerator MoveLocal(Transform transform, Vector3 end, float duration = 0.125f)
        {
            Vector3 start = transform.localPosition;
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime / duration;
                transform.localPosition = Vector3.Lerp(a: start, b: end, t: t);
                yield return null;
            }

            transform.localPosition = end;
        }
    }
}
