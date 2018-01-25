using System.Linq;
using BattleMode.Core.Actions;
using BattleMode.Entities.Actions;
using BattleMode.Gui.Actions;
using BattleMode.Shared;
using BattleMode.Shared.Components;
using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Actions;

namespace BattleMode.Core.Systems
{
    public class GameDriverSystem
    {
        private readonly IMessageBus _messageBus;

        public GameDriverSystem(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }
        public void UseMove(UseMoveAction action, IEntityManager entityManager)
        {
            var pokemon = entityManager.GetComponentByTypeAndEntity<PokemonComponent>(action.Source).First();
            _messageBus.SendAction(new QueueAction(new ShowMessageAction($"{pokemon.Pokemon.Name} uses {action.Move.Name}")));
        }

        public void DoDamage(DoDamageAction action, IEntityManager entityManager)
        {
            _messageBus.SendAction(new QueueAction(new ChangeHpAction(-action.Damage, action.Target)));
            if (action.Critical)
            {
                _messageBus.SendAction(new QueueAction(new ShowMessageAction("Critical!")));
            }

            if (action.MoveEfficiency == MoveEfficiency.NoEffect)
            {
                _messageBus.SendAction(new QueueAction(new ShowMessageAction("It has no effect!")));

            }
            else if (action.MoveEfficiency == MoveEfficiency.NotEffective)
            {
                _messageBus.SendAction(new QueueAction(new ShowMessageAction("It is not very effective!")));
            }
            else if (action.MoveEfficiency == MoveEfficiency.VeryEffective)
            {
                _messageBus.SendAction(new QueueAction(new ShowMessageAction("It is very effective!")));
            }
        }

        public void HpChangeFinsihed(HpChangeFinishedAction action, IEntityManager entityManager)
        {
            _messageBus.SendAction(new UnblockQueueAction());
        }

        
    }
}