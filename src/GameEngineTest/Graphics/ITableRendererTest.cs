using NUnit.Framework;
using System;
using System.Collections.Generic;
using GameEngine.GUI.Graphics.TableView;

namespace GameEngineTest.Graphics
{
    public abstract class ITableRendererTest
    {
        private TestType _data = new TestType("test");
        protected abstract ITableRenderer<TestType> CreateRenderer();

        [TestCase]
        public void GetComponent_NullData_DoesNotThrow()
        {
            // Table renderer need to handle null as data
            var testRenderer = CreateRenderer();

            _data = null;
            Assert.DoesNotThrow(delegate { testRenderer.GetComponent(0, 0, _data); });
            Assert.DoesNotThrow(delegate { testRenderer.GetComponent(0, 0, _data); });

        }
        [TestCase]
        public void GetComponent_NullData_ReturnsNotNull()
        {
            var testRenderer = CreateRenderer();

            _data = null;
            Assert.NotNull(testRenderer.GetComponent(0, 0, _data));
            Assert.NotNull(testRenderer.GetComponent(0, 0, _data));
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

        [TestCaseSource(typeof(ITableRendererTest), nameof(InvalidIndexes))]
        public void GetComponent_CallWithInvalidIndexes_ThrowsException(int row, int column)
        {
            var testRenderer = CreateRenderer();

            Assert.Throws(typeof(ArgumentOutOfRangeException), delegate { testRenderer.GetComponent(row, column, _data); });
            Assert.Throws(typeof(ArgumentOutOfRangeException), delegate { testRenderer.GetComponent(row, column, _data); });
        }
    }
}
