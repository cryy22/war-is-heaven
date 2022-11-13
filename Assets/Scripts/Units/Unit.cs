using UnityEngine;
using WarIsHeaven.Killables;

namespace WarIsHeaven.Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private Killable HealthComponent;
        public Killable Health => HealthComponent;
    }
}
