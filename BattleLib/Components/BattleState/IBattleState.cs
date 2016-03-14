using Base;

namespace BattleLib.Components.BattleState
{
    public interface IBattleState
    {
        BattleStates State { get; }
        IBattleState Update(BattleData data);

        void SetCharacter(ClientIdentifier id, Pokemon pkmn);
        void SetMove(ClientIdentifier id, ClientIdentifier target, Move move);
        void SetItem(ClientIdentifier id, ClientIdentifier target, Item item);
    }
}
