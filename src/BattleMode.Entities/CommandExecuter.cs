using System;
using Base;
using Base.Rules;
using BattleMode.Entities.BattleState;
using BattleMode.Entities.BattleState.Commands;
using BattleMode.Shared;
using GameEngine.TypeRegistry;

namespace BattleMode.Entities
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
            ExecChange(Data.GetPokemon(command.Source), command.Pokemon);
        }

        private void ExecChange(PokemonWrapper pokemonWrapper, Pokemon newPokemon)
        {
            pokemonWrapper.Pokemon = newPokemon;
            _eventCreator.SwitchPokemon(pokemonWrapper);
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

        private void ExecMove(PokemonWrapper source, Move move, PokemonWrapper target)
        {
            _eventCreator.UsingMove(source, move);
            _calculator.Init(source, move, target);
            
            DoDamage(target);
            HandleEfficiency(target);
            CheckIfCritical();
            HandleStatusConditionChange(target);
        }

        private void HandleStatusConditionChange(PokemonWrapper target)
        {

            if (target.Condition != _calculator.StatusCondition)
            {
                target.Condition = _calculator.StatusCondition;
                _eventCreator.SetStatus(target, target.Condition);
            }
        }

        private void CheckIfCritical()
        {
            if (_calculator.IsCritical)
                _eventCreator.Critical();
        }

        private void DoDamage(PokemonWrapper target)
        {
            var damage = _calculator.Damage;
            target.HP -= damage;
            _eventCreator.SetHp(target.Identifier, target.HP);
        }

        private void HandleEfficiency(PokemonWrapper target)
        {
            var effect = GetEffect(_calculator.TypeModifier);
            _eventCreator.Effective(effect, target);
        }
    }
}