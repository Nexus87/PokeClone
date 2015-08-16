﻿using System;
using Base;

namespace PokemonRules
{
	public delegate int RndNumGen(int min, int max) ;

	public class Gen1CharRules : CharacterRules
	{
		readonly RndNumGen _generator;
		Random _rnd;

		public Gen1CharRules(RndNumGen gen = null){
			if (gen == null) {
				_rnd = new Random ();
				_generator = _rnd.Next;
			} else
				_generator = gen;
		}

		#region CharacterRules implementation

		static int newState(double baseV, double iV, double level) {
			return (int) Math.Floor ((baseV + iV) * level / 50.0d + 10.0d);
		}

		public void levelUp (Pokemon charakter)
		{
			if (charakter.Level == 100)
				return;
			
			toLevel (charakter, charakter.Level + 1);
		}

		public void toLevel (Pokemon charakter, int level)
		{
			var baseStates = charakter.BaseValues;
			var ivStates = charakter.IV;

			var newStats = new Stats{
				HP = newState (baseStates.HP, ivStates.HP + 50.0d, level),
				Atk = newState (baseStates.Atk, ivStates.Atk, level),
				Def = newState (baseStates.Def, ivStates.Def, level),
				SpAtk = newState (baseStates.SpAtk, ivStates.SpAtk, level),
				SpDef = newState (baseStates.SpDef, ivStates.SpDef, level),
				Speed = newState (baseStates.Speed, ivStates.Speed, level)
			};

			charakter.Stats = newStats;
			charakter.Level++;
		}

		public Pokemon toPokemon (PKData data)
		{
			if (data == null)
				return null;

			var iStats = generateIV ();

			var stats = new Stats {
				HP = data.baseStats.HP + iStats.HP,
				Atk = data.baseStats.Atk + iStats.Atk,
				Def = data.baseStats.Def + iStats.Def,
				SpAtk = data.baseStats.SpAtk + iStats.SpAtk,
				SpDef = data.baseStats.SpDef + iStats.SpDef,
				Speed = data.baseStats.Speed + iStats.Speed
			};

			var builder = new PokemonBuilder(data);
			builder.setIV (iStats).setStats (stats);
			return builder.build ();
		}


		public Stats generateIV ()
		{
			return new Stats {
				Atk = _generator (0, 15),
				Def = _generator (0, 15),
				HP = _generator (0, 15),
				SpAtk = _generator (0, 15),
				SpDef = _generator (0, 15),
				Speed = _generator (0, 15)
			};
		}
		#endregion
	}
}

