using System;
using Base;
using Base.Data;
using BattleMode.Core;
using BattleMode.Core.Components.BattleState;

namespace BattleModeTest.Components
{
    public class BattleEventFake : IEventCreator
    {
        private ClientIdentifier id = new ClientIdentifier();
        private PokemonWrapper pokemon;

        public event EventHandler CriticalDamage;
        public event EventHandler<MoveEffectiveEventArgs> MoveEffective;
        public event EventHandler NewTurn;
        public event EventHandler<HPChangedEventArgs> HPChanged;
        public event EventHandler<ClientPokemonChangedEventArgs> PokemonChanged;
        public event EventHandler<ClientStatusChangedEventArgs> StatusChanged;
        public event EventHandler<MoveUsedEventArgs> MoveUsed;

        public BattleEventFake()
        {
            var pkmn = new Pokemon(new PokemonData { Name = "name" }, new Stats());
            pokemon = new PokemonWrapper(id) { Pokemon = pkmn };
        }
        public void RaiseCritcalDamageEvent()
        {
            CriticalDamage(this, EventArgs.Empty);
        }

        public void RaiseMoveEffectiveEvent(MoveEfficiency effect)
        {
            MoveEffective(this, new MoveEffectiveEventArgs(effect, pokemon));
        }

        public void RaiseNewTurnEvent()
        {
            NewTurn(this, EventArgs.Empty);
        }

        public void RaiseHPChangedEvent(int hp = 10)
        {
            HPChanged(this, new HPChangedEventArgs(id, hp));
        }

        public void RaisePokemonChangedEvent()
        {
            PokemonChanged(this, new ClientPokemonChangedEventArgs(id, pokemon));
        }

        public void RaiseStatusChanged(StatusCondition condition)
        {
            StatusChanged(this, new ClientStatusChangedEventArgs(id, condition));
        }

        public void RaiseMoveUsed()
        {
            var move = new Move(new MoveData(){Name = "Name"});
            MoveUsed(this, new MoveUsedEventArgs(move, pokemon));
        }



        public void Critical()
        {
            throw new NotImplementedException();
        }

        public void Effective(MoveEfficiency effect, PokemonWrapper target)
        {
            throw new NotImplementedException();
        }

        public void SetNewTurn()
        {
            throw new NotImplementedException();
        }

        public void SetHP(ClientIdentifier id, int hp)
        {
            throw new NotImplementedException();
        }

        public void SetPokemon(ClientIdentifier id, PokemonWrapper pokemon)
        {
            throw new NotImplementedException();
        }

        public void SetStatus(PokemonWrapper pokemon, StatusCondition condition)
        {
            throw new NotImplementedException();
        }

        public void UsingMove(PokemonWrapper source, Move move)
        {
            throw new NotImplementedException();
        }

        public void SwitchPokemon(PokemonWrapper pokemon)
        {
            throw new NotImplementedException();
        }
    }
}