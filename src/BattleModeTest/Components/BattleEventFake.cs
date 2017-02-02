using System;
using BattleMode.Entities.BattleState;
using BattleMode.Shared;
using Pokemon.Services.Rules;
using PokemonShared.Data;
using PokemonShared.Models;
using HpChangedEventArgs = BattleMode.Entities.BattleState.HpChangedEventArgs;

namespace BattleModeTest.Components
{
    public class BattleEventFake : IEventCreator
    {
        private readonly ClientIdentifier _id = new ClientIdentifier();
        private readonly PokemonEntity _pokemon;

        public event EventHandler CriticalDamage;
        public event EventHandler<MoveEffectiveEventArgs> MoveEffective;
        public event EventHandler NewTurn;
        public event EventHandler<HpChangedEventArgs> HpChanged;
        public event EventHandler<ClientPokemonChangedEventArgs> PokemonChanged;
        public event EventHandler<ClientStatusChangedEventArgs> StatusChanged;
        public event EventHandler<MoveUsedEventArgs> MoveUsed;

        public BattleEventFake()
        {
            var pkmn = new PokemonShared.Models.Pokemon(new PokemonData { Name = "name" }, new Stats());
            _pokemon = new PokemonEntity { Pokemon = pkmn };
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
            HpChanged?.Invoke(this, new HpChangedEventArgs(_id, hp));
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
            var move = new Move(new MoveData {Name = "Name"});
            MoveUsed?.Invoke(this, new MoveUsedEventArgs(move, _pokemon));
        }



        public void Critical()
        {
            throw new NotImplementedException();
        }

        public void Effective(MoveEfficiency effect, PokemonEntity target)
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

        public void SetPokemon(ClientIdentifier id, PokemonEntity pokemon)
        {
            throw new NotImplementedException();
        }

        public void SetStatus(PokemonEntity pokemon, StatusCondition condition)
        {
            throw new NotImplementedException();
        }

        public void UsingMove(PokemonEntity source, Move move)
        {
            throw new NotImplementedException();
        }

        public void SwitchPokemon(PokemonEntity pokemon)
        {
            throw new NotImplementedException();
        }
    }
}