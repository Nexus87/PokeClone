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
        public class CellData
        {
            public int row;
            public int column;
            public int custom;

            public CellData(int row, int column, int custom = 0)
            {
                this.row = row;
                this.column = column;
                this.custom = custom;
            }
        }
        private TableView<CellData> CreateTable(Mock<ITableModel<CellData>> modelMock, TableRendererMock<CellData> renderer, Mock<ITableSelectionModel> selectionModelMock)
        {
            var table = new TableView<CellData>(modelMock.Object, renderer, selectionModelMock.Object, gameMock.Object);
            table.SetCoordinates(0, 0, 500, 500);
            table.Setup();
            return table;
        }

        private static void SetDimension(Mock<ITableModel<CellData>> modelMock, int rows, int columns)
        {
            modelMock.Setup(o => o.Rows).Returns(rows);
            modelMock.Setup(o => o.Columns).Returns(columns);
        }

        protected override IGraphicComponent CreateComponent()
        {
            var modelMock = new Mock<ITableModel<CellData>>();
            var selectionModelMock = new Mock<ITableSelectionModel>();
            var renderer = new TableRendererMock<CellData>();
            var table = new TableView<CellData>(modelMock.Object, renderer, selectionModelMock.Object, gameMock.Object);
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