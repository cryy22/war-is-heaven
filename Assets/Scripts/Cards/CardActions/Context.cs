using WarIsHeaven.Killables;
using WarIsHeaven.Units;

namespace WarIsHeaven.Cards.CardActions
{
    public struct Context
    {
        public Killable Target;
        public Unit PlayerUnit;
        public EnemyUnit EnemyUnit;
    }
}
