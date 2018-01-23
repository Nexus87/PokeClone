﻿using BattleMode.Shared;
using PokemonShared.Models;

namespace BattleMode.Entities.BattleState
{
    public interface IEventCreator
    {
        void Critical();
        void Effective(MoveEfficiency effect, PokemonEntity target);
        void SetNewTurn();
        void UsingMove(PokemonEntity source, Move move);
    }
}
