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

namespace Base
{
	public class PokemonBuilder {

		public PokemonBuilder(Pokemon source) {
			Id = source.Id;
			Level = source.Level;
			Name = source.Name;
			Stats = source.Stats;
			IV = source.IV;
			BaseValue = source.BaseValues;
			Type1 = source.Type1;
			Type2 = source.Type2;
		}

		public PokemonBuilder(PKData source) {
			Id = source.id;
			Level = 1;
			Name = source.name;
			BaseValue = source.baseStats;
			Type1 = source.type1;
			Type2 = source.type2;
		}

		public PokemonBuilder() {
			Level = 1;
			Type1 = PokemonType.None;
			Type2 = PokemonType.None;
		}

		int? Id { get; set; }
		int Level { get; set; }
		string Name { get; set; }
		Stats Stats{ get; set; }
		Stats IV { get; set; }
		Stats BaseValue { get; set; }
		PokemonType Type1 { get; set; }
		PokemonType Type2 { get; set; }

		public PokemonBuilder setId( int Id) {
			this.Id = Id;
			return this;
		}

		public PokemonBuilder setLevel(int Level){
			this.Level = Level;
			return this;
		}

		public PokemonBuilder setName( string Name ){
			this.Name = Name;
			return this;
		}
		public PokemonBuilder setIV(Stats IV){
			this.IV = IV;
			return this;
		}

		public PokemonBuilder setBaseValues(Stats BaseValue) {
			this.BaseValue = BaseValue;
			return this;
		}
		public PokemonBuilder setStats(Stats Stats){
			this.Stats = Stats;
			return this;
		}
		public PokemonBuilder setType1 (PokemonType Type1){
			this.Type1 = Type1;
			return this;
		}

		public PokemonBuilder setType2 (PokemonType Type2) {
			this.Type2 = Type2;
			return this;
		}

		public Pokemon build() {
			if (Name == null || Stats == null || Type1 == PokemonType.None || Id == null || IV == null || BaseValue == null)
				throw new InvalidOperationException ("Builder does not have all values");

			return new Pokemon (Id.Value, Level, Name, Stats, IV, BaseValue, Type1, Type2);
		}
	}

	public class Pokemon : ICharakter
	{

		public Pokemon(int Id, int Level, string Name, Stats Stats, Stats IV, Stats BaseValues, PokemonType Type1, PokemonType Type2)
		{
			this.Id = Id;
			this.Name = Name;
			this.Level = Level;
			this.Stats = Stats;
			this.IV = IV;
			this.BaseValues = BaseValues;
			this.Type1 = Type1;
			this.Type2 = Type2;
			Condition = StatusCondition.Normal;
		}

		public int Id { get; private set; }
		public int Level { get; set; }

		public Stats IV { get; private set; }
		public Stats BaseValues { get; private set; }
		public Stats Stats { get; set; }


		public PokemonType Type1 { get; internal set; }
		public PokemonType Type2 { get; internal set; }

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

		public bool isKO ()
		{
			return Condition == StatusCondition.KO;
		}
		#endregion
		
	}
}

