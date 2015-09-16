//
//  EmptyClass.cs
//
//  Author:
//       Nexxuz0 <>
//
//  Copyright (c) 2015 Nexxuz0
//
//  This program is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 2 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
//
using System;
using System.Collections.Generic;

namespace Base
{
	public class PokemonBuilder {

		public PokemonBuilder(Pokemon source) {
            if (source == null) throw new ArgumentNullException("source", "Argument should not be null");

			Level = source.Level;
			Name = source.Name;
			Stats = source.Stats;
			IV = source.IV;
            BaseData = source.BaseData;
		}

		public PokemonBuilder(PKData source) {
            if (source == null) throw new ArgumentNullException("source", "Argument should not be null");

			Level = 1;
			Name = source.Name;
            BaseData = source;
		}

		public PokemonBuilder() {
			Level = 1;
		}

		int Level { get; set; }
		string Name { get; set; }
		Stats Stats{ get; set; }
		Stats IV { get; set; }
        PKData BaseData { get; set; }


		public PokemonBuilder SetLevel(int level){
            this.Level = level;
			return this;
		}

		public PokemonBuilder SetName( string name ){
            this.Name = name;
			return this;
		}
		public PokemonBuilder SetIV(Stats iv){
            this.IV = iv;
			return this;
		}

        public PokemonBuilder SetBaseData(PKData baseData)
        {
            this.BaseData = baseData;
			return this;
		}
		public PokemonBuilder SetStats(Stats stats){
            this.Stats = stats;
			return this;
		}

		public Pokemon Build() {
            if (Name == null || Stats == null || IV == null || BaseData == null)
				throw new InvalidOperationException ("Builder does not have all values");

            return new Pokemon(BaseData, Level, Name, Stats, IV);
		}
	}

	public class Pokemon : ICharacter
	{

		public Pokemon(PKData baseData, int level, string name, Stats stats, Stats iv)
		{
            this.BaseData = baseData;
			this.Name = name;
			this.Level = level;
			this.Stats = stats;
			this.IV = iv;
			Condition = StatusCondition.Normal;
            Moves = new List<Move>();
		}

        public Pokemon(PKData baseData, Stats iv) : this(baseData, 1, baseData.Name, baseData.BaseStats, iv)
        {}

        public PKData BaseData { get; set; }
		public int Level { get; set; }

		public Stats IV { get; private set; }
		public Stats Stats { get; set; }

        public List<Move> Moves { get; private set; }

		public PokemonType Type1 { get{ return BaseData.Type1; }}
        public PokemonType Type2 { get { return BaseData.Type2; }}

		public StatusCondition Condition { get; set; }

		int _hp;


		#region ICharakter implementation

		public string Name { get; private set; }
		public int HP { 
			get{ return _hp; }
			set{
				if (value <= 0) {
					_hp = 0;
					Condition = StatusCondition.KO;
				} else if (value > Stats.HP)
					_hp = Stats.HP;
				else
					_hp = value;
			} 
		}

		public bool IsKO ()
		{
			return Condition == StatusCondition.KO;
		}
		#endregion


        public int Id
        {
            get { return BaseData.Id;  }
        }
    }
}

