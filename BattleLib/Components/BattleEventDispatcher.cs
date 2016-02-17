using BattleLib.Components.BattleState;
using BattleLib.GraphicComponents.GUI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components
{
    public interface BattleEvent
    {
        event EventHandler OnEventProcessed;
        void Dispatch();

    }

    class ReduceHPEvent : BattleEvent
    {
        public event EventHandler OnEventProcessed = delegate { };

        private BattleModel model;
        private bool player;
        private int newValue;

        public ReduceHPEvent(BattleModel model, bool player, int newValue)
        {
            this.model = model;
            this.player = player;
            this.newValue = newValue;
        }

        public void Dispatch()
        {
            if (player)
                model.PlayerHP = newValue;
            else
                model.AIHP = newValue;

            OnEventProcessed(this, null);
        }
    }
    public interface IEventDispatcher
    {
        void AddEvent(BattleEvent ev);
    }

    public class BattleEventDispatcher : GameComponent, IEventDispatcher
    {
        bool eventDispatched = false;

        public BattleEventDispatcher(Game game) : base(game) 
        {
            game.Services.AddService(typeof(IEventDispatcher), this);
        }

        LinkedList<BattleEvent> eventQueue = new LinkedList<BattleEvent>();

        public override void Update(GameTime gameTime)
        {
            if (eventDispatched || eventQueue.Count == 0)
                return;

            var ev = eventQueue.First.Value;
            eventQueue.RemoveFirst();
            ev.OnEventProcessed += OnEventProcessedHandler;

            eventDispatched = true;
            ev.Dispatch();
        }

        private void OnEventProcessedHandler(Object sender, EventArgs args)
        {
            var ev = (BattleEvent) sender;
            ev.OnEventProcessed -= OnEventProcessedHandler;
            eventDispatched = false;
        }

        public void AddEvent(BattleEvent ev)
        {
            eventQueue.AddLast(ev);
        }

    }
}
