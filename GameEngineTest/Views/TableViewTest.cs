﻿using GameEngine.Graphics.Views;
using GameEngine.Wrapper;
using GameEngineTest.Util;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;

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

        private Mock<IItemModel<TestType>> modelMock;
        private InternalTableView<TestType, SpriteFontMock> table;

        [TestCase]
        public void NoDataTest()
        {
            SpriteBatchMock spriteBatch = new SpriteBatchMock();
            modelMock = new Mock<IItemModel<TestType>>();
            modelMock.Setup(o => o.Columns).Returns(2);
            modelMock.Setup(o => o.Rows).Returns(2);

            table = new InternalTableView<TestType, SpriteFontMock>(modelMock.Object);
            table.X = 50.0f;
            table.Y = 50.0f;
            table.Width = 200.0f;
            table.Height = 200.0f;
            table.Setup(contentMock.Object);

            table.Draw(spriteBatch);

            Assert.AreEqual(0, spriteBatch.Objects.Count);
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
            modelMock.Setup(o => o.DataStringAt(It.IsAny<int>(), It.IsAny<int>())).Returns<int, int>((a, b) => a == insertRow && b == insertColumn ? data.ToString() : null);

            table = new InternalTableView<TestType, SpriteFontMock>(modelMock.Object);
            table.X = 0.0f;
            table.Y = 0.0f;
            table.Width = 180.0f;
            table.Height = 180.0f;
            table.Setup(contentMock.Object);

            modelMock.Setup(o => o.Columns).Returns(3);
            modelMock.Setup(o => o.Rows).Returns(3);
            modelMock.Raise(o => o.SizeChanged += null, modelMock.Object, new SizeChangedArgs { newColumns = 3, newRows = 3 });
            modelMock.Raise(o => o.DataChanged += null, modelMock.Object, new DataChangedArgs<TestType> { column = 2, row = 2, newData = data });

            Assert.AreEqual(3, table.Rows);
            Assert.AreEqual(3, table.Columns);

            table.Draw(spriteBatch);

            foreach (var obj in spriteBatch.Objects)
                obj.IsInConstraints(2* 60.0f, 2 * 60.0f, 60.0f, 60.0f);
        }

        [TestCaseSource("ModelCoordinates")]
        public void PartialDataTest(int row, int column)
        {
            SpriteBatchMock spriteBatch = new SpriteBatchMock();
            modelMock = new Mock<IItemModel<TestType>>();
            modelMock.Setup(o => o.Columns).Returns(2);
            modelMock.Setup(o => o.Rows).Returns(2);
            modelMock.Setup(o => o.DataStringAt(It.IsAny<int>(), It.IsAny<int>())).Returns<int, int>((a, b) => a == row && b == column ? "Data" : null);

            table = new InternalTableView<TestType, SpriteFontMock>(modelMock.Object);
            table.X = 0.0f;
            table.Y = 0.0f;
            table.Width = 200.0f;
            table.Height = 200.0f;
            table.Setup(contentMock.Object);

            table.Draw(spriteBatch);

            foreach (var obj in spriteBatch.Objects)
                obj.IsInConstraints(column * 100.0f, row * 100.0f, 100.0f, 100.0f);
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
            modelMock.Raise(o => o.SizeChanged += null, modelMock.Object, new SizeChangedArgs { newColumns = 3, newRows = 3 });
            modelMock.Raise(o => o.DataChanged += null, modelMock.Object, new DataChangedArgs<TestType> { column = 2, row = 2, newData = data });

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
            modelMock.Setup(o => o.DataStringAt(It.IsAny<int>(), It.IsAny<int>())).Returns<int, int>((a, b) => a == row && b == column ? "Data" : null);

            table = new InternalTableView<TestType, SpriteFontMock>(modelMock.Object);

            TestTableCellSelection();

            int selectRow = row == 0 ? 1 : 0;
            int selectColumn = column == 0 ? 1 : 0;

            Assert.IsFalse(table.SetCellSelection(selectRow, selectColumn, true));
            Assert.IsFalse(table.SetCellSelection(selectRow, selectColumn, false));

            TestTableCellSelection();
        }

        [SetUp]
        public void Setup()
        {
            contentMock.SetupLoad();
            modelMock = new Mock<IItemModel<TestType>>();
            modelMock.Setup(o => o.Columns).Returns(2);
            modelMock.Setup(o => o.Rows).Returns(2);
            modelMock.Setup(o => o.DataStringAt(It.IsAny<int>(), It.IsAny<int>())).Returns<int, int>((a, b) => "Data " + a + " " + b);

            table = new InternalTableView<TestType, SpriteFontMock>(modelMock.Object);
            table.Setup(contentMock.Object);
            testObj = table;
        }

        [TestCase]
        public void ZeroSizeSelectionTest()
        {
            var data = new TestType { testString = "Data" };

            modelMock.Setup(o => o.Columns).Returns(0);
            modelMock.Setup(o => o.Rows).Returns(0);

            table = new InternalTableView<TestType, SpriteFontMock>(modelMock.Object);

            Assert.IsFalse(table.SetCellSelection(0, 0, true));
        }

        private void TestTableCellSelection()
        {
            for (int i = 0; i < table.Rows; i++)
                for (int j = 0; j < table.Columns; j++)
                    Assert.IsFalse(table.IsCellSelected(i, j));
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