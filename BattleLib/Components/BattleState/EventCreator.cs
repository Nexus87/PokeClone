using Base;
using BattleLib.Components.BattleState.Commands;
using BattleLib.Events;
using BattleLib.GraphicComponents;
using GameEngine.EventComponent;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.BattleState
{
    interface EventRecord : IComparable<EventRecord>
    {
        int Priority { get; }
        ClientIdentifier ID { get; }
        void AddEvent(IEventQueue queue);
    }

    class DamageEvent : EventRecord
    {
        private HPReductionArgs args;
        private IBattleGraphicService graphic;
        private IGUIService gui;

        public DamageEvent(HPReductionArgs args, IGUIService gui, IBattleGraphicService graphic)
        {
            ID = args.target;
            this.args = args;
            this.gui = gui;
            this.graphic = graphic;
            
        }
        public int Priority { get { return 0; } }

        public ClientIdentifier ID { get; private set; }

        public void AddEvent(IEventQueue queue)
        {
            if (!args.success)
            {
                if (args.resason == MoveFailedReasons.missed)
                    queue.AddShowMessageEvent(gui, "Attack missed");
                else
                    queue.AddShowMessageEvent(gui, "Attack has no effect");

                return;
            }

            queue.AddHPEvent(graphic, args.target.IsPlayer, args.newHP);

        }

        public int CompareTo(EventRecord other)
        {
            return Priority.CompareTo(other.Priority);
        }
    }

    class StateChangedEvent : EventRecord
    {
        private StateChangeArgs args;
        private IGUIService gui;
        private string pkmnName;

        public StateChangedEvent(string pkmnName, StateChangeArgs args, IGUIService gui)
        {
            this.ID = args.target;
            this.args = args;
            this.gui = gui;
            this.pkmnName = pkmnName;
        }

        public int Priority { get { return 1; } }
        public ClientIdentifier ID { get; private set; }

        public void AddEvent(IEventQueue queue)
        {
            if (!args.success)
            {
                if (args.resason == MoveFailedReasons.noEffect)
                {
                    queue.AddShowMessageEvent(gui, "Attack has no effect");
                }
                return;
            }

            if (args.oldState < args.newState)
                queue.AddShowMessageEvent(gui, pkmnName + "'s " + args.state + " was lowered");
            else
                queue.AddShowMessageEvent(gui, pkmnName + "'s " + args.state + "  increased");
        }

        public int CompareTo(EventRecord other)
        {
            return Priority.CompareTo(other.Priority);
        }
    }

    class ConditionChangedEvent : EventRecord
    {
        private ConditionChangeArgs args;
        private IGUIService gui;
        private IBattleGraphicService graphic;
        private string pkmnName;
        
        public ConditionChangedEvent(string pkmnName, ConditionChangeArgs args, IGUIService gui, IBattleGraphicService graphic)
        {
            ID = args.target;
            this.args = args;
            this.gui = gui;
            this.graphic = graphic;
            this.pkmnName = pkmnName;
        }

        public int Priority { get { return 2; } }
        public ClientIdentifier ID { get; private set; }

        public void AddEvent(IEventQueue queue)
        {
            if (!args.success)
            {
                queue.AddShowMessageEvent(gui, "Attack has no effect");
                return;
            }

            if (args.newCondition == StatusCondition.Normal)
                queue.AddShowMessageEvent(gui, pkmnName + " is no longer " + args.oldCondition);
            else
                queue.AddShowMessageEvent(gui, pkmnName + " is " + args.newCondition);
        }

        public int CompareTo(EventRecord other)
        {
            return Priority.CompareTo(other.Priority);
        }
    }


    public class EventCreator
    {
        
        private IEventQueue eventDispatcher;
        private IBattleGraphicService graphicService;
        private IGUIService guiService;

        private BattleData data;
        private PokemonWrapper playerPkmn;
        private PokemonWrapper aiPkmn;

        private readonly List<EventRecord> records = new List<EventRecord>();
        public EventCreator(BattleData data)
        {
            this.data = data;
            playerPkmn = data.PlayerPkmn;
            aiPkmn = data.AIPkmn;

        }

        public void Setup(Game game, IBattleRules rules, ExecuteState state)
        {
            eventDispatcher = game.Services.GetService<IEventQueue>();
            graphicService = game.Services.GetService<IBattleGraphicService>();
            guiService = game.Services.GetService<IGUIService>();

            state.OnCommandStarted += CommandStartHandler;
            state.OnCommandFinished += CommandFinishedHandler;

            rules.ChangeUsed += ChangeUsedHandler;
            rules.ItemUsed += ItemUsedHandler;
            rules.MoveUsed += MoveUsedHandler;

            rules.ConditionChange += ConditionChangeHandler;
            rules.HPReduction += HPReductionHandler;
            rules.StateChange += StateChangeHandler;
        }

        private void StateChangeHandler(object sender, StateChangeArgs e)
        {
            records.Add(new StateChangedEvent(data.GetPkmn(e.target).Name, e, guiService));
        }

        private void HPReductionHandler(object sender, HPReductionArgs e)
        {
            records.Add(new DamageEvent(e, guiService, graphicService));
        }

        private void ConditionChangeHandler(object sender, ConditionChangeArgs e)
        {
            records.Add(new ConditionChangedEvent(data.GetPkmn(e.target).Name, e, guiService, graphicService));
        }

        private void MoveUsedHandler(object sender, MoveUsedArgs e)
        {
            var pkmnName = data.GetPkmn(e.source).Name;
            eventDispatcher.AddShowMessageEvent(guiService, pkmnName + " uses " + e.move);
        }

        private void ChangeUsedHandler(object sender, ChangeUsedArgs e)
        {
            throw new NotImplementedException();
        }

        private void ItemUsedHandler(object sender, ItemUsedArgs e)
        {
            throw new NotImplementedException();
        }

        private void CommandFinishedHandler(object sender, ExecutionEventArgs e)
        {
            var eventsByTarget = records.GroupBy(r => r.ID);
            
            foreach (var events in eventsByTarget)
            {
                foreach (var ev in events.OrderBy(a => a.Priority))
                    ev.AddEvent(eventDispatcher);
            }
        }

        private void CommandStartHandler(object sender, ExecutionEventArgs e)
        {
            records.Clear();
        }
    }
}
