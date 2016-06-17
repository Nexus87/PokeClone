using Base;
using Base.Data;
using BattleLib.GraphicComponents;
using GameEngine.Registry;
using System;

namespace BattleLib.Components.BattleState
{
    [GameTypeAttribute(RegisterType=typeof(IEventCreator), SingleInstance=true)]
    public class EventCreator : IEventCreator, IBattleEvents
    {
        public void UsingMove(PokemonWrapper source, Move move)
        {
            MoveUsed(this, new MoveUsedEventArgs(move, source));
        }

        public void SetHP(ClientIdentifier id, int hp)
        {
            HPChanged(this, new HPChangedEventArgs(id, hp));
        }

        public void Effective(MoveEfficiency effect, PokemonWrapper target)
        {
            MoveEffective(this, new MoveEffectiveEventArgs(effect, target));
        }

        public void Critical()
        {
            CriticalDamage(this, EventArgs.Empty);
        }

        public void SetStatus(PokemonWrapper pokemon, StatusCondition condition)
        {
            StatusChanged(this, new ClientStatusChangedEventArgs(pokemon.Identifier, condition));
        }

        public void SetPokemon(ClientIdentifier id, PokemonWrapper pokemon)
        {
            PokemonChanged(this, new ClientPokemonChangedEventArgs(id, pokemon));
        }

        public void SetNewTurn()
        {
            NewTurn(this, EventArgs.Empty);
        }

        public event EventHandler CriticalDamage = delegate { };

        public event EventHandler<MoveEffectiveEventArgs> MoveEffective = delegate { };

        public event EventHandler NewTurn = delegate { };

        public event EventHandler<HPChangedEventArgs> HPChanged = delegate { };

        public event EventHandler<ClientPokemonChangedEventArgs> PokemonChanged = delegate { };

        public event EventHandler<ClientStatusChangedEventArgs> StatusChanged = delegate { };

        public event EventHandler<MoveUsedEventArgs> MoveUsed = delegate { };
    }
}
