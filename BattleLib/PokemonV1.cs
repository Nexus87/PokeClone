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

namespace BattleLib
{

	public class PokemonV1 : ICharakter
	{
		public enum StatusCondition {
			Normal,
			KO,
			Paralyzed,
			Sleeping,
			Poisoned,
			Freezed,
			Burned
		}

		public PokemonV1(int maxHP, string name) {
			MaxHP = maxHP;
			_hp = maxHP;
			Name = name;
			Condition = StatusCondition.Normal;
		}

		public int MaxHP { get; private set; }
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

