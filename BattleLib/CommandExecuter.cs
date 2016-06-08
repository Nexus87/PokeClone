using Base;
using Base.Data;
using Base.Rules;
using BattleLib.Components.BattleState;
using BattleLib.Components.BattleState.Commands;
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

    public class CommandExecuter
    {
        private IEventCreator eventCreator;
        private IMoveEffectCalculator calculator;
        public CommandExecuter(IMoveEffectCalculator calculator, IEventCreator eventCreator)
        {
            this.eventCreator = eventCreator;
            this.calculator = calculator;
        }

        private void ExecMove(PokemonWrapper source, Move move, PokemonWrapper target)
        {
            eventCreator.UsingMove(source, move);

            switch (move.DamageType)
            {
                case DamageCategory.Physical:
                case DamageCategory.Special:
                    HandleDamage(source, target, move);
                    break;
                case DamageCategory.Status:
                    throw new NotImplementedException();
            }
        }

        public BattleData Data{get;set;}

        public void DispatchCommand(ItemCommand command)
        {
            UseItem(Data.GetPokemon(command.Source), command.Item);
        }

        public void DispatchCommand(MoveCommand command)
        {
            ExecMove(Data.GetPokemon(command.Source), command.Move, Data.GetPokemon(command.Target));
        }

        public void DispatchCommand(ChangeCommand command)
        {
            ChangePokemon(Data.GetPokemon(command.Source), command.Pokemon);
        }

        private void HandleDamage(PokemonWrapper source, PokemonWrapper target, Move move)
        {
            calculator.Init(source, move, target);

            int damage = calculator.Damage;
            MoveEfficiency effect = GetEffect(calculator.TypeModifier);
            bool critical = calculator.IsCritical;

            target.HP -= damage;

            eventCreator.SetHP(target.Identifier, target.HP);
            eventCreator.Effective(effect, target);

            if (critical)
                eventCreator.Critical();

            if (target.Condition == StatusCondition.KO)
                eventCreator.SetStatus(target, StatusCondition.KO);
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

        private void ChangePokemon(PokemonWrapper oldPokemon, Pokemon newPokemon)
        {
            throw new NotImplementedException();
        }

        private bool UseItem(PokemonWrapper target, Item item)
        {
            throw new NotImplementedException();
        }

        private void HandleStatusDamage(PokemonWrapper source, Move move, PokemonWrapper target)
        {
            throw new NotImplementedException();
        }

    }
}