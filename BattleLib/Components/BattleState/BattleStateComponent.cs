using Base;
using Base.Rules;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace BattleLib.Components.BattleState
{
    public enum BattleStates 
    {
        WaitForPokemon,
        WaitForAction,
        Execute
    }

    public class BattleStateComponent : GameComponent
    {
        public event EventHandler<StateChangedEventArgs> StateChanged = delegate { };

        internal WaitForActionState actionState;
        internal WaitForCharState charState;
        internal ExecuteState exeState;

        private RulesSet rules;
        private ICommandScheduler scheduler;
        private IBattleState currentState;
        private IBattleState CurrentState
        {
            get { return currentState; }
            set
            {
                if (currentState == value)
                    return;

                currentState = value;
                StateChanged(this, new StateChangedEventArgs(currentState.State));
                if (currentState.State == BattleStates.WaitForAction)
                    eventCreator.NewTurn();
            }
        }
        private BattleData data;
        private EventCreator eventCreator;

        public PokemonWrapper GetPokemon(ClientIdentifier id)
        {
            return data.GetPokemon(id);
        }

        public BattleStateComponent(ClientIdentifier player, ClientIdentifier ai, Game game, RulesSet rules, ICommandScheduler scheduler)
            : base(game)
        {
            data = new BattleData(player, ai);
            data.Player = player;
            data.Ai = ai;
            eventCreator = new EventCreator(data);
            this.rules = rules;
            this.scheduler = scheduler;
        }

        public ClientIdentifier AIIdentifier
        {
            get { return data.Ai; }
        }

        public ClientIdentifier PlayerIdentifier
        {
            get { return data.Player; }
        }

        public override void Initialize()
        {
            if (AIIdentifier == null || PlayerIdentifier == null)
                throw new InvalidOperationException("One of the identifier is missing");

            eventCreator.Setup(Game);
            actionState = new WaitForActionState(this);
            charState = new WaitForCharState(this, PlayerIdentifier, AIIdentifier, eventCreator);
            exeState = new ExecuteState(this, scheduler, new CommandExecuter(eventCreator, rules));

            CurrentState = charState;
            charState.Init(new List<ClientIdentifier>{data.Player, data.Ai});
        }

        public void SetCharacter(ClientIdentifier id, Pokemon pkmn)
        {
            CurrentState.SetCharacter(id, pkmn);
        }

        public void SetItem(ClientIdentifier id, Item item)
        {
            CurrentState.SetItem(id, item);
        }

        public void SetMove(ClientIdentifier id, Move move)
        {
            CurrentState.SetMove(id, move);
        }

        public override void Update(GameTime gameTime)
        {
            CurrentState = CurrentState.Update(data);
        }
    }

    public class ClientIdentifier
    {
        public ClientIdentifier()
        {
            Guid = Guid.NewGuid();
        }
        public String Name { get; set; }
        public bool IsPlayer { get; set; }

        private Guid Guid;

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var id = obj as ClientIdentifier;
            if (id == null)
                return false;

            return Equals(id);
            
        }

        public bool Equals(ClientIdentifier id)
        {
            return Guid.Equals(id.Guid);
        }

        public override int GetHashCode()
        {
            return Guid.GetHashCode();
        }
    }

    public class StateChangedEventArgs : EventArgs
    {
        public BattleStates NewState { get; private set; }
        public StateChangedEventArgs(BattleStates newState)
        {
            NewState = newState;
        }
    }

}