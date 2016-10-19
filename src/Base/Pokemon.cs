using Base.Data;
using System;
using System.Collections.Generic;

namespace Base
{
    public class Pokemon
    {
        private string name;
        public Pokemon(PokemonData baseData, int level, string name, Stats stats, Stats iv)
        {
            BaseData = baseData;
            this.name = name;
            Level = level;
            Stats = stats;
            IV = iv;
            HP = MaxHP;

            Condition = StatusCondition.Normal;
        }

        public Pokemon(PokemonData baseData, Stats iv)
            : this(baseData, 0, null, new Stats(), iv)
        { }

        public int Atk { get { return Stats.Atk; } }
        public PokemonData BaseData { get; private set; }
        public StatusCondition Condition { get; set; }
        public int Def { get { return Stats.Def; } }
        public int HP { get; set; }

        public int Id { get { return BaseData.Id; } }

        public Stats IV { get; private set; }
        public int Level { get; set; }
        public int MaxHP { get { return Stats.HP; } }
        public IReadOnlyList<Move> Moves { get { return moves.AsReadOnly(); } }
        public string Name { get { return name == null ? BaseData.Name : name; } set { name = value; } }
        public int SpAtk { get { return Stats.SpAtk; } }
        public int SpDef { get { return Stats.SpDef; } }
        public int Speed { get { return Stats.Speed; } }
        public Stats Stats { get; set; }

        public PokemonType Type1 { get { return BaseData.Type1; } }
        public PokemonType Type2 { get { return BaseData.Type2; } }

        private readonly List<Move> moves = new List<Move>();
        public void SetMove(int index, Move move)
        {
            if (index > 4)
                throw new InvalidOperationException("Only 4 moves are allowed");
            else if (index < 0)
                throw new InvalidOperationException("Index must be positive");

            if (index >= moves.Count)
                moves.Add(move);
            else
                moves[index] = move;
            
        }
        public bool IsKO()
        {
            return Condition == StatusCondition.KO;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}