using Base;
using Base.Data;
using Base.Rules;
using BattleLib.Components.BattleState;
using BattleLib.Components.BattleState.Commands;
using GameEngine.Registry;
using System;

namespace BattleLib
{
    [GameType]
    public class CommandExecuter
    {
        private readonly IEventCreator eventCreator;
        private readonly IMoveEffectCalculator calculator;

        public CommandExecuter(IMoveEffectCalculator calculator, IEventCreator eventCreator)
        {
            this.eventCreator = eventCreator;
            this.calculator = calculator;
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
            eventCreator.SwitchPokemon(pokemonWrapper);
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
            eventCreator.UsingMove(source, move);
            calculator.Init(source, move, target);
            
            DoDamage(target);
            HandleEfficiency(target);
            CheckIfCritical();
            HandleStatusConditionChange(target);
        }

        private void HandleStatusConditionChange(PokemonWrapper target)
        {

            if (target.Condition != calculator.StatusCondition)
            {
                target.Condition = calculator.StatusCondition;
                eventCreator.SetStatus(target, target.Condition);
            }
        }

        private void CheckIfCritical()
        {
            if (calculator.IsCritical)
                eventCreator.Critical();
        }

        private void DoDamage(PokemonWrapper target)
        {
            var damage = calculator.Damage;
            target.HP -= damage;
            eventCreator.SetHP(target.Identifier, target.HP);
        }

        private void HandleEfficiency(PokemonWrapper target)
        {
            var effect = GetEffect(calculator.TypeModifier);
            eventCreator.Effective(effect, target);
        }
    }
}