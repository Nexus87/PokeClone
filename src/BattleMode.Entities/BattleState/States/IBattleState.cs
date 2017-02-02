using BattleMode.Shared;
using PokemonShared.Models;

namespace BattleMode.Entities.BattleState.States
{
    public interface IBattleState
    {
        BattleStates State { get; }

        bool IsDone { get; }
        void Init(BattleData data);
        void Update(BattleData data);

        void SetCharacter(ClientIdentifier id, Pokemon pkmn);
        void SetMove(ClientIdentifier id, ClientIdentifier target, Move move);
        void SetItem(ClientIdentifier id, ClientIdentifier target, Item item);
    }
}
