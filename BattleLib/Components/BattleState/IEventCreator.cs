using Base;
using Base.Data;
using System;
namespace BattleLib.Components.BattleState
{
    public interface IEventCreator
    {
        void Critical();
        void Effective(MoveEfficiency effect, PokemonWrapper target);
        void SetNewTurn();
        void SetHP(ClientIdentifier id, int hp);
        void SetPokemon(ClientIdentifier id, PokemonWrapper pokemon);
        void SetStatus(PokemonWrapper pokemon, StatusCondition condition);
        void UsingMove(PokemonWrapper source, Base.Move move);
    }

    public class MoveEffectiveEventArgs : EventArgs
    {
        public MoveEfficiency Effect { get; private set; }
        public PokemonWrapper Target { get; private set; }

        public MoveEffectiveEventArgs(MoveEfficiency effect, PokemonWrapper target)
        {
            Effect = effect;
            Target = target;
        }
    }

    public interface IBattleEvents
    {
        event EventHandler CriticalDamage;
        event EventHandler<MoveEffectiveEventArgs> MoveEffective;
        event EventHandler NewTurn;
        event EventHandler<HPChangedEventArgs> HPChanged;
        event EventHandler<ClientPokemonChangedEventArgs> PokemonChanged;
        event EventHandler<ClientStatusChangedEventArgs> StatusChanged;
        event EventHandler<MoveUsedEventArgs> MoveUsed;
    }

    public class HPChangedEventArgs : EventArgs
    {
        public ClientIdentifier ID { get; private set; }
        public int HP { get; private set; }

        public HPChangedEventArgs(ClientIdentifier id, int hp)
        {
            ID = id;
            HP = hp;
        }
    }

    public class ClientPokemonChangedEventArgs : EventArgs
    {
        public PokemonWrapper Pokemon { get; private set; }
        public ClientIdentifier ID { get; private set; }

        public ClientPokemonChangedEventArgs(ClientIdentifier id, PokemonWrapper pokemon)
        {
            ID = id;
            Pokemon = pokemon;
        }
    }

    public class ClientStatusChangedEventArgs : EventArgs
    {
        public StatusCondition Status { get; private set; }
        public ClientIdentifier ID { get; private set; }

        public ClientStatusChangedEventArgs(ClientIdentifier id, StatusCondition status)
        {
            Status = status;
            ID = id;
        }
    }

    public class MoveUsedEventArgs : EventArgs
    {
        public Move Move { get; private set; }
        public PokemonWrapper Source { get; private set; }

        public MoveUsedEventArgs(Move move, PokemonWrapper source)
        {
            Move = move;
            Source = source;
        }
    }
}
