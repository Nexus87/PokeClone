using Base.Data;
using System;
using System.Collections.Generic;

namespace Base
{
    public class Pokemon
    {
        private string _name;
        public Pokemon(PokemonData baseData, int level, string name, Stats stats, Stats iv)
        {
            BaseData = baseData;
            _name = name;
            Level = level;
            Stats = stats;
            Iv = iv;
            Hp = MaxHp;

            Condition = StatusCondition.Normal;
        }

        public Pokemon(PokemonData baseData, Stats iv)
            : this(baseData, 0, null, new Stats(), iv)
        { }

        public int Atk => Stats.Atk;
        public PokemonData BaseData { get; }
        public StatusCondition Condition { get; set; }
        public int Def => Stats.Def;
        public int Hp { get; set; }

        public int Id => BaseData.Id;

        public Stats Iv { get; private set; }
        public int Level { get; set; }
        public int MaxHp => Stats.Hp;
        public IReadOnlyList<Move> Moves => _moves.AsReadOnly();
        public string Name { get { return _name ?? BaseData.Name; } set { _name = value; } }
        public int SpAtk => Stats.SpAtk;
        public int SpDef => Stats.SpDef;
        public int Speed => Stats.Speed;
        public Stats Stats { get; set; }

        public PokemonType Type1 => BaseData.Type1;
        public PokemonType Type2 => BaseData.Type2;

        private readonly List<Move> _moves = new List<Move>();

        public void SetMove(int index, Move move)
        {
            if (index > 4)
                throw new InvalidOperationException("Only 4 moves are allowed");
            else if (index < 0)
                throw new InvalidOperationException("Index must be positive");

            if (index >= _moves.Count)
                _moves.Add(move);
            else
                _moves[index] = move;
            
        }
        public bool IsKo()
        {
            return Condition == StatusCondition.KO;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}