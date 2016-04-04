using GameEngine;
using GameEngine.Graphics;
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
        protected class TestTableRenderer : DefaultTableRenderer<TestType>
        {
            public TestTableRenderer(PokeEngine game) : base(game) { }
            protected override ISelectableTextComponent CreateComponent()
            {
                return new TableComponentMock<TestType>();
            }
        }


        public static List<TestCaseData> TestData = new List<TestCaseData>
        {
            new TestCaseData(null, ""),
            new TestCaseData(new TestType("testString"), "testString"),
            new TestCaseData(new TestType(""), "")
        };

        [TestCaseSource("TestData")]
        public void DataTest(TestType data, string expectedString)
        {
            var renderer = CreateTestRenderer();
            var component = renderer.GetComponent(0, 0, data, false);

            Assert.IsInstanceOf<ISelectableTextComponent>(component);

            Assert.AreEqual(expectedString, ((ISelectableTextComponent)component).Text);
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
            var renderer = CreateTestRenderer();
            var data = new TestType("test");

            var component = renderer.GetComponent(row, column, data, false);

            Assert.NotNull(component);
        }

        private DefaultTableRenderer<TestType> CreateTestRenderer()
        {
            return new TestTableRenderer(new PokeEngine());
        }

        protected override ITableRenderer<TestType> CreateRenderer()
        {
            return CreateTestRenderer();
        }
    }
}
