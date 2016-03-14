using GameEngine.Graphics.Views;
using GameEngine.Graphics;
using GameEngine.Wrapper;
using GameEngineTest.Util;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GameEngineTest.Views
{
    [TestFixture]
    public class TableViewTest : IGraphicComponentTest
    {
        public static List<TestCaseData> ModelCoordinates = new List<TestCaseData>{
            new TestCaseData(0, 0),
            new TestCaseData(0, 1),
            new TestCaseData(1, 0),
            new TestCaseData(1, 1),
        };


        private TableRendererMock<TestType> renderer;
        private Mock<IItemModel<TestType>> modelMock;
        private TableView<TestType> table;


        [TestCase]
        public void NoDataTest()
        {
            SpriteBatchMock spriteBatch = new SpriteBatchMock();

            modelMock = new Mock<IItemModel<TestType>>();
            modelMock.Setup(o => o.Columns).Returns(2);
            modelMock.Setup(o => o.Rows).Returns(2);

            table = new TableView<TestType>(8, 8, modelMock.Object, renderer, gameMock.Object);
            table.SetCoordinates(50, 50, 200, 200);

            table.Setup(contentMock.Object);

            table.Draw(spriteBatch);

            Assert.AreEqual(4, spriteBatch.Objects.Count);
            foreach (var i in renderer.entries)
                Assert.AreEqual(null, i);
        }

        public static List<TestCaseData> ChangeModelTestData = new List<TestCaseData>
        {
            new TestCaseData(4, 4, 8, 8),
            new TestCaseData(0, 0, 4, 4),
            new TestCaseData(5, 5, 2, 2),
            new TestCaseData(6, 6, 0, 0)
        };

        [TestCaseSource("ChangeModelTestData")]
        public void ChangeModelTest(int oldRows, int oldColumns, int newRows, int newColumns)
        {
            var spriteBatch = new SpriteBatchMock();
            var oldModel = new Mock<IItemModel<TestType>>();
            var newModel = new Mock<IItemModel<TestType>>();
            
            bool resizeCalled = false;

            var testDataN = new TestType("n");
            var testDataM = new TestType("m");

            oldModel.SetupModelMock(oldRows, oldColumns, testDataN);
            newModel.SetupModelMock(newRows, newColumns, testDataM);

            var newTable = new TableView<TestType>(4, 4, oldModel.Object, renderer, gameMock.Object);
            newTable.OnTableResize += (a, b) => { resizeCalled = true; };;

            newTable.Draw(spriteBatch);
            renderer.Reset();

            newTable.Model = newModel.Object;

            Assert.AreEqual(newRows, newTable.Rows);
            Assert.AreEqual(newColumns, newTable.Columns);
            Assert.True(resizeCalled);

            table.Draw(spriteBatch);

            Assert.AreEqual(newColumns * newRows, (from TestType s in renderer.entries where  s == testDataM select s).Count());
        }

        [TestCase]
        public void OnModelResizeTest()
        {
            SpriteBatchMock spriteBatch = new SpriteBatchMock();
            var data = new TestType { testString = "Data" };
            int insertColumn = 2;
            int insertRow = 2;
            modelMock = new Mock<IItemModel<TestType>>();
            modelMock.Setup(o => o.Columns).Returns(2);
            modelMock.Setup(o => o.Rows).Returns(2);
            modelMock.Setup(o => o.DataAt(It.IsAny<int>(), It.IsAny<int>())).Returns<int, int>((a, b) => a == insertRow && b == insertColumn ? data : null);

            table = new TableView<TestType>(4, 4, modelMock.Object, renderer, gameMock.Object);
            table.XPosition = 0.0f;
            table.YPosition = 0.0f;
            table.Width = 180.0f;
            table.Height = 180.0f;
            table.Setup(contentMock.Object);

            modelMock.Setup(o => o.Columns).Returns(3);
            modelMock.Setup(o => o.Rows).Returns(3);
            modelMock.Raise(o => o.SizeChanged += null, modelMock.Object, new SizeChangedEventArgs(newColumns: 3, newRows: 3));
            modelMock.Raise(o => o.DataChanged += null, modelMock.Object, new DataChangedEventArgs<TestType>(column: 2, row: 2, newData: data));

            Assert.AreEqual(3, table.Rows);
            Assert.AreEqual(3, table.Columns);

            table.Draw(spriteBatch);

            foreach (var obj in spriteBatch.Objects)
                obj.IsInConstraints(0.0f, 0.0f, 3*60.0f, 3*60.0f);
        }

        public static List<TestCaseData> TableSizes = new List<TestCaseData>{
            new TestCaseData(5, 5, true),
            new TestCaseData(1, 1, true),
            new TestCaseData(2, 2, false),
            new TestCaseData(2, 5, true),
            new TestCaseData(5, 2, true),
        };

        [TestCaseSource("TableSizes")]
        public void ResizeEventTest(int row, int column, bool result)
        {
            bool eventCalled = false;
            table.OnTableResize += (a, b) => { eventCalled = true; };

            modelMock.Setup(o => o.Columns).Returns(column);
            modelMock.Setup(o => o.Rows).Returns(row);

            Assert.False(eventCalled);
            modelMock.Raise(o => o.SizeChanged += null, modelMock.Object, new SizeChangedEventArgs(newColumns: column, newRows: row));
            Assert.AreEqual(result, eventCalled);
        }

        [TestCaseSource("ModelCoordinates")]
        public void PartialDataTest(int row, int column)
        {
            SpriteBatchMock spriteBatch = new SpriteBatchMock();
            modelMock = new Mock<IItemModel<TestType>>();
            modelMock.Setup(o => o.Columns).Returns(2);
            modelMock.Setup(o => o.Rows).Returns(2);
            modelMock.Setup(o => o.DataAt(It.IsAny<int>(), It.IsAny<int>())).Returns<int, int>((a, b) => a == row && b == column ? new TestType("Data") : null);

            table = new TableView<TestType>(4, 4, modelMock.Object, renderer, gameMock.Object);
            table.XPosition = 0.0f;
            table.YPosition = 0.0f;
            table.Width = 200.0f;
            table.Height = 200.0f;
            table.Setup(contentMock.Object);

            table.Draw(spriteBatch);

            foreach (var obj in spriteBatch.Objects)
                obj.IsInConstraints(0, 0, 200.0f, 200.0f);
        }

        [TestCaseSource("ModelCoordinates")]
        public void SelectCellTest(int a, int b)
        {
            TestTableCellSelection();

            Assert.IsTrue(table.SetCellSelection(a, b, true));
            for (int i = 0; i < table.Rows; i++)
            {
                for (int j = 0; j < table.Columns; j++)
                {
                    Assert.AreEqual(i == a && j == b, table.IsCellSelected(i, j));
                }
            }

            Assert.IsTrue(table.SetCellSelection(a, b, false));
            TestTableCellSelection();
        }

        [TestCase]
        public void SelectInvalidIndex()
        {
            int selectRow = table.Rows;
            int selectColumn = table.Columns;

            TestTableCellSelection();

            Assert.IsFalse(table.SetCellSelection(selectRow, selectColumn, true));
            Assert.IsFalse(table.SetCellSelection(selectRow, selectColumn, false));
        }

        [TestCase]
        public void SelectionOnModelResizeTest()
        {
            var data = new TestType { testString = "Data" };
            Assert.IsTrue(table.SetCellSelection(1, 1, true));
            Assert.IsTrue(table.IsCellSelected(1, 1));

            modelMock.Setup(o => o.Columns).Returns(3);
            modelMock.Setup(o => o.Rows).Returns(3);
            modelMock.Raise(o => o.SizeChanged += null, modelMock.Object, new SizeChangedEventArgs(newColumns: 3, newRows: 3));
            modelMock.Raise(o => o.DataChanged += null, modelMock.Object, new DataChangedEventArgs<TestType>(column: 2, row: 2, newData: data));

            Assert.AreEqual(3, table.Rows);
            Assert.AreEqual(3, table.Columns);

            Assert.IsTrue(table.IsCellSelected(1, 1));

            Assert.IsTrue(table.SetCellSelection(2, 2, true));
            Assert.IsTrue(table.IsCellSelected(2, 2));
        }

        [TestCaseSource("ModelCoordinates")]
        public void SelectNoDataCell(int row, int column)
        {
            modelMock = new Mock<IItemModel<TestType>>();
            modelMock.Setup(o => o.Columns).Returns(2);
            modelMock.Setup(o => o.Rows).Returns(2);
            modelMock.Setup(o => o.DataAt(It.IsAny<int>(), It.IsAny<int>())).Returns<int, int>((a, b) => a == row && b == column ? new TestType("Data") : null);

            table = new TableView<TestType>(4, 4, modelMock.Object, renderer, gameMock.Object);

            TestTableCellSelection();

            int selectRow = row == 0 ? 1 : 0;
            int selectColumn = column == 0 ? 1 : 0;

            Assert.IsTrue(table.SetCellSelection(selectRow, selectColumn, true));
            Assert.IsTrue(table.SetCellSelection(selectRow, selectColumn, false));

            TestTableCellSelection();
        }

        [SetUp]
        public void Setup()
        {
            contentMock.SetupLoad();
            
            modelMock = new Mock<IItemModel<TestType>>();
            
            modelMock.Setup(o => o.Columns).Returns(2);
            modelMock.Setup(o => o.Rows).Returns(2);
            modelMock.Setup(o => o.DataAt(It.IsAny<int>(), It.IsAny<int>())).Returns<int, int>((a, b) => new TestType("Data " + a + " " + b));

            gameMock.Object.Content = contentMock.Object;
            renderer = new TableRendererMock<TestType>();

            table = new TableView<TestType>(4, 4, modelMock.Object, renderer, gameMock.Object);
            table.Setup(contentMock.Object);
            testObj = table;
        }

        [TestCase]
        public void ZeroSizeSelectionTest()
        {
            var data = new TestType { testString = "Data" };

            modelMock.Setup(o => o.Columns).Returns(0);
            modelMock.Setup(o => o.Rows).Returns(0);

            table = new TableView<TestType>(4, 4, modelMock.Object, renderer, gameMock.Object);

            Assert.IsFalse(table.SetCellSelection(0, 0, true));
        }

        private void TestTableCellSelection()
        {
            for (int i = 0; i < table.Rows; i++)
                for (int j = 0; j < table.Columns; j++)
                    Assert.IsFalse(table.IsCellSelected(i, j));
        }

        [TestCase]
        public void ViewportTest()
        {
            SpriteBatchMock spriteBatch = new SpriteBatchMock();
            modelMock = new Mock<IItemModel<TestType>>();
            modelMock.Setup(o => o.Columns).Returns(20);
            modelMock.Setup(o => o.Rows).Returns(20);
            modelMock.Setup(o => o.DataAt(It.IsAny<int>(), It.IsAny<int>())).Returns<int, int>((a, b) => new TestType(a + " " + b));

            table = new TableView<TestType>(4, 4, modelMock.Object, renderer, gameMock.Object);
            table.SetCoordinates(0, 0, 2000, 2000);

            Assert.Less(table.Rows, 20);
            Assert.Less(table.Columns, 20);

            Assert.AreEqual(0, table.StartColumn);
            Assert.AreEqual(0, table.StartRow);

            table.StartRow = 1;
            table.StartColumn = 1;

            Assert.AreEqual(1, table.StartColumn);
            Assert.AreEqual(1, table.StartRow);

            table.Draw(spriteBatch);

            foreach (var s in spriteBatch.DrawnStrings)
            {
                var nums = s.Split(' ');
                Assert.AreEqual(2, nums.Length);
                int a = int.Parse(nums[0]);
                int b = int.Parse(nums[1]);

                Assert.GreaterOrEqual(a, 1);
                Assert.GreaterOrEqual(b, 1);

                Assert.Less(a, table.Rows + 1);
                Assert.Less(a, table.Columns + 1);
            }

            table.StartRow = 19;
            table.StartColumn = 19;

            spriteBatch.DrawnStrings.Clear();
            table.Draw(spriteBatch);

            foreach (var s in spriteBatch.DrawnStrings)
            {
                var nums = s.Split(' ');
                Assert.AreEqual(2, nums.Length);
                int a = int.Parse(nums[0]);
                int b = int.Parse(nums[1]);

                Assert.GreaterOrEqual(a, 20 - table.Rows);
                Assert.GreaterOrEqual(b, 20 - table.Columns);

                Assert.Less(a, 20);
                Assert.Less(a, 20);
            }
        }
    }

    internal class SpriteFontMock : ISpriteFont
    {
        public ISpriteFont spriteFont;
        public Mock<ISpriteFont> spriteMock = new Mock<ISpriteFont>();

        public SpriteFontMock()
        {
            spriteMock.SetupMeasureString();
            spriteFont = spriteMock.Object;
        }

        public System.Collections.ObjectModel.ReadOnlyCollection<char> Characters { get { return spriteFont.Characters; } }
        public char? DefaultCharacter { get { return spriteFont.DefaultCharacter; } set { spriteFont.DefaultCharacter = value; } }
        public int LineSpacing { get { return spriteFont.LineSpacing; } set { spriteFont.LineSpacing = value; } }
        public float Spacing { get { return spriteFont.Spacing; } set { spriteFont.Spacing = value; } }
        public Microsoft.Xna.Framework.Graphics.SpriteFont SpriteFont { get { return spriteFont.SpriteFont; } }

        public Microsoft.Xna.Framework.Graphics.Texture2D Texture
        {
            get { return spriteFont.Texture; }
        }

        public void Load(Microsoft.Xna.Framework.Content.ContentManager content, string fontName)
        {
            spriteFont.Load(content, fontName);
        }

        public Microsoft.Xna.Framework.Vector2 MeasureString(StringBuilder text)
        {
            return spriteFont.MeasureString(text);
        }

        public Microsoft.Xna.Framework.Vector2 MeasureString(string text)
        {
            return spriteFont.MeasureString(text);
        }
    }
}