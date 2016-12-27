using System;
using Base;
using Base.Data;
using BattleMode.Components.BattleState;
using BattleMode.Core;
using BattleMode.Shared;

namespace BattleModeTest.Components
{
    public class BattleEventFake : IEventCreator
    {
        private readonly ClientIdentifier _id = new ClientIdentifier();
        private readonly PokemonWrapper _pokemon;

        public event EventHandler CriticalDamage;
        public event EventHandler<MoveEffectiveEventArgs> MoveEffective;
        public event EventHandler NewTurn;
        public event EventHandler<HpChangedEventArgs> HPChanged;
        public event EventHandler<ClientPokemonChangedEventArgs> PokemonChanged;
        public event EventHandler<ClientStatusChangedEventArgs> StatusChanged;
        public event EventHandler<MoveUsedEventArgs> MoveUsed;

        public BattleEventFake()
        {
            var pkmn = new Pokemon(new PokemonData { Name = "name" }, new Stats());
            _pokemon = new PokemonWrapper(_id) { Pokemon = pkmn };
        }
        public void RaiseCritcalDamageEvent()
        {
            CriticalDamage?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseMoveEffectiveEvent(MoveEfficiency effect)
        {
            MoveEffective?.Invoke(this, new MoveEffectiveEventArgs(effect, _pokemon));
        }

        public void RaiseNewTurnEvent()
        {
            NewTurn?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseHpChangedEvent(int hp = 10)
        {
            HPChanged?.Invoke(this, new HpChangedEventArgs(_id, hp));
        }

        public void RaisePokemonChangedEvent()
        {
            PokemonChanged?.Invoke(this, new ClientPokemonChangedEventArgs(_id, _pokemon));
        }

        public void RaiseStatusChanged(StatusCondition condition)
        {
            StatusChanged?.Invoke(this, new ClientStatusChangedEventArgs(_id, condition));
        }

        public void RaiseMoveUsed()
        {
            var move = new Move(new MoveData(){Name = "Name"});
            MoveUsed?.Invoke(this, new MoveUsedEventArgs(move, _pokemon));
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

        public void SetHp(ClientIdentifier id, int hp)
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