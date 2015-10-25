using Base;
using Moq;
using NUnit.Framework;
using PokemonRules;
using System.Collections.Generic;
using System.Linq;

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

        Mock<ICharRepository> _charRepositoryMock;
        CharFactory _factory;
		Mock<ICharacterRules> _rulesMock;
        List<PKData> _testData = new List<PKData>();
        List<Pokemon> _testChar = new List<Pokemon>();

        [SetUp]
        public void init()
        {
            _charRepositoryMock = new Mock<ICharRepository>();
            _rulesMock = new Mock<ICharacterRules>();
			_factory = new CharFactory(_charRepositoryMock.Object, _rulesMock.Object);

            Stats testStats = new Stats();
            _testData.Add(new PKData{Id = 0, BaseStats = testStats, Name = "Data1"});
            _testData.Add(new PKData { Id = 1, BaseStats = testStats, Name = "Data2" });
            _testData.Add(new PKData { Id = 2, BaseStats = testStats, Name = "Data3" });

            foreach(var data in _testData)
                _testChar.Add(new Pokemon(data, testStats));
        }

        [TestCase]
        public void GetCharTest()
        {
            for (int i = 0; i < _testData.Count; i++)
            {
                var retData = _testData[i];
                var retChar = _testChar[i];

                _charRepositoryMock.Setup(rep => rep.getPKData(retData.Id)).Returns(retData).Verifiable();
                _rulesMock.Setup(rules => rules.ToPokemon(retData)).Returns(retChar).Verifiable();

                var result = _factory.GetChar(retData.Id);

                Assert.AreEqual(result, retChar);
                _charRepositoryMock.Verify();
                _rulesMock.Verify();
            }

        }

        [TestCase]
        public void GetCharLeveledTest()
        {
            for (int i = 0; i < _testData.Count; i++)
            {
                var retData = _testData[i];
                var retChar = _testChar[i];
                int retLevel = 0;
                int testLevel = 15;
                _charRepositoryMock.Setup(rep => rep.getPKData(retData.Id)).Returns(retData).Verifiable();
                _rulesMock.Setup(rules => rules.ToPokemon(retData)).Returns(retChar).Verifiable();
                _rulesMock.Setup(rules => rules.ToLevel(retChar, It.IsAny<int>())).Callback<Pokemon, int>( (pkm, lvl) => retLevel = lvl).Verifiable();

                var result = _factory.GetChar(retData.Id, testLevel);

                Assert.AreEqual(result, retChar);
                Assert.AreEqual(retLevel, testLevel);
                _charRepositoryMock.Verify();
                _rulesMock.Verify();
            }
        }
        [TestCase]
        public void GetIdTest()
        {
            var ids = from data in _testData
                      select data.Id;
            _charRepositoryMock.Setup(rep => rep.Ids).Returns(ids);

            var result = _factory.Ids;
            Assert.AreEqual(result, ids);
        }
    }
}
