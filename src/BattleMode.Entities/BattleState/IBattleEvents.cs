using System;

namespace BattleMode.Entities.BattleState
{
    public interface IBattleEvents
    {
        event EventHandler CriticalDamage;
        event EventHandler<MoveEffectiveEventArgs> MoveEffective;
        event EventHandler NewTurn;
        event EventHandler<HpChangedEventArgs> HpChanged;
        event EventHandler<ClientPokemonChangedEventArgs> PokemonChanged;
        event EventHandler<ClientStatusChangedEventArgs> StatusChanged;
        event EventHandler<MoveUsedEventArgs> MoveUsed;
    }
}