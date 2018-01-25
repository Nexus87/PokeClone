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
        public static void RegisterHandler(IMessageBus messageBus)
        {
            messageBus.RegisterForAction<UseMoveAction>(UseMove);
            messageBus.RegisterForAction<DoDamageAction>(DoDamage);
        }
        public static void UseMove(UseMoveAction action, IEntityManager entityManager, IMessageBus messageBus)
        {
            var pokemon = entityManager.GetComponentByTypeAndEntity<PokemonComponent>(action.Source).First();
            messageBus.SendAction(new QueueAction(new ShowMessageAction($"{pokemon.Pokemon.Name} uses {action.Move.Name}")));
        }

        public static void DoDamage(DoDamageAction action, IEntityManager entityManager, IMessageBus messageBus)
        {
            messageBus.SendAction(new QueueAction(new ChangeHpAction(-action.Damage, action.Target)));
            if (action.Critical)
            {
                messageBus.SendAction(new QueueAction(new ShowMessageAction("Critical!")));
            }

            if (action.MoveEfficiency == MoveEfficiency.NoEffect)
            {
                messageBus.SendAction(new QueueAction(new ShowMessageAction("It has no effect!")));

            }
            else if (action.MoveEfficiency == MoveEfficiency.NotEffective)
            {
                messageBus.SendAction(new QueueAction(new ShowMessageAction("It is not very effective!")));
            }
            else if (action.MoveEfficiency == MoveEfficiency.VeryEffective)
            {
                messageBus.SendAction(new QueueAction(new ShowMessageAction("It is very effective!")));
            }
        }

        public void HpChangeFinsihed(HpChangeFinishedAction action, IEntityManager entityManager, IMessageBus messageBus)
        {
            messageBus.SendAction(new UnblockQueueAction());
        }


    }
}