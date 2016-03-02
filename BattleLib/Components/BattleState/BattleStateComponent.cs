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

        public BattleStateComponent(ClientIdentifier player, ClientIdentifier ai, Game game)
            : base(game)
        {
            data = new BattleData(player, ai);
            data.player = player;
            data.ai = ai;
            eventCreator = new EventCreator(data);
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

            eventCreator.Setup(Game);
            actionState = new WaitForActionState(this, PlayerIdentifier, AIIdentifier);
            charState = new WaitForCharState(this, PlayerIdentifier, AIIdentifier, eventCreator);
            exeState = new ExecuteState(this, new Gen1CommandScheduler(), new CommandExecuter(new DummyRules(), eventCreator));

            currentState = charState;
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
            currentState = currentState.Update(data);
        }
    }
    public class DummyRules : IBattleRules
    {
        public float CalculateBaseDamage(PokemonWrapper source, PokemonWrapper target, Move move)
        {
            return 1.0f;
        }

        public bool CanChange()
        {
            return false;
        }

        public bool CanEscape()
        {
            return false;
        }

        public float GetCriticalHitChance(Move move)
        {
            return 0.15f;
        }

        public float GetCriticalHitModifier()
        {
            return 2.0f;
        }

        public float GetHitChance(PokemonWrapper source, PokemonWrapper target, Move move)
        {
            return 0.95f;
        }

        public float GetMiscModifier(PokemonWrapper source, PokemonWrapper target, Move move)
        {
            return 1.0f;
        }

        public float GetStateModifier(int stage)
        {
            return 1.0f;
        }

        public float GetTypeModifier(PokemonWrapper source, PokemonWrapper target, Move move)
        {
            return 1.0f;
        }

        public float SameTypeAttackBonus(PokemonWrapper source, Move move)
        {
            return 1.0f;
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