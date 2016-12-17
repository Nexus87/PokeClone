using GameEngineTest.TestUtils;
using NUnit.Framework;
using System.Collections.Generic;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.TableView;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class TableSingleSelectionModelTest : ITableRendererTest
    {
        protected class TestTableRenderer : DefaultTableRenderer<TestType>
        {
            public TestTableRenderer() : base(null) { }
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

        [TestCaseSource(nameof(TestData))]
        public void GetComponent_WithSpecificData_StringIsGivenToComponent(TestType data, string expectedString)
        {
            var renderer = CreateTestRenderer();
            var component = renderer.GetComponent(0, 0, data);

            Assert.IsInstanceOf<ISelectableTextComponent>(component);

            Assert.AreEqual(expectedString, ((ISelectableTextComponent)component).Text);
        }

        public static List<TestCaseData> ValidIndices = new List<TestCaseData>
        {
            new TestCaseData(10, 10),
            new TestCaseData(0, 20),
            new TestCaseData(20, 0)
        };

        [TestCaseSource(nameof(ValidIndices))]
        public void GetComponent_SomeIndex_NeverNull(int row, int column)
        {
            var renderer = CreateTestRenderer();
            var data = new TestType("test");

            var component = renderer.GetComponent(row, column, data);

            Assert.NotNull(component);
        }

        private static DefaultTableRenderer<TestType> CreateTestRenderer()
        {
            return new TestTableRenderer();
        }

        protected override ITableRenderer<TestType> CreateRenderer()
        {
            return CreateTestRenderer();
        }
    }
}
