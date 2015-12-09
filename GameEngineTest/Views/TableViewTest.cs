using GameEngine.Graphics.Views;
using GameEngineTest.Util;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Views
{
    [TestFixture]
    public class TableViewTest : IGraphicComponentTest
    {
        TableView<TestType> table;
        Mock<IItemModel<TestType>> modelMock;
        [SetUp]
        public void Setup()
        {
            modelMock = new Mock<IItemModel<TestType>>();
            modelMock.Setup(o => o.Columns).Returns(2);
            modelMock.Setup(o => o.Rows).Returns(2);
            modelMock.Setup(o => o.DataStringAt(It.IsAny<int>(), It.IsAny<int>())).Returns<int, int>( (a, b) => "Data " + a + " " + b);
            
            table = new TableView<TestType>(modelMock.Object);
            table.Setup(contentMock.Object);
            testObj = table;
        }

        [TestCase]
        public void NoDataTest()
        {
            SpriteBatchMock spriteBatch = new SpriteBatchMock();
            modelMock = new Mock<IItemModel<TestType>>();
            modelMock.Setup(o => o.Columns).Returns(2);
            modelMock.Setup(o => o.Rows).Returns(2);

            table = new TableView<TestType>(modelMock.Object);
            table.X = 50.0f;
            table.Y = 50.0f;
            table.Width = 200.0f;
            table.Height = 200.0f;
            table.Setup(contentMock.Object);

            table.Draw(spriteBatch);

            Assert.AreEqual(0, spriteBatch.Objects.Count);
        }

        public static List<TestCaseData> ModelCoordinates = new List<TestCaseData>{
            new TestCaseData(0, 0),
            new TestCaseData(0, 1),
            new TestCaseData(1, 0),
            new TestCaseData(1, 1),
        };

        [TestCaseSource("ModelCoordinates")]
        public void PartialDataTest(int row, int column)
        {
            SpriteBatchMock spriteBatch = new SpriteBatchMock();
            modelMock = new Mock<IItemModel<TestType>>();
            modelMock.Setup(o => o.Columns).Returns(2);
            modelMock.Setup(o => o.Rows).Returns(2);
            modelMock.Setup(o => o.DataStringAt(It.IsAny<int>(), It.IsAny<int>())).Returns<int, int>((a, b) => a == row && b == column ? "Data" : null);

            table = new TableView<TestType>(modelMock.Object);
            table.X = 0.0f;
            table.Y = 0.0f;
            table.Width = 200.0f;
            table.Height = 200.0f;
            table.Setup(contentMock.Object);

            table.Draw(spriteBatch);

            Assert.AreEqual(1, spriteBatch.Objects.Count);
            Assert.AreEqual(row * 100, spriteBatch.Objects[0].Position.Y);
            Assert.AreEqual(column * 100, spriteBatch.Objects[0].Position.X);
            Assert.AreEqual(100, spriteBatch.Objects[0].Size.Y);
            Assert.AreEqual(100, spriteBatch.Objects[0].Size.X);
        }

        [TestCaseSource("ModelCoordinates")]
        public void SelectCellTest(int a, int b)
        {
            Assert.AreEqual(0, table.SelectedColumn);
            Assert.AreEqual(0, table.SelectedRow);

            table.SelectItem(a, b);
            Assert.AreEqual(b, table.SelectedColumn);
            Assert.AreEqual(a, table.SelectedRow);
        }

        [TestCaseSource("ModelCoordinates")]
        public void SelectNoDataCell(int row, int column)
        {
            modelMock = new Mock<IItemModel<TestType>>();
            modelMock.Setup(o => o.Columns).Returns(2);
            modelMock.Setup(o => o.Rows).Returns(2);
            modelMock.Setup(o => o.DataStringAt(It.IsAny<int>(), It.IsAny<int>())).Returns<int, int>((a, b) => a == row && b == column ? "Data" : null);

            table = new TableView<TestType>(modelMock.Object);

            Assert.AreEqual(row, table.SelectedRow);
            Assert.AreEqual(column, table.SelectedColumn);

            int selectRow = row == 0 ? 1 : 0;
            int selectColumn = column == 0 ? 1 : 0;

            table.SelectItem(selectRow, selectColumn);

            Assert.AreEqual(row, table.SelectedRow);
            Assert.AreEqual(column, table.SelectedColumn);
        }

        [TestCase]
        public void SelectInvalidIndex()
        {
            int selectRow = table.Rows;
            int selectColumn = table.Columns;

            Assert.AreEqual(0, table.SelectedColumn);
            Assert.AreEqual(0, table.SelectedRow);

            table.SelectItem(selectRow, selectColumn);
            Assert.AreEqual(0, table.SelectedColumn);
            Assert.AreEqual(0, table.SelectedRow);
        }

        [TestCase]
        public void OnModelResizeTest()
        {
            SpriteBatchMock spriteBatch = new SpriteBatchMock();
            var data = new TestType{testString = "Data"};
            int insertColumn = 2;
            int insertRow = 2;
            modelMock = new Mock<IItemModel<TestType>>();
            modelMock.Setup(o => o.Columns).Returns(2);
            modelMock.Setup(o => o.Rows).Returns(2);
            modelMock.Setup(o => o.DataStringAt(It.IsAny<int>(), It.IsAny<int>())).Returns<int, int>((a, b) => a == insertRow && b == insertColumn ? data.ToString() : null);

            table = new TableView<TestType>(modelMock.Object);
            table.X = 0.0f;
            table.Y = 0.0f;
            table.Width = 200.0f;
            table.Height = 200.0f;
            table.Setup(contentMock.Object);

            modelMock.Setup(o => o.Columns).Returns(3);
            modelMock.Setup(o => o.Rows).Returns(3);
            modelMock.Raise(o => o.SizeChanged += null, modelMock.Object, new SizeChangedArgs{ newColumns = 3, newRows = 3});
            modelMock.Raise(o => o.DataChanged += null, modelMock.Object, new DataChangedArgs<TestType> { column = 2, row = 2, newData = data });

            Assert.AreEqual(3, table.Rows);
            Assert.AreEqual(3, table.Columns);

            table.Draw(spriteBatch);

            Assert.AreEqual(1, spriteBatch.DrawnStrings);
            Assert.AreEqual("Data", spriteBatch.DrawnStrings.First.Value);
        }

        [TestCase]
        public void SelectionOnModelResizeTest()
        {
            var data = new TestType { testString = "Data" };
            table.SelectItem(1, 1);
            Assert.AreEqual(1, table.SelectedColumn);
            Assert.AreEqual(1, table.SelectedRow);

            modelMock.Setup(o => o.Columns).Returns(3);
            modelMock.Setup(o => o.Rows).Returns(3);
            modelMock.Raise(o => o.SizeChanged += null, modelMock.Object, new SizeChangedArgs { newColumns = 3, newRows = 3 });
            modelMock.Raise(o => o.DataChanged += null, modelMock.Object, new DataChangedArgs<TestType> { column = 2, row = 2, newData = data });

            Assert.AreEqual(3, table.Rows);
            Assert.AreEqual(3, table.Columns);

            Assert.AreEqual(1, table.SelectedColumn);
            Assert.AreEqual(1, table.SelectedRow);

            table.SelectItem(2, 2);
            Assert.AreEqual(2, table.SelectedColumn);
            Assert.AreEqual(2, table.SelectedRow);
        }

        [TestCase]
        public void ZeroSizeSelectionTest()
        {
            var data = new TestType { testString = "Data" };

            modelMock.Setup(o => o.Columns).Returns(0);
            modelMock.Setup(o => o.Rows).Returns(0);

            table = new TableView<TestType>(modelMock.Object);

            Assert.AreEqual(-1, table.SelectedColumn);
            Assert.AreEqual(-1, table.SelectedRow);

            table.SelectItem(0, 0);
            Assert.AreEqual(-1, table.SelectedColumn);
            Assert.AreEqual(-1, table.SelectedRow);

            modelMock.Setup(o => o.Columns).Returns(3);
            modelMock.Setup(o => o.Rows).Returns(3);
            modelMock.Raise(o => o.SizeChanged += null, modelMock.Object, new SizeChangedArgs { newColumns = 3, newRows = 3 });
            modelMock.Raise(o => o.DataChanged += null, modelMock.Object, new DataChangedArgs<TestType> { column = 2, row = 2, newData = data });

            Assert.AreEqual(0, table.SelectedColumn);
            Assert.AreEqual(0, table.SelectedRow);

            table.SelectItem(2, 2);
            Assert.AreEqual(2, table.SelectedColumn);
            Assert.AreEqual(2, table.SelectedRow);
        }
    }
}
