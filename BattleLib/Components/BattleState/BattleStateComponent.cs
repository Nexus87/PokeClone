using Base;
using BattleLib.Components.BattleState.Commands;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace BattleLib.Components.BattleState
{
    class DummySchedular : ICommandScheduler
    {
        List<ICommand> commands = new List<ICommand>();

        public void AppendCommand(ICommand command)
        {
            commands.Add(command);
        }

        public void ClearCommands()
        {
            commands.Clear();
        }

        public IEnumerable<ICommand> ScheduleCommands()
        {
            return commands;
        }
    }

    public class ClientIdentifier
    {
        public String Name { get; set; }
    }

    public class BattleStateComponent : GameComponent
    {
        BattleData data = new BattleData();

        internal WaitForActionState actionState;
        internal WaitForCharState charState;
        internal ExecuteState exeState;

        IBattleState currentState;

        public ClientIdentifier PlayerIdentifier {
            get { return data.player; }
            set { data.player = value; }
        }

        public ClientIdentifier AIIdentifier
        {
            get { return data.ai; }
            set { data.ai = value; }
        }

        public BattleStateComponent(Game game) : base(game) { }

        public BattleStateComponent(ClientIdentifier player, ClientIdentifier ai, Game game) : base(game)
        {
            data.player = player;
            data.ai = ai;
        }

        public override void Initialize()
        {
            if (AIIdentifier == null || PlayerIdentifier == null)
                throw new InvalidOperationException("One of the identifier is missing");

            actionState = new WaitForActionState(this, PlayerIdentifier, AIIdentifier);
            charState = new WaitForCharState(this, PlayerIdentifier, AIIdentifier);
            exeState = new ExecuteState(this, new DummySchedular(), new Gen1BattleRules(false));

            currentState = actionState;
        }

        public override void Update(GameTime gameTime)
        {
            var newState = currentState.Update(data);
            
            if (newState != currentState)
                newState.Init();

            currentState = newState;
        }

        public void SetCharacter(ClientIdentifier id, Pokemon pkmn)
        {
            currentState.SetCharacter(id, pkmn);
        }

        public void SetMove(ClientIdentifier id, Move move)
        {
            currentState.SetMove(id, move);
        }

        public void SetItem(ClientIdentifier id, Item item)
        {
            currentState.SetItem(id, item);
        }
    }
}
