using BattleMode.Shared;
using GameEngine.Core.ECS;

namespace BattleMode.Entities.Actions
{
    public class DoDamageAction
    {
        public readonly int Damage;
        public readonly MoveEfficiency MoveEfficiency;
        public readonly Entity Target;
        public readonly bool Critical;
        public DoDamageAction(int damage, MoveEfficiency moveEfficiency, Entity target, bool critical)
        {
            Damage = damage;
            MoveEfficiency = moveEfficiency;
            Target = target;
            Critical = critical;
        }
    }
}