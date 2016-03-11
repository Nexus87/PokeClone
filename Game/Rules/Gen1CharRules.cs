﻿using Base;
using Base.Data;
using Base.Factory;
using Base.Rules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonGame.Rules
{
    public delegate int RndNumGen(int min, int max);

    public class Gen1CharRules : IPokemonRules
    {
        private readonly RndNumGen _generator;
        private MoveFactory _factory;
        private Random _rnd;

        public Gen1CharRules(MoveFactory factory)
            : this(factory, null)
        { }

        public Gen1CharRules(MoveFactory factory, RndNumGen gen)
        {
            if (gen == null)
            {
                _rnd = new Random();
                _generator = _rnd.Next;
            }
            else
                _generator = gen;
            _factory = factory;
        }

        public Pokemon FromPokemonData(PokemonData data)
        {
            if (data == null)
                return null;

            var iStats = GenerateIV();

            var stats = new Stats
            {
                HP = data.BaseStats.HP + iStats.HP,
                Atk = data.BaseStats.Atk + iStats.Atk,
                Def = data.BaseStats.Def + iStats.Def,
                SpAtk = data.BaseStats.SpAtk + iStats.SpAtk,
                SpDef = data.BaseStats.SpDef + iStats.SpDef,
                Speed = data.BaseStats.Speed + iStats.Speed
            };

            //var builder = new PokemonBuilder(data);
            //builder.SetIV (iStats).SetStats (stats);
            //return builder.Build ();
            throw new NotImplementedException();
        }

        public Stats GenerateIV()
        {
            return new Stats
            {
                Atk = _generator(0, 15),
                Def = _generator(0, 15),
                HP = _generator(0, 15),
                SpAtk = _generator(0, 15),
                SpDef = _generator(0, 15),
                Speed = _generator(0, 15)
            };
        }

        public IEnumerable<Move> LevelUp(Pokemon character)
        {
            if (character == null) throw new ArgumentNullException("character", "Argument should not be null");

            if (character.Level == 100)
                return new List<Move>();

            ToLevel(character, character.Level + 1);

            return from moves in character.BaseData.MoveList.Moves
                   where moves.Item1 == character.Level
                   select _factory.GetMove(moves.Item2);
        }

        public void ToLevel(Pokemon character, int level)
        {
            if (character == null) throw new ArgumentNullException("character", "Argument should not be null");

            var baseStates = character.BaseData.BaseStats;
            var ivStates = character.IV;
            var stats = new Stats();

            stats.HP = newState(baseStates.HP, ivStates.HP + 50.0d, level);
            stats.Atk = newState(baseStates.Atk, ivStates.Atk, level);
            stats.Def = newState(baseStates.Def, ivStates.Def, level);
            stats.SpAtk = newState(baseStates.SpAtk, ivStates.SpAtk, level);
            stats.SpDef = newState(baseStates.SpDef, ivStates.SpDef, level);
            stats.Speed = newState(baseStates.Speed, ivStates.Speed, level);

            character.Stats = stats;
            character.Level++;
        }

        private static int newState(double baseV, double iV, double level)
        {
            return (int)Math.Floor((baseV + iV) * level / 50.0d + 10.0d);
        }

    }
}