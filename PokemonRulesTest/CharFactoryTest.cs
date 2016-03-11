using Base;
using Base.Data;
using Base.Factory;
using Base.Rules;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace PokemonRulesTest
{
	static class Extensions {
		public static bool compare(this PokemonData d, object obj) {
            if (obj == null || !(obj is PokemonData))
				return false;

            var other = (PokemonData)obj;
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
            var data = obj as PokemonData;
			if (data == null)
				return false;
			
			return p.Atk == data.BaseStats.Atk &&
				p.Def == data.BaseStats.Def &&
				p.SpAtk == data.BaseStats.SpAtk &&
				p.SpDef == data.BaseStats.SpDef &&
				p.Speed == data.BaseStats.Speed &&
				p.HP == data.BaseStats.HP &&
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

        Mock<IPokemonRepository> charRepositoryMock;
        PokemonFactory factory;
		Mock<IPokemonRules> rulesMock;
        List<PokemonData> testData = new List<PokemonData>();
        List<Pokemon> testChar = new List<Pokemon>();

        [SetUp]
        public void Setup()
        {
            charRepositoryMock = new Mock<IPokemonRepository>();
            rulesMock = new Mock<IPokemonRules>();

            factory = new PokemonFactory(charRepositoryMock.Object, rulesMock.Object);

            Stats testStats = new Stats();
            testData.Add(new PokemonData { Id = 0, BaseStats = testStats, Name = "Data1" });
            testData.Add(new PokemonData { Id = 1, BaseStats = testStats, Name = "Data2" });
            testData.Add(new PokemonData { Id = 2, BaseStats = testStats, Name = "Data3" });

            foreach(var data in testData)
                testChar.Add(new Pokemon(data, testStats));
        }

        [TestCase]
        public void GetCharTest()
        {
            for (int i = 0; i < testData.Count; i++)
            {
                var retData = testData[i];
                var retChar = testChar[i];

                charRepositoryMock.Setup(rep => rep.GetPokemonData(retData.Id)).Returns(retData).Verifiable();
                rulesMock.Setup(rules => rules.FromPokemonData(retData)).Returns(retChar).Verifiable();

                var result = factory.GetPokemon(retData.Id);

                Assert.AreEqual(result, retChar);
                charRepositoryMock.Verify();
                rulesMock.Verify();
            }

        }

        [TestCase]
        public void GetCharLeveledTest()
        {
            for (int i = 0; i < testData.Count; i++)
            {
                var retData = testData[i];
                var retChar = testChar[i];
                int retLevel = 0;
                int testLevel = 15;
                charRepositoryMock.Setup(rep => rep.GetPokemonData(retData.Id)).Returns(retData).Verifiable();
                rulesMock.Setup(rules => rules.FromPokemonData(retData)).Returns(retChar).Verifiable();
                rulesMock.Setup(rules => rules.ToLevel(retChar, It.IsAny<int>())).Callback<Pokemon, int>( (pkm, lvl) => retLevel = lvl).Verifiable();

                var result = factory.GetPokemon(retData.Id, testLevel);

                Assert.AreEqual(result, retChar);
                Assert.AreEqual(retLevel, testLevel);
                charRepositoryMock.Verify();
                rulesMock.Verify();
            }
        }
        [TestCase]
        public void GetIdTest()
        {
            var ids = from data in testData
                      select data.Id;
            charRepositoryMock.Setup(rep => rep.Ids).Returns(ids);

            var result = factory.Ids;
            Assert.AreEqual(result, ids);
        }
    }
}
