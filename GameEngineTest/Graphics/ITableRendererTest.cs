using GameEngine.Graphics;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace GameEngineTest.Graphics
{
    public abstract class ITableRendererTest
    {
        TestType data = new TestType("test");
        protected abstract ITableRenderer<TestType> CreateRenderer();

        [TestCase]
        public void GetComponent_NullData_DoesNotThrow()
        {
            // Table renderer need to handle null as data
            var testRenderer = CreateRenderer();

            data = null;
            Assert.DoesNotThrow(delegate { testRenderer.GetComponent(0, 0, data, true); });
            Assert.DoesNotThrow(delegate { testRenderer.GetComponent(0, 0, data, false); });

        }
        [TestCase]
        public void GetComponent_NullData_ReturnsNotNull()
        {
            var testRenderer = CreateRenderer();

            data = null;
            Assert.NotNull(testRenderer.GetComponent(0, 0, data, true));
            Assert.NotNull(testRenderer.GetComponent(0, 0, data, false));
        }

        [TestCase]
        public void GetComponent_SelectionIsTrue_IsSelected()
        {
            var testRenderer = CreateRenderer();

            var component = testRenderer.GetComponent(0, 0, data, true);

            Assert.IsTrue(component.IsSelected);
        }

        [TestCase]
        public void GetComponent_SelectionIsFalse_IsNotSelected()
        {
            var testRenderer = CreateRenderer();

            var component = testRenderer.GetComponent(0, 0, data, false);

            Assert.IsFalse(component.IsSelected);
        }

        public static List<TestCaseData> InvalidIndexes = new List<TestCaseData>
        {
            new TestCaseData(-1, 0),
            new TestCaseData(0, -1),
            new TestCaseData(-1, -1),

            new TestCaseData(-10, 0),
            new TestCaseData(0, -10),
            new TestCaseData(-10, -10)
        };

        [TestCaseSource(typeof(ITableRendererTest), "InvalidIndexes")]
        public void GetComponent_CallWithInvalidIndexes_ThrowsException(int row, int column)
        {
            var testRenderer = CreateRenderer();

            Assert.Throws(typeof(ArgumentOutOfRangeException), delegate { testRenderer.GetComponent(row, column, data, true); });
            Assert.Throws(typeof(ArgumentOutOfRangeException), delegate { testRenderer.GetComponent(row, column, data, false); });
        }
    }
}
