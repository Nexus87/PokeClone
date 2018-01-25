using System.Linq;
using BattleMode.Core.Actions;
using BattleMode.Entities.Actions;
using BattleMode.Gui.Actions;
using BattleMode.Shared;
using BattleMode.Shared.Components;
using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Actions;
using GameEngine.Core.ECS.Components;

namespace BattleMode.Core.Systems
{
    public class GameDriverSystem
    {
        private static GameDriverSystem dummyInstance = new GameDriverSystem();

        public static void RegisterHandler(IMessageBus messageBus)
        {
            messageBus.RegisterForAction<UseMoveAction>(UseMove);
            messageBus.RegisterForAction<DoDamageAction>(DoDamage);
            messageBus.RegisterForAction<HpChangeFinishedAction>(HpChangeFinsihed);
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
                messageBus.SendAction(new QueueAction(new ShowMessageAction("Critical!")), dummyInstance);
            }

            if (action.MoveEfficiency == MoveEfficiency.NoEffect)
            {
                messageBus.SendAction(new QueueAction(new ShowMessageAction("It has no effect!")), dummyInstance);

            }
            else if (action.MoveEfficiency == MoveEfficiency.NotEffective)
            {
                messageBus.SendAction(new QueueAction(new ShowMessageAction("It is not very effective!")), dummyInstance);
            }
            else if (action.MoveEfficiency == MoveEfficiency.VeryEffective)
            {
                messageBus.SendAction(new QueueAction(new ShowMessageAction("It is very effective!")), dummyInstance);
            }
        }

        public static void HpChangeFinsihed(HpChangeFinishedAction action, IEntityManager entityManager, IMessageBus messageBus)
        {
            var component = entityManager.GetFirstComponentOfType<ActionQueueComponent>();
            if (component.Actions.Count > 0)
                messageBus.SendAction(new UnblockQueueAction(), dummyInstance);
            else
                messageBus.SendExecuteNextCommandAction(dummyInstance);
        }


    }
}