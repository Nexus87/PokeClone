using NUnit.Framework;

using PokemonRules;
using Base;

namespace PokemonRulesTest
{
	static class Extensions {
		public static bool compare(this PKData d, object obj) {
			if (obj == null || !(obj is PKData))
				return false;

			var other = (PKData)obj;
			return d.Name.Equals(other.Name) &&
				d.Id == other.Id &&
				d.BaseStats.HP == other.BaseStats.HP &&
				d.BaseStats.Atk == other.BaseStats.Atk &&
				d.BaseStats.Def == other.BaseStats.Def &&
				d.BaseStats.SpAtk == other.BaseStats.SpAtk &&
				d.BaseStats.SpDef == other.BaseStats.SpDef &&
				d.BaseStats.Speed == other.BaseStats.Speed &&
				d.Type1 == other.Type1 &&
				d.Type2 == other.Type2;
		}

		public static bool compare(this Pokemon p, object obj) {
			var data = obj as PKData;
			if (data == null)
				return false;
			
			return p.Stats.Atk == data.BaseStats.Atk &&
				p.Stats.Def == data.BaseStats.Def &&
				p.Stats.SpAtk == data.BaseStats.SpAtk &&
				p.Stats.SpDef == data.BaseStats.SpDef &&
				p.Stats.Speed == data.BaseStats.Speed &&
				p.Stats.HP == data.BaseStats.HP &&
				p.Name == data.Name &&
				p.Type1 == data.Type1 &&
				p.Type2 == data.Type2;
		}
	}

    [TestFixture]
    class CharFactoryTest
    {
		static int PseudoRandom(int min, int max){
			return 0;
		}

        CharFactory _factory;
		CharacterRules _rules;
        [SetUp]
        public void init()
        {
			_rules = new Gen1CharRules (new MoveFactory(""), PseudoRandom);
			_factory = new CharFactory("../../TestData/CharFactoryTestData.txt", _rules);
        }

        [TestCase]
        public void getCharTest()
        {
            Pokemon result = null;
			foreach (var data in CharFactoryTestData.Data) {
				Assert.DoesNotThrow (() => result = _factory.getChar (data.Id));
				Assert.NotNull (result);
				Assert.IsTrue (result.compare ( data ));
			}
        }
    }
}
