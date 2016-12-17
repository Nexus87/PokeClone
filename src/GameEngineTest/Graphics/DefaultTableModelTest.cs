using GameEngine.GUI.Graphics.TableView;
using NUnit.Framework;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class DefaultTableModelTest : IItemModelTest
    {
        private DefaultTableModel<TestType> model;
        [SetUp]
        public void Setup()
        {
            model = new DefaultTableModel<TestType>();
        }

        [TestCase(1, 0, 2, 1)]
        [TestCase(0, 1, 1, 2)]
        [TestCase(3, 4, 4, 5)]
        public void SetDataAt_IndexOutOfCurrentSize_ModelResizes(int row, int column, int expectedRows, int expectedColumns)
        {
            var t1 = new TestType { testString = "T1" };

            Assert.AreEqual(0, model.Rows);
            Assert.AreEqual(0, model.Columns);

            model.SetDataAt(t1, row, column);

            Assert.AreEqual(expectedRows, model.Rows);
            Assert.AreEqual(expectedColumns, model.Columns);

        }

        [TestCase(3, 4, 0, 1)]
        public void GetData_ResizeModel_DefaultIsNull(int row, int column, int testRow, int testColumn)
        {
            var t1 = new TestType { testString = "T1" };

            Assert.AreEqual(0, model.Rows);
            Assert.AreEqual(0, model.Columns);

            model.SetDataAt(t1, row, column);

            Assert.Null(model.DataAt(testRow, testColumn));
        }

        [TestCase]
        public void TableResizeEvent_SetDataOutOfCurrentSize_EventIsRaised()
        {
            var testModel = GetModel();
            TableResizeEventArgs eventArgs = null;
            var t = new TestType { testString = "Test" };
            int row = 1;
            int column = 1;

            testModel.SizeChanged += (obj, args) => { eventArgs = args; };

            testModel.SetDataAt(t, row, column);

            Assert.NotNull(eventArgs);
            Assert.AreEqual(row + 1, eventArgs.Rows);
            Assert.AreEqual(column + 1, eventArgs.Columns);
            
        }
        protected override ITableModel<TestType> GetModel()
        {
            return new DefaultTableModel<TestType>();
        }
    }
}
