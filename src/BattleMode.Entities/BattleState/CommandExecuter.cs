using System;
using BattleMode.Entities.BattleState.Commands;
using BattleMode.Shared;
using GameEngine.TypeRegistry;
using PokemonShared.Models;

namespace BattleMode.Entities.BattleState
{
    [GameType]
    public class CommandExecuter
    {
        private readonly IEventCreator _eventCreator;
        private readonly IMoveEffectCalculator _calculator;

        public CommandExecuter(IMoveEffectCalculator calculator, IEventCreator eventCreator)
        {
            _eventCreator = eventCreator;
            _calculator = calculator;
        }

        public BattleData Data { get; set; }

        public void DispatchCommand(ItemCommand command)
        {
            throw new NotImplementedException();
        }

        public void DispatchCommand(MoveCommand command)
        {
            ExecMove(Data.GetPokemon(command.Source), command.Move, Data.GetPokemon(command.Target));
        }

        public void DispatchCommand(ChangeCommand command)
        {
            var pokemonEntity = Data.GetPokemon(command.Source);
            pokemonEntity.Pokemon = command.Pokemon;
        }

        private static MoveEfficiency GetEffect(float typeModifier)
        {
            if (typeModifier.CompareTo(0) == 0)
                return MoveEfficiency.NoEffect;

            var result = typeModifier.CompareTo(1.0f);

            if (result > 0)
                return MoveEfficiency.VeryEffective;
            if (result < 0)
                return MoveEfficiency.NotEffective;

            return MoveEfficiency.Normal;
        }

        private void ExecMove(PokemonEntity source, Move move, PokemonEntity target)
        {
            _eventCreator.UsingMove(source, move);
            _calculator.Init(source, move, target);
            
            DoDamage(target);
            HandleEfficiency(target);
            CheckIfCritical();
            HandleStatusConditionChange(target);
        }

        private void HandleStatusConditionChange(PokemonEntity target)
        {
            if (target.Condition == _calculator.StatusCondition)
                return;

            target.Condition = _calculator.StatusCondition;
        }

        private void CheckIfCritical()
        {
            if (_calculator.IsCritical)
                _eventCreator.Critical();
        }

        private void DoDamage(PokemonEntity target)
        {
            var damage = _calculator.Damage;
            target.Hp -= damage;
        }

        private void HandleEfficiency(PokemonEntity target)
        {
            var effect = GetEffect(_calculator.TypeModifier);
            _eventCreator.Effective(effect, target);
        }
    }
}