using System;

namespace BattleMode.Components.BattleState
{
    public interface IBattleEvents
    {
        event EventHandler CriticalDamage;
        event EventHandler<MoveEffectiveEventArgs> MoveEffective;
        event EventHandler NewTurn;
        event EventHandler<HpChangedEventArgs> HPChanged;
        event EventHandler<ClientPokemonChangedEventArgs> PokemonChanged;
        event EventHandler<ClientStatusChangedEventArgs> StatusChanged;
        event EventHandler<MoveUsedEventArgs> MoveUsed;
    }
}