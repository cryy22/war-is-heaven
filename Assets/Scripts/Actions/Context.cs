using WarIsHeaven.Killables;
using WarIsHeaven.Units;

namespace WarIsHeaven.Actions
{
    public struct Context
    {
        public Killable Target;
        public Unit PlayerUnit;
        public EnemyUnit EnemyUnit;
    }
}
