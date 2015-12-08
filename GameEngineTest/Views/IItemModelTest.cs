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
        public string testString;
        public override string ToString()
        {
            return testString + "!";
        }
    }

    public abstract class IItemModelTest
    {
        public IItemModel<TestType> testModel;

        [TestCase]
        public void SetGetTest()
        {
            var t = new TestType { testString = "Test" };
            Assert.True(testModel.SetData(t, 0, 0));

            Assert.AreEqual(t, testModel.DataAt(0, 0));
            Assert.AreEqual(t.testString + "!", testModel.DataStringAt(0, 0));

        }

        [TestCase]
        public void DefaultValuesTest()
        {
            int rows = testModel.Rows;
            int columns = testModel.Columns;
            var t = new TestType { testString = "Test" };

            Assert.AreEqual(default(TestType), testModel.DataAt(rows, columns));
            Assert.AreEqual("", testModel.DataStringAt(rows, columns));

            Assert.True(testModel.SetData(t, rows + 1, 0));
            Assert.AreEqual(default(TestType), testModel.DataAt(rows, 0));
            Assert.AreEqual("", testModel.DataStringAt(rows, columns));
        }

        [TestCase]
        public void DataChangedEventTest()
        {
            DataChangedArgs<TestType> eventArgs = null;
            var t = new TestType { testString = "Test" };

            testModel.DataChanged += (obj, args) => { eventArgs = args; };

            Assert.True(testModel.SetData(t, 0, 0));
            Assert.NotNull(eventArgs);
            Assert.AreEqual(0, eventArgs.row);
            Assert.AreEqual(0, eventArgs.column);
            Assert.AreEqual(t, eventArgs.newData);

            eventArgs = null;

            Assert.True(testModel.SetData(t, 0, 0));
            Assert.IsNull(eventArgs);

            Assert.True(testModel.SetData(null, 0, 0));
            Assert.NotNull(eventArgs);
            Assert.AreEqual(0, eventArgs.row);
            Assert.AreEqual(0, eventArgs.column);
            Assert.AreEqual(null, eventArgs.newData);
        }

        [TestCase]
        public void SizeChangedEventTest()
        {
            SizeChangedArgs eventArgs = null;
            var t = new TestType { testString = "Test" };
            int rows = testModel.Rows;
            int columns = testModel.Columns;

            testModel.SizeChanged += (obj, args) => { eventArgs = args; };

            Assert.True(testModel.SetData(t, rows, columns));
            if (rows != testModel.Rows)
            {
                Assert.NotNull(eventArgs);
                Assert.AreEqual(rows + 1, eventArgs.newRows);
            }
            if (columns != testModel.Columns)
            {
                Assert.NotNull(eventArgs);
                Assert.AreEqual(columns + 1, eventArgs.newColumns);
            }

            eventArgs = null;
            rows = testModel.Rows;
            columns = testModel.Columns;

            Assert.True(testModel.SetData(t, rows, columns-1));
            if (rows != testModel.Rows)
            {
                Assert.NotNull(eventArgs);
                Assert.AreEqual(rows + 1, eventArgs.newRows);
            }
            if (columns != testModel.Columns)
            {
                Assert.NotNull(eventArgs);
                Assert.AreEqual(columns, eventArgs.newColumns);
            }

            eventArgs = null;
            rows = testModel.Rows;
            columns = testModel.Columns;

            Assert.True(testModel.SetData(t, rows - 1, columns));
            if (rows != testModel.Rows)
            {
                Assert.NotNull(eventArgs);
                Assert.AreEqual(rows, eventArgs.newRows);
            }
            if (columns != testModel.Columns)
            {
                Assert.AreEqual(columns + 1, eventArgs.newColumns);
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
