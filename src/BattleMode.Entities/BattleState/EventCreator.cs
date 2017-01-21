using System;
using System.Linq;
using Base;
using Base.Rules;
using BattleMode.Shared;
using GameEngine.TypeRegistry;

namespace BattleMode.Entities.BattleState
{
    [GameService(typeof(IEventCreator))]
    public class EventCreator : IEventCreator
    {
        public EventCreator(BattleData data)
        {
            var ids = data.Clients.ToDictionary(data.GetPokemon, x => x);

            foreach (var pokemonEntity in ids.Keys)
            {
                pokemonEntity.HpChanged += (sender, args) =>
                    HpChanged?.Invoke(this, new HpChangedEventArgs(ids[pokemonEntity], args.NewHp));

                pokemonEntity.PokemonChanged += (sender, args) =>
                    PokemonChanged?.Invoke(this, new ClientPokemonChangedEventArgs(ids[pokemonEntity], pokemonEntity));

                pokemonEntity.StatusChanged += (sender, args) =>
                    StatusChanged?.Invoke(this, new ClientStatusChangedEventArgs(ids[pokemonEntity], args.NewCondition));

            }
        }

        public void UsingMove(PokemonEntity source, Move move)
        {
            MoveUsed?.Invoke(this, new MoveUsedEventArgs(move, source));
        }


        public void Effective(MoveEfficiency effect, PokemonEntity target)
        {
            MoveEffective?.Invoke(this, new MoveEffectiveEventArgs(effect, target));
        }

        public void Critical()
        {
            CriticalDamage?.Invoke(this, EventArgs.Empty);
        }

        public void SetNewTurn()
        {
            NewTurn?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CriticalDamage;
        public event EventHandler<MoveEffectiveEventArgs> MoveEffective;
        public event EventHandler NewTurn;
        public event EventHandler<HpChangedEventArgs> HpChanged ;
        public event EventHandler<ClientPokemonChangedEventArgs> PokemonChanged;
        public event EventHandler<ClientStatusChangedEventArgs> StatusChanged;
        public event EventHandler<MoveUsedEventArgs> MoveUsed;
    }
}
