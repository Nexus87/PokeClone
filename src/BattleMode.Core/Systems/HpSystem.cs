using System;
using System.Linq;
using BattleMode.Core.Actions;
using BattleMode.Gui.Actions;
using BattleMode.Shared.Components;
using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Actions;

namespace BattleMode.Core.Systems
{
    public class HpSystem
    {
        public static void RegisterHandler(IMessageBus messageBus)
        {
            messageBus.RegisterForAction<TimeAction>(Update);
            messageBus.RegisterForAction<ChangeHpAction>(ChangeHp);
        }
        public static void ChangeHp(ChangeHpAction action, IEntityManager entityManager, IMessageBus messageBus)
        {
            var component = entityManager.GetComponentByTypeAndEntity<PokemonComponent>(action.Target).First();
            component.ChangeHp = action.Diff;
        }

        public static void Update(IEntityManager entityManager, IMessageBus messageBus)
        {
            var components = entityManager.GetComponentsOfType<PokemonComponent>().Where(x => x.ChangeHp != 0);
            foreach (var item in components)
            {
                var change = (int)(Math.Sign(item.ChangeHp) * Math.Min(item.Pokemon.MaxHp / 10.0, Math.Abs(item.ChangeHp)));
                var nextHp = item.Pokemon.Hp + change;
                if (nextHp > item.Pokemon.MaxHp)
                {
                    item.Pokemon.Hp = item.Pokemon.MaxHp;
                    item.ChangeHp = 0;
                    messageBus.SendAction(new HpChangeFinishedAction(item.EntityId));
                }
                else if (nextHp < 0)
                {
                    item.Pokemon.Hp = 0;
                    item.ChangeHp = 0;
                    messageBus.SendAction(new HpChangeFinishedAction(item.EntityId));
                }
                else
                {
                    item.ChangeHp -= change;
                    item.Pokemon.Hp = nextHp;
                    if(item.ChangeHp == 0)
                    {
                        messageBus.SendAction(new HpChangeFinishedAction(item.EntityId));
                    }
                }
            }
        }
    }
}