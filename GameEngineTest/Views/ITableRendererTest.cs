using GameEngine.Graphics;
using GameEngine.Graphics.Views;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Views
{
    public abstract class ITableRendererTest
    {
        private TestType data = new TestType("test");

        protected abstract ITableRenderer<TestType> CreateRenderer();
        [TestCase]
        public void DefaultDataTest()
        {
            // Table renderer need to handle null as data
            var testRenderer = CreateRenderer();

            data = null;
            Assert.DoesNotThrow(delegate { testRenderer.GetComponent(0, 0, data, true); });
            Assert.DoesNotThrow(delegate { testRenderer.GetComponent(0, 0, data, false); });

            Assert.NotNull(testRenderer.GetComponent(0, 0, data, true));
            Assert.NotNull(testRenderer.GetComponent(0, 0, data, false));
        }

        [TestCase]
        public void IsSelectedTest()
        {
            var testRenderer = CreateRenderer();

            Assert.IsTrue(testRenderer.GetComponent(0, 0, data, true).IsSelected);
            Assert.IsFalse(testRenderer.GetComponent(0, 0, data, false).IsSelected);
        }

        public static List<TestCaseData> InvalidIndices = new List<TestCaseData>
        {
            new TestCaseData(-1, 0),
            new TestCaseData(0, -1),
            new TestCaseData(-1, -1),

            new TestCaseData(-10, 0),
            new TestCaseData(0, -10),
            new TestCaseData(-10, -10),
        };

        [TestCaseSource(typeof(ITableRendererTest), "InvalidIndices")]
        public void InvalidIndicesTest(int row, int column)
        {
            var testRenderer = CreateRenderer();

            Assert.Throws(typeof(ArgumentOutOfRangeException), delegate { testRenderer.GetComponent(row, column, data, true); });
            Assert.Throws(typeof(ArgumentOutOfRangeException), delegate { testRenderer.GetComponent(row, column, data, false); });
        }
    }
}
