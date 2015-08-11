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

using BattleLib;

namespace PokemonRules
{

	public class PokemonV1 : ICharakter
	{

		public PokemonV1(int maxHP, string name) {
			MaxHP = maxHP;
			_hp = maxHP;
			Name = name;
			Condition = StatusCondition.Normal;
			resetModifier();
		}

		int _attack;
		int _defense;
		int _spAtk;
		int _spDef;
		int _speed;

		public int MaxHP { get; private set; }
		public int Attack { get{return _attack + AttackModifer;} set{_attack = value;} }
		public int Defense { get{ return _defense + DefenseModifer;} set{_defense = value;} }
		public int SpAtk { get{ return _spAtk + SpAtkModifer;} set{_spAtk = value;} }
		public int SpDef { get{ return _spDef + SpDefModifer;} set{_spDef = value;} }
		public int Speed { get{ return _speed + SpeedModifer;} set{_speed = value;} }

		int AttackModifer { get; public set; }
		int DefenseModifer { get; public set; }
		int SpAtkModifer { get; public set; }
		int SpDefModifer { get; public set; }
		int SpeedModifer { get; public set; }

		public void resetModifier() {
			AttackModifer = 0;
			DefenseModifer = 0;
			SpAtkModifer = 0;
			SpDefModifer = 0;
			SpeedModifer = 0;
		}

		public PokemonType Type1 { get; private set; }
		public PokemonType Type2 { get; private set; }

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
				} else if (value > MaxHP)
					_hp = MaxHP;
				else
					_hp = value;
			} 
		}

		public string Status {
			get {
				return Condition.ToString ();
			}
		}
			
		public bool isKO ()
		{
			return Condition == StatusCondition.KO;
		}
		#endregion
		
	}
}

