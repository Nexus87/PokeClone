using GameEngine.Graphics.GUI;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    public class GraphicComponentFactory
    {
        private XNATexture2D pixel;
        private ISpriteFont DefaultFont { get; set; }
        private ITexture2D DefaultArrowTexture { get; set; }
        private ITexture2D DefaultBorderTexture { get; set; }
        private ITexture2D Pixel { get { return pixel; } }
        private ITexture2D Cup { get; set; }

        public GraphicComponentFactory(Configuration config, PokeEngine game)
        {
            DefaultArrowTexture = new XNATexture2D(config.DefaultArrowTexture, game.Content);
            DefaultFont = new XNASpriteFont(config.DefaultFont, game.Content);
            DefaultBorderTexture = new XNATexture2D(config.DefaultBorderTexture, game.Content);
            pixel = new XNATexture2D();
            Cup = new XNATexture2D("circle", game.Content);
            game.factory = this;

        }
        public T CreateGraphicComponent<T>() where T : IGraphicComponent
        {
            if (typeof(T) == typeof(Line))
                return (T) (Object) new Line(pixel, Cup);
            else if (typeof(T) == typeof(ItemBox))
                return (T)(Object)new ItemBox(new TextureBox(DefaultArrowTexture), new TextBox(DefaultFont));

            throw new NotSupportedException("Type not found");
        }

        public void Setup(Game game)
        {
            pixel.Texture = new Texture2D(game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color, 1);
            pixel.SetData(new[] { Color.White });
        }

        public TableView<T> CreateTableView<T>(ITableModel<T> model = null, ITableRenderer<T> renderer = null, ITableSelectionModel selectionModel = null)
        {
            if (model == null)
                model = new DefaultTableModel<T>();
            if (renderer == null)
                renderer = new DefaultTableRenderer<T>(this);
            if (selectionModel == null)
                selectionModel = new DefaultTableSelectionModel();

            return new TableView<T>(model, renderer, selectionModel);
        }

        public TableWidget<T> CreateTableWidget<T>(ITableView<T> view = null, int? visibleRows = null, int? visibleColumns = null)
        {
            if (view == null)
                view = CreateTableView<T>();
            return new TableWidget<T>(visibleRows, visibleColumns, view);
        }

        public MessageBox CreateMessageBox(int lineNumber = 2)
        {
            return new MessageBox(DefaultFont, new DefaultTextSplitter(), lineNumber);
        }

        public Dialog CreateDialog()
        {
            return new Dialog(DefaultBorderTexture);
        }

        public TextBox CreateTextBox()
        {
            return new TextBox(DefaultFont);
        }
    }
}
