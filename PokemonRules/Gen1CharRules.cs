using System;
using Base;

namespace PokemonRules
{
	public class Gen1CharRules : CharacterRules
	{
		#region CharacterRules implementation

		public void levelUp (Pokemon charakter)
		{
			throw new NotImplementedException ();
		}

		public void toLevel (Pokemon charakter, int level)
		{
			throw new NotImplementedException ();
		}

		public Pokemon toPokemon (PKData data)
		{
			if (data == null)
				return null;

			var iStats = generateIV ();

			var stats = new Stats () {
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
			var r = new Random ();
			return new Stats () {
				Atk = r.Next (0, 15),
				Def = r.Next (0, 15),
				HP = r.Next (0, 15),
				SpAtk = r.Next (0, 15),
				SpDef = r.Next (0, 15),
				Speed = r.Next (0, 15)
			};
		}
		#endregion
	}
}

