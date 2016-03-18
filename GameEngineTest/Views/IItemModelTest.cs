using GameEngine.Graphics.Views;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Views
{
    public class TestType
    {
        public TestType() { }
        public TestType(string testString)
        {
            this.testString = testString;
        }

        public string testString;
        public override string ToString()
        {
            return testString;
        }
    }

    public abstract class IItemModelTest
    {
        public ITableModel<TestType> testModel;

        [TestCase]
        public void SetGetTest()
        {
            var t = new TestType { testString = "Test" };
            Assert.True(testModel.SetData(t, 0, 0));

            Assert.AreEqual(t, testModel.DataAt(0, 0));

        }

        [TestCase]
        public void DefaultValuesTest()
        {
            int rows = testModel.Rows;
            int columns = testModel.Columns;
            var t = new TestType { testString = "Test" };

            Assert.AreEqual(default(TestType), testModel.DataAt(rows, columns));

            Assert.True(testModel.SetData(t, rows + 1, 0));
            Assert.AreEqual(default(TestType), testModel.DataAt(rows, 0));
            Assert.AreEqual(default(TestType), testModel.DataAt(rows, columns));
        }

        [TestCase]
        public void DataChangedEventTest()
        {
            DataChangedEventArgs<TestType> eventArgs = null;
            var t = new TestType { testString = "Test" };

            testModel.DataChanged += (obj, args) => { eventArgs = args; };

            Assert.True(testModel.SetData(t, 0, 0));
            Assert.NotNull(eventArgs);
            Assert.AreEqual(0, eventArgs.Row);
            Assert.AreEqual(0, eventArgs.Column);
            Assert.AreEqual(t, eventArgs.NewData);

            eventArgs = null;

            Assert.True(testModel.SetData(t, 0, 0));
            Assert.IsNull(eventArgs);

            Assert.True(testModel.SetData(null, 0, 0));
            Assert.NotNull(eventArgs);
            Assert.AreEqual(0, eventArgs.Row);
            Assert.AreEqual(0, eventArgs.Column);
            Assert.AreEqual(null, eventArgs.NewData);
        }

        [TestCase]
        public void SizeChangedEventTest()
        {
            TableResizeEventArgs eventArgs = null;
            var t = new TestType { testString = "Test" };
            int rows = testModel.Rows;
            int columns = testModel.Columns;

            testModel.SizeChanged += (obj, args) => { eventArgs = args; };

            Assert.True(testModel.SetData(t, rows, columns));
            if (rows != testModel.Rows)
            {
                Assert.NotNull(eventArgs);
                Assert.AreEqual(rows + 1, eventArgs.Rows);
            }
            if (columns != testModel.Columns)
            {
                Assert.NotNull(eventArgs);
                Assert.AreEqual(columns + 1, eventArgs.Columns);
            }

            eventArgs = null;
            rows = testModel.Rows;
            columns = testModel.Columns;

            Assert.True(testModel.SetData(t, rows, columns-1));
            if (rows != testModel.Rows)
            {
                Assert.NotNull(eventArgs);
                Assert.AreEqual(rows + 1, eventArgs.Rows);
            }
            if (columns != testModel.Columns)
            {
                Assert.NotNull(eventArgs);
                Assert.AreEqual(columns, eventArgs.Columns);
            }

            eventArgs = null;
            rows = testModel.Rows;
            columns = testModel.Columns;

            Assert.True(testModel.SetData(t, rows - 1, columns));
            if (rows != testModel.Rows)
            {
                Assert.NotNull(eventArgs);
                Assert.AreEqual(rows, eventArgs.Rows);
            }
            if (columns != testModel.Columns)
            {
                Assert.AreEqual(columns + 1, eventArgs.Columns);
                Assert.NotNull(eventArgs);
            }

            eventArgs = null;
            rows = Math.Max(1, testModel.Rows);
            columns = Math.Max(1, testModel.Columns);

            Assert.True(testModel.SetData(t, rows - 1, columns - 1));
            Assert.IsNull(eventArgs);
        }
    }
}
