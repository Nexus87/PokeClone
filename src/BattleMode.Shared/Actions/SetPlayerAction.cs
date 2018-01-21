using GameEngine.Core.ECS;

namespace BattleMode.Shared.Actions
{
    public class SetPlayerAction
    {
        public readonly Entity PlayerEntity;

        public SetPlayerAction(Entity playerEntity)
        {
            PlayerEntity = playerEntity;
        }
    }
}