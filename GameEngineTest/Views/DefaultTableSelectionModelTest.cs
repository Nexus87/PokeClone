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
            new TestCaseData(0, 10),
            new TestCaseData(10, 10),
            new TestCaseData(10, 0)
        };

        [TestCase]
        public void DefaultValueTest()
        {
            Assert.IsTrue(model.IsSelected(0, 0));
        }

        [TestCaseSource("ValidIndices")]
        public void SimpleSelectEventTest(int row, int column)
        {
            var args = new List<SelectionChangedEventArgs>();
            model.SelectionChanged += (obj, arg) => args.Add(arg);

            bool success = model.SelectIndex(row, column);

            Assert.True(success);
            Assert.AreEqual(2, args.Count);

            var argument = (from a in args where a.IsSelected select a).FirstOrDefault();

            Assert.AreEqual(row, argument.Row);
            Assert.AreEqual(column, argument.Column);
            Assert.True(argument.IsSelected);
        }

        [TestCaseSource("ValidIndices")]
        public void SimpleUnselectEventTest(int row, int column)
        {
            var args = new List<SelectionChangedEventArgs>();
            model.SelectionChanged += (obj, arg) => args.Add(arg);

            bool success = model.UnselectIndex(row, column);
            Assert.False(success);
            Assert.AreEqual(0, args.Count);

            model.SelectIndex(row, column);
            args.Clear();
            success = false;

            success = model.UnselectIndex(row, column);

            Assert.True(success);
            Assert.AreEqual(1, args.Count);

            var argument = args[0];
            Assert.AreEqual(row, argument.Row);
            Assert.AreEqual(column, argument.Column);
            Assert.False(argument.IsSelected);
        }

        [TestCaseSource("ValidIndices")]
        public void SameCellSelectionEventTest(int row, int column)
        {
            model.SelectIndex(row, column);

            bool wasSelected = false;
            model.SelectionChanged += (obj, arg) => wasSelected = true;

            model.SelectIndex(row, column);

            Assert.IsFalse(wasSelected);
        }

        [TestCaseSource("MultipleIndices")]
        public void MultipleSelectionEventTest(int row, int column, int newRow, int newColumn)
        {
            model.SelectIndex(row, column);

            var args = new List<SelectionChangedEventArgs>();
            model.SelectionChanged += (obj, arg) => args.Add(arg);

            model.SelectIndex(newRow, newColumn);

            Assert.AreEqual(2, args.Count);

            // The cell (row, column) was unselected, (newRow, newColumn) was selected
            var unselected = (from ev in args where ev.IsSelected == false select ev).FirstOrDefault();
            var selected = (from ev in args where ev.IsSelected == true select ev).FirstOrDefault();

            Assert.NotNull(unselected);
            Assert.NotNull(selected);

            Assert.AreEqual(row, unselected.Row);
            Assert.AreEqual(column, unselected.Column);

            Assert.AreEqual(newRow, selected.Row);
            Assert.AreEqual(newColumn, selected.Column);
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
