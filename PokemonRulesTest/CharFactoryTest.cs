using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using PokemonRules;

namespace PokemonRulesTest
{
    [TestFixture]
    class CharFactoryTest
    {
        static CharFactory.PrimitiveData testData = new CharFactory.PrimitiveData
        {
            atk = 10,
            def = 11,
            hp = 12,
            id = 13,
            init = 14,
            name = "Name",
            spatk = 15,
            spdef = 16,
            type1 = PokemonType.Bug,
            type2 = PokemonType.None
        };

        CharFactory _factory;
        [SetUp]
        public void init()
        {
            _factory = new CharFactory("./Test.txt");
        }
        [TestCase]
        public void simpleReadWriteTest()
        {

            CharFactory.PrimitiveData result = new CharFactory.PrimitiveData();
            Assert.DoesNotThrow(() => _factory.writeData(testData));
            Assert.DoesNotThrow(() => result = _factory.getData());

            Assert.AreEqual(result, testData);
        }

        [TestCase]
        public void getCharTest()
        {
            PokemonV1 result = null;
            Assert.DoesNotThrow(() => _factory.writeData(testData));
            Assert.DoesNotThrow(() => result = _factory.getChar(testData.name));

            Assert.NotNull(result);
            Assert.AreEqual(result.Attack, testData.atk);
            Assert.AreEqual(result.Defense, testData.def);
            Assert.AreEqual(result.SpAtk, testData.spatk);
            Assert.AreEqual(result.SpDef, testData.spdef);
            Assert.AreEqual(result.Speed, testData.init);
            Assert.AreEqual(result.Name, testData.name);
            Assert.AreEqual(result.Type1, testData.type1);
            Assert.AreEqual(result.Type2, testData.type2);
        }
    }
}
