using GameEngine.Core.ECS;

namespace BattleMode.Shared.Actions
{
    public class SetOpponentAction
    {
        public readonly Entity OppenentEntity;

        public SetOpponentAction(Entity oppenentEntity)
        {
            OppenentEntity = oppenentEntity;
        }
    }
}