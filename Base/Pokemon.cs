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
			Level = source.Level;
			Name = source.Name;
			Stats = source.Stats;
			IV = source.IV;
            BaseData = source.BaseData;
			Type1 = source.Type1;
			Type2 = source.Type2;
		}

		public PokemonBuilder(PKData source) {
			Id = source.Id;
			Level = 1;
			Name = source.Name;
            BaseData = source;
			Type1 = source.Type1;
			Type2 = source.Type2;
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
        PKData BaseData { get; set; }
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

        public PokemonBuilder setBaseData(PKData BaseData)
        {
            this.BaseData = BaseData;
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
            if (Name == null || Stats == null || IV == null || BaseData == null)
				throw new InvalidOperationException ("Builder does not have all values");

            return new Pokemon(BaseData, Level, Name, Stats, IV);
		}
	}

	public class Pokemon : ICharakter
	{

		public Pokemon(PKData BaseData, int Level, string Name, Stats Stats, Stats IV)
		{
            this.BaseData = BaseData;
			this.Name = Name;
			this.Level = Level;
			this.Stats = Stats;
			this.IV = IV;
			Condition = StatusCondition.Normal;
		}

        public PKData BaseData { get; set; }
		public int Level { get; set; }

		public Stats IV { get; private set; }
		public Stats Stats { get; set; }

        public List<Move> Moves { get; set; }

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

