using GameEngine.Graphics.Views;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Views
{
    [TestFixture]
    public class DefaultTableSelectionModelTest
    {
        DefaultTableSelectionModel model;

        [SetUp]
        public void Setup()
        {
            model = new DefaultTableSelectionModel();
        }

        public static List<TestCaseData> ValidIndices = new List<TestCaseData> 
        { 
            new TestCaseData(0, 0),
            new TestCaseData(10, 10),
            new TestCaseData(10, 0)
        };

        [TestCaseSource("ValidIndices")]
        public void DefaultValueTest(int row, int column)
        {
            Assert.IsFalse(model.IsSelected(row, column));
        }

        [TestCaseSource("ValidIndices")]
        public void SimpleSelectUnselectTest(int row, int column)
        {
            Assert.IsFalse(model.IsSelected(row, column));

            model.SelectIndex(row, column);

            Assert.IsTrue(model.IsSelected(row, column));

            model.UnselectIndex(row, column);

            Assert.IsFalse(model.IsSelected(row, column));
        }

        public static List<TestCaseData> MultipleIndices = new List<TestCaseData>
        {
            new TestCaseData(0, 0, 10, 10),
            new TestCaseData(0, 1, 1, 0),
        };

        [TestCaseSource("MultipleIndices")]
        public void SingleSelectionTest(int oldRow, int oldColumn, int newRow, int newColumn)
        {
            model.SelectIndex(oldRow, oldColumn);

            Assert.IsTrue(model.IsSelected(oldRow, oldColumn));
            Assert.IsFalse(model.IsSelected(newRow, newColumn));

            model.SelectIndex(newRow, newColumn);

            Assert.IsTrue(model.IsSelected(newRow, newColumn));
            Assert.IsFalse(model.IsSelected(oldRow, oldColumn));
        }

        public static List<TestCaseData> InvalidIndices = new List<TestCaseData>
        {
            new TestCaseData(-1, 0),
            new TestCaseData(0, -1),
            new TestCaseData(-1, -1),

            new TestCaseData(-10, 0),
            new TestCaseData(0, -10),
            new TestCaseData(-10, -10)
        };

        [TestCaseSource("InvalidIndices")]
        public void InvalidIndicesTest(int row, int column)
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), delegate { model.SelectIndex(row, column); });
            Assert.Throws(typeof(ArgumentOutOfRangeException), delegate { model.UnselectIndex(row, column); });
            Assert.Throws(typeof(ArgumentOutOfRangeException), delegate { model.IsSelected(row, column); });
        }
    }
}
