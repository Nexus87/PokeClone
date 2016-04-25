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


        public static List<TestCaseData> ValidIndices = new List<TestCaseData> 
        { 
            new TestCaseData(0, 10),
            new TestCaseData(10, 10),
            new TestCaseData(10, 0)
        };

        [TestCase]
        public void IsSelected_DefaultSetup_IsTrue()
        {
            var model = new DefaultTableSelectionModel();
            Assert.IsTrue(model.IsSelected(0, 0));
        }

        [TestCaseSource("ValidIndices")]
        public void SelectionChanged_SelectIndex_IsCalledTwice(int row, int column)
        {
            var model = new DefaultTableSelectionModel();
            var args = new List<SelectionChangedEventArgs>();
            model.SelectionChanged += (obj, arg) => args.Add(arg);

            model.SelectIndex(row, column);

            Assert.AreEqual(2, args.Count);
        }

        [TestCaseSource("ValidIndices")]
        public void SelectionChanged_SelectIndex_DefaultIndexIsUnselected(int row, int column)
        {
            var model = new DefaultTableSelectionModel();
            var args = new List<SelectionChangedEventArgs>();
            var expectedEvent = new SelectionChangedEventArgs(0, 0, false);
            model.SelectionChanged += (obj, arg) => args.Add(arg);

            model.SelectIndex(row, column);

            AssertContainsEvent(expectedEvent, args);
        }

        [TestCaseSource("ValidIndices")]
        public void SelectionChanged_SelectIndex_NewIndexIsSelected(int row, int column)
        {
            var model = new DefaultTableSelectionModel();
            var args = new List<SelectionChangedEventArgs>();
            var expectedEvent = new SelectionChangedEventArgs(row, column, true);
            model.SelectionChanged += (obj, arg) => args.Add(arg);

            model.SelectIndex(row, column);

            AssertContainsEvent(expectedEvent, args);
        }

        private void AssertContainsEvent(SelectionChangedEventArgs expectedEvent, List<SelectionChangedEventArgs> args)
        {
            var result = args.Any(v => v.Column == expectedEvent.Column && v.Row == expectedEvent.Row && v.IsSelected == expectedEvent.IsSelected);
            Assert.IsTrue(result);
        }

        [TestCase(0, 0)]
        public void SelectionChanged_UnselectIndex_ArgumentIsAsExpected(int row, int column)
        {
            var model = new DefaultTableSelectionModel();
            var args = new List<SelectionChangedEventArgs>();
            var expectedArgument = new SelectionChangedEventArgs(row, column, false);
            model.SelectionChanged += (obj, arg) => args.Add(arg);

            bool success = model.UnselectIndex(row, column);

            AssertContainsEvent(expectedArgument, args);
        }

        [TestCaseSource("ValidIndices")]
        public void SelectionChanged_SelectSameIndexTwice_NoEventRaised(int row, int column)
        {
            var model = new DefaultTableSelectionModel();
            model.SelectIndex(row, column);
            bool wasSelected = false;
            model.SelectionChanged += (obj, arg) => wasSelected = true;

            model.SelectIndex(row, column);

            Assert.IsFalse(wasSelected);
        }

        [TestCase(1, 1)]
        public void SelectionChanged_UnselectOnNotSelectedIndex_NoEventRaised(int row, int column)
        {
            var model = new DefaultTableSelectionModel();
            bool wasSelected = false;
            model.SelectionChanged += (obj, arg) => wasSelected = true;

            model.UnselectIndex(row, column);

            Assert.IsFalse(wasSelected);
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
        public void SelectIndex_InvalidIndexes_ThrowsException(int row, int column)
        {
            var model = new DefaultTableSelectionModel();
            Assert.Throws(typeof(ArgumentOutOfRangeException), delegate { model.SelectIndex(row, column); });
        }
        [TestCaseSource("InvalidIndices")]
        public void UnselectIndex_InvalidIndexes_ThrowsException(int row, int column)
        {
            var model = new DefaultTableSelectionModel();
            Assert.Throws(typeof(ArgumentOutOfRangeException), delegate { model.UnselectIndex(row, column); });
        }
        [TestCaseSource("InvalidIndices")]
        public void IsSelected_InvalidIndexes_ThrowsException(int row, int column)
        {
            var model = new DefaultTableSelectionModel();
            Assert.Throws(typeof(ArgumentOutOfRangeException), delegate { model.IsSelected(row, column); });
        }
    }
}
