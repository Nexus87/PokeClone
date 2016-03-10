using Base.Data;
using System;
using System.Collections.Generic;

namespace Base
{
    public class Pokemon
    {
        public Pokemon(PokemonData baseData, int level, string name, Stats stats, Stats iv)
        {
            this.BaseData = baseData;
            this.Name = name;
            this.Level = level;
            this.Stats = stats;
            this.IV = iv;
            this.HP = MaxHP;
            Condition = StatusCondition.Normal;
            Moves = new List<Move>();
        }

        public Pokemon(PokemonData baseData, Stats iv)
            : this(baseData, 1, baseData.Name, baseData.BaseStats, iv)
        { }

        public PokemonData BaseData { get; private set; }
        public StatusCondition Condition { get; set; }
        public int HP { get; set; }

        public int Id
        {
            get { return BaseData.Id; }
        }

        public Stats IV { get; private set; }
        public int Level { get; set; }
        public int MaxHP { get { return Stats.HP; } }
        public List<Move> Moves { get; set; }
        public String Name { get; set; }
        public Stats Stats { get; private set; }
        public PokemonType Type1 { get { return BaseData.Type1; } }
        public PokemonType Type2 { get { return BaseData.Type2; } }

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