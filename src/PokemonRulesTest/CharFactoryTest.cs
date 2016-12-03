using Base;
using Base.Data;
using Base.Factory;
using Base.Rules;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;

namespace PokemonRulesTest
{
    [TestFixture]
    internal class CharFactoryTest
    {
        private IPokemonRepository _charRepositoryMock;
        private PokemonFactory _factory;
        private IPokemonRules _rulesMock;
        private readonly List<PokemonData> _testData = new List<PokemonData>();
        private readonly List<Pokemon> _testChar = new List<Pokemon>();

        [SetUp]
        public void Setup()
        {
            _charRepositoryMock = A.Fake<IPokemonRepository>();
            _rulesMock = A.Fake<IPokemonRules>();

            _factory = new PokemonFactory(_charRepositoryMock, _rulesMock);

            var testStats = new Stats();
            _testData.Add(new PokemonData { Id = 0, BaseStats = testStats, Name = "Data1" });
            _testData.Add(new PokemonData { Id = 1, BaseStats = testStats, Name = "Data2" });
            _testData.Add(new PokemonData { Id = 2, BaseStats = testStats, Name = "Data3" });

            foreach(var data in _testData)
                _testChar.Add(new Pokemon(data, testStats));
        }

        [TestCase]
        public void GetCharTest()
        {
            for (var i = 0; i < _testData.Count; i++)
            {
                var retData = _testData[i];
                var retChar = _testChar[i];

                A.CallTo(() => _charRepositoryMock.GetPokemonData(retData.Id)).Returns(retData);
                A.CallTo(() => _rulesMock.FromPokemonData(retData)).Returns(retChar);

                var result = _factory.GetPokemon(retData.Id);

                Assert.AreEqual(result, retChar);
                A.CallTo(() => _charRepositoryMock.GetPokemonData(retData.Id)).MustHaveHappened(Repeated.AtLeast.Once);
                A.CallTo(() => _rulesMock.FromPokemonData(retData)).MustHaveHappened(Repeated.AtLeast.Once);
            }

        }

        [TestCase]
        public void GetCharLeveledTest()
        {
            for (var i = 0; i < _testData.Count; i++)
            {
                var retData = _testData[i];
                var retChar = _testChar[i];
                var retLevel = 0;
                const int testLevel = 15;

                A.CallTo(() => _charRepositoryMock.GetPokemonData(retData.Id)).Returns(retData);
                A.CallTo(() => _rulesMock.FromPokemonData(retData)).Returns(retChar);
                A.CallTo(() => _rulesMock.ToLevel(retChar, A<int>.Ignored)).Invokes( (Pokemon pkm, int lvl) => retLevel = lvl);

                var result = _factory.GetPokemon(retData.Id, testLevel);

                Assert.AreEqual(result, retChar);
                Assert.AreEqual(retLevel, testLevel);
                A.CallTo(() => _charRepositoryMock.GetPokemonData(retData.Id)).MustHaveHappened(Repeated.AtLeast.Once);
                A.CallTo(() => _rulesMock.FromPokemonData(retData)).MustHaveHappened(Repeated.AtLeast.Once);
                A.CallTo(() => _rulesMock.ToLevel(retChar, A<int>.Ignored)).MustHaveHappened(Repeated.AtLeast.Once);
            }
        }
        [TestCase]
        public void GetIdTest()
        {
            var ids = from data in _testData
                      select data.Id;
            A.CallTo(() => _charRepositoryMock.Ids).Returns(ids);

            var result = _factory.Ids;
            Assert.AreEqual(result, ids);
        }
    }
}
