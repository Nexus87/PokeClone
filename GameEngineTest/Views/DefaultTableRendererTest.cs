using GameEngine;
using GameEngine.Graphics.Views;
using GameEngine.Wrapper;
using GameEngineTest.Util;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Views
{
    [TestFixture]
    public class DefaultTableRendererTest : ITableRendererTest
    {
        private DefaultTableRenderer<TestType> renderer;

        [SetUp]
        public void Setup()
        {
            var fontMock = new Mock<ISpriteFont>();
            fontMock.SetupMeasureString();
            renderer = new DefaultTableRenderer<TestType>(new PokeEngine(), delegate { return fontMock.Object; });

            testRenderer = renderer;
        }

        public static List<TestCaseData> TestData = new List<TestCaseData>
        {
            new TestCaseData(null, ""),
            new TestCaseData(new TestType("testString"), "testString"),
            new TestCaseData(new TestType(""), "")
        };

        /// <summary>
        /// Test if the ISelectableGraphicComponent that the DefaultRenderer returns is
        /// of the type ItemBox
        /// </summary>
        [TestCase]
        public void ComponentTypeTest()
        {
            var data = new TestType("test");
            var component = renderer.GetComponent(0, 0, data, false);

            Assert.IsInstanceOf<ItemBox>(component);
        }

        [TestCaseSource("TestData")]
        public void DataTest(TestType data, string expectedString)
        {
            var component = renderer.GetComponent(0, 0, data, false);

            Assert.IsInstanceOf<ItemBox>(component);

            Assert.AreEqual(expectedString, ((ItemBox)component).Text);
        }

        public static List<TestCaseData> ValidIndices = new List<TestCaseData>
        {
            new TestCaseData(10, 10),
            new TestCaseData(0, 20),
            new TestCaseData(20, 0)
        };

        [TestCaseSource("ValidIndices")]
        public void GetComponentTest(int row, int column)
        {
            var data = new TestType("test");

            var component = renderer.GetComponent(row, column, data, false);

            Assert.NotNull(component);
        }
        
    }
}
