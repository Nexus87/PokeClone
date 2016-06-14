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
        private AutofacGameRegistry registry = new AutofacGameRegistry();

        public GraphicComponentFactory(Configuration config, PokeEngine game)
        {
            DefaultArrowTexture = new XNATexture2D(config.DefaultArrowTexture, game.Content);
            DefaultFont = new XNASpriteFont(config.DefaultFont, game.Content);
            DefaultBorderTexture = new XNATexture2D(config.DefaultBorderTexture, game.Content);
            pixel = new XNATexture2D();
            Cup = new XNATexture2D("circle", game.Content);

            game.factory = this;
            GameEngineTypes.Register(registry);
        }
        public T CreateGraphicComponent<T>() where T : IGraphicComponent
        {
            if (typeof(T) == typeof(Line))
                return (T) (Object) new Line(pixel, Cup);
            else if (typeof(T) == typeof(ItemBox))
            {
                var parameters = new Dictionary<Type, object>{
                    {typeof(TextureBox), new TextureBox(DefaultArrowTexture)}, 
                    {typeof(TextBox), new TextBox(DefaultFont)}
                };
                return registry.ResolveTypeWithParameters<T>(parameters);
            }

            throw new NotSupportedException("Type not found");
        }

        public void Setup(Game game)
        {
            pixel.Texture = new Texture2D(game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color, 1);
            pixel.SetData(new[] { Color.White });
        }

        public ITableView<T> CreateTableView<T>(ITableModel<T> model = null, ITableRenderer<T> renderer = null, ITableSelectionModel selectionModel = null)
        {
            if (model == null)
                model = new DefaultTableModel<T>();
            if (renderer == null)
                renderer = new DefaultTableRenderer<T>(this);
            if (selectionModel == null)
                selectionModel = new TableSingleSelectionModel();

            var parameters = new Dictionary<Type, object>{
                {typeof(ITableModel<T>), model},
                {typeof(ITableRenderer<T>), renderer},
                {typeof(ITableSelectionModel), selectionModel}
            };
            return registry.ResolveTypeWithParameters<ITableView<T>>(parameters);
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
