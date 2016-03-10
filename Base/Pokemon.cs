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
            this.BaseData = baseData;
            this.name = name;
            this.Level = level;
            this.Stats = stats;
            this.IV = iv;
            this.HP = MaxHP;

            Condition = StatusCondition.Normal;
            Moves = new List<Move>();
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
        public List<Move> Moves { get; set; }
        public String Name { get { return name == null ? BaseData.Name : name; } set { name = value; } }
        public int SpAtk { get { return Stats.SpAtk; } }
        public int SpDef { get { return Stats.SpDef; } }
        public int Speed { get { return Stats.Speed; } }
        public Stats Stats { private get; set; }

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