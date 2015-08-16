using Base;

namespace PokemonRulesTest
{
	public class CharFactoryTestData
	{
		static PKData d1 = new PKData 
			{
			baseStats = new Stats
				{
					Atk = 10,
					Def = 10,
					HP = 10,
					SpAtk = 10,
					SpDef = 10,
					Speed = 10
				},
				id = 0,
				name = "Name1",
			type1 = PokemonType.Bug,
				type2 = 0
			};

		static PKData d2 = new PKData {
			baseStats = new Stats {
				Atk = 1,
				Def = 1,
				HP = 1,
				SpAtk = 1,
				SpDef = 1,
				Speed = 1
			},
			id = 1,
			name = "Name2",
			type1 = PokemonType.Normal,
			type2 = 0
		};
		static PKData d3 = new PKData {
			baseStats = new Stats {
				Atk = 2,
				Def = 2,
				HP = 2,
				SpAtk = 2,
				SpDef = 2,
				Speed = 2
			},
			id = 2,
			name = "Name3",
			type1 = PokemonType.Electric,
			type2 = PokemonType.Normal
		};
		public static PKData[] Data = { d1, d2, d3 };
	}
		
}

