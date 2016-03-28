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

        public IBattleState ActionState { get; set; }
        public IBattleState CharacterSetState { get; set; }
        public IBattleState ExecutionState { get; set; }

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
                currentState.Init(data);
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
            eventCreator = new EventCreator(data);

            this.rules = rules;
            this.scheduler = scheduler;
            ActionState = new WaitForActionState(this);
            CharacterSetState = new WaitForCharState(this, eventCreator);
            ExecutionState = new ExecuteState(this, scheduler, new CommandExecuter(eventCreator, rules));
        }

        public override void Initialize()
        {

            eventCreator.Setup(Game);
            CurrentState = CharacterSetState;
        }

        public void SetCharacter(ClientIdentifier id, Pokemon pkmn)
        {
            CurrentState.SetCharacter(id, pkmn);
        }

        public void SetItem(ClientIdentifier id, ClientIdentifier target, Item item)
        {
            CurrentState.SetItem(id, target, item);
        }

        public void SetMove(ClientIdentifier id, ClientIdentifier target, Move move)
        {
            CurrentState.SetMove(id, target, move);
        }

        public override void Update(GameTime gameTime)
        {
            CurrentState = CurrentState.Update(data);
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