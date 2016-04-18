using GameEngine.Graphics.Views;
using GameEngine.Graphics;
using GameEngine.Wrapper;
using GameEngine.Utils;
using GameEngineTest.Util;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System;
using Microsoft.Xna.Framework.Content;

namespace GameEngineTest.Views
{
    [TestFixture]
    public class TableViewTest : IGraphicComponentTest
    {
        

        [TestCase]
        public void RowsColumns_ResizeModelBeforeSetup_ReturnsNewSize()
        {
            var modelStub = new Mock<ITableModel<Object>>();
            var rendererStub = new TableRendererMock<Object>();
            var selectionModelStub = new Mock<ITableSelectionModel>();
            var table = CreateTable(modelStub, rendererStub, selectionModelStub);
            int rows = 10;
            int columns = 20;

            SetDimension(modelStub, rows, columns);
            modelStub.Raise(o => o.SizeChanged += null, new TableResizeEventArgs(rows, columns));

            Assert.AreEqual(rows, table.Rows);
            Assert.AreEqual(columns, table.Columns);
        }
        private TableView<Object> CreateTable(Mock<ITableModel<Object>> modelMock, TableRendererMock<Object> renderer, Mock<ITableSelectionModel> selectionModelMock)
        {
            var table = new TableView<Object>(modelMock.Object, renderer, selectionModelMock.Object, gameMock.Object);
            table.SetCoordinates(0, 0, 500, 500);
            table.Setup();
            return table;
        }

        private static void SetDimension(Mock<ITableModel<Object>> modelMock, int rows, int columns)
        {
            modelMock.Setup(o => o.Rows).Returns(rows);
            modelMock.Setup(o => o.Columns).Returns(columns);
        }

        protected override IGraphicComponent CreateComponent()
        {
            var modelMock = new Mock<ITableModel<Object>>();
            var selectionModelMock = new Mock<ITableSelectionModel>();
            var renderer = new TableRendererMock<Object>();
            var table = new TableView<Object>(modelMock.Object, renderer, selectionModelMock.Object, gameMock.Object);
            table.Setup();
            return table;
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

        public void Load(ContentManager content, string fontName)
        {
            spriteFont.LoadContent();
        }

        public Microsoft.Xna.Framework.Vector2 MeasureString(StringBuilder text)
        {
            return spriteFont.MeasureString(text);
        }

        public Microsoft.Xna.Framework.Vector2 MeasureString(string text)
        {
            return spriteFont.MeasureString(text);
        }


        public void LoadContent()
        {
        }
    }
}