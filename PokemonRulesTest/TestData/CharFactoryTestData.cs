using Base;

namespace PokemonRulesTest
{
	public class CharFactoryTestData
	{
		static PKData d1 = new PKData 
			{
			BaseStats = new Stats
				{
					Atk = 10,
					Def = 10,
					HP = 10,
					SpAtk = 10,
					SpDef = 10,
					Speed = 10
				},
				Id = 0,
				Name = "Name1",
			Type1 = PokemonType.Bug,
				Type2 = 0
			};

		static PKData d2 = new PKData {
			BaseStats = new Stats {
				Atk = 1,
				Def = 1,
				HP = 1,
				SpAtk = 1,
				SpDef = 1,
				Speed = 1
			},
			Id = 1,
			Name = "Name2",
			Type1 = PokemonType.Normal,
			Type2 = 0
		};
		static PKData d3 = new PKData {
			BaseStats = new Stats {
				Atk = 2,
				Def = 2,
				HP = 2,
				SpAtk = 2,
				SpDef = 2,
				Speed = 2
			},
			Id = 2,
			Name = "Name3",
			Type1 = PokemonType.Electric,
			Type2 = PokemonType.Normal
		};
		public static PKData[] Data = { d1, d2, d3 };
	}
		
}

