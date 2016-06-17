using Base;
using Base.Data;
using Base.Rules;
using BattleLib.Components.BattleState;
using BattleLib.Components.BattleState.Commands;
using GameEngine.Registry;
using System;

namespace BattleLib
{
    public enum MoveEfficiency
    {
        NoEffect,
        NotEffective,
        Normal,
        VeryEffective
    }

    [GameTypeAttribute]
    public class CommandExecuter
    {
        private IEventCreator eventCreator;
        private IMoveEffectCalculator calculator;

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
            throw new NotImplementedException();
        }

        private static MoveEfficiency GetEffect(float typeModifier)
        {
            if (typeModifier.CompareTo(0) == 0)
                return MoveEfficiency.NoEffect;

            int result = typeModifier.CompareTo(1.0f);

            if (result > 0)
                return MoveEfficiency.VeryEffective;
            else if (result < 0)
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
            int damage = calculator.Damage;
            target.HP -= damage;
            eventCreator.SetHP(target.Identifier, target.HP);
        }

        private void HandleEfficiency(PokemonWrapper target)
        {
            MoveEfficiency effect = GetEffect(calculator.TypeModifier);
            eventCreator.Effective(effect, target);
        }
    }
}