using Base;
using Microsoft.Xna.Framework;
using System;

namespace BattleLib.Components.BattleState
{
    public class BattleStateComponent : GameComponent
    {
        internal WaitForActionState actionState;
        internal WaitForCharState charState;
        internal ExecuteState exeState;
        
        private IBattleState currentState;
        private BattleData data;
        private EventCreator eventCreator;

        public BattleStateComponent(Game game)
            : base(game)
        {
        }

        public BattleStateComponent(ClientIdentifier player, ClientIdentifier ai, Game game)
            : base(game)
        {
            data = new BattleData(player, ai);
            data.player = player;
            data.ai = ai;
        }

        public ClientIdentifier AIIdentifier
        {
            get { return data.ai; }
        }

        public ClientIdentifier PlayerIdentifier
        {
            get { return data.player; }
        }

        public override void Initialize()
        {
            if (AIIdentifier == null || PlayerIdentifier == null)
                throw new InvalidOperationException("One of the identifier is missing");

            actionState = new WaitForActionState(this, PlayerIdentifier, AIIdentifier);
            charState = new WaitForCharState(this, PlayerIdentifier, AIIdentifier);
            exeState = new ExecuteState(this, new Gen1CommandScheduler(), new Gen1BattleRules(false));

            currentState = actionState;
        }

        public void SetCharacter(ClientIdentifier id, Pokemon pkmn)
        {
            currentState.SetCharacter(id, pkmn);
        }

        public void SetItem(ClientIdentifier id, Item item)
        {
            currentState.SetItem(id, item);
        }

        public void SetMove(ClientIdentifier id, Move move)
        {
            currentState.SetMove(id, move);
        }

        public override void Update(GameTime gameTime)
        {
            var newState = currentState.Update(data);

            if (newState != currentState)
                newState.Init();

            currentState = newState;
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
}