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
			return d.name.Equals(other.name) &&
				d.id == other.id &&
				d.baseStats.HP == other.baseStats.HP &&
				d.baseStats.Atk == other.baseStats.Atk &&
				d.baseStats.Def == other.baseStats.Def &&
				d.baseStats.SpAtk == other.baseStats.SpAtk &&
				d.baseStats.SpDef == other.baseStats.SpDef &&
				d.baseStats.Speed == other.baseStats.Speed &&
				d.type1 == other.type1 &&
				d.type2 == other.type2;
		}

		public static bool compare(this Pokemon p, object obj) {
			var data = obj as PKData;
			if (data == null)
				return false;
			
			return p.Stats.Atk == data.baseStats.Atk &&
				p.Stats.Def == data.baseStats.Def &&
				p.Stats.SpAtk == data.baseStats.SpAtk &&
				p.Stats.SpDef == data.baseStats.SpDef &&
				p.Stats.Speed == data.baseStats.Speed &&
				p.Stats.HP == data.baseStats.HP &&
				p.Name == data.name &&
				p.Type1 == data.type1 &&
				p.Type2 == data.type2;
		}
	}

    [TestFixture]
    class CharFactoryTest
    {
		static readonly PKData testData = new PKData
        {
			baseStats = new Stats{
            	Atk = 10,
            	Def = 11,
            	HP = 12,
				SpAtk = 15,
				SpDef = 16,
				Speed = 14,
			},
            id = 13,            
            name = "Name",
            type1 = PokemonType.Bug,
            type2 = PokemonType.None
        };


        CharFactory _factory;
        [SetUp]
        public void init()
        {
			_factory = new CharFactory("./Test.txt", new Gen1CharRules());
        }
        [TestCase]
        public void simpleReadWriteTest()
        {

			var result = new PKData();
            Assert.DoesNotThrow(() => _factory.writeData(testData));
            Assert.DoesNotThrow(() => result = _factory.getData());

			Assert.IsTrue(result.compare(testData));
        }

        [TestCase]
        public void getCharTest()
        {
            Pokemon result = null;
            Assert.DoesNotThrow(() => _factory.writeData(testData));
            Assert.DoesNotThrow(() => result = _factory.getChar(testData.name));

            Assert.NotNull(result);
			Assert.IsTrue (result.compare(testData));
        }
    }
}
