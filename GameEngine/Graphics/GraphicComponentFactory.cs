using GameEngine.Graphics.GUI;
using GameEngine.Registry;
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
        internal XNATexture2D pixel;
        internal ISpriteFont DefaultFont { get; set; }
        internal ITexture2D DefaultArrowTexture { get; set; }
        internal ITexture2D DefaultBorderTexture { get; set; }
        internal ITexture2D Pixel { get { return pixel; } }
        internal ITexture2D Cup { get; set; }
        public readonly IGameTypeRegistry registry = new AutofacGameTypeRegistry();

        public GraphicComponentFactory(Configuration config, PokeEngine game)
        {
            DefaultArrowTexture = new XNATexture2D(config.DefaultArrowTexture, game.Content);
            DefaultFont = new XNASpriteFont(config.DefaultFont, game.Content);
            DefaultBorderTexture = new XNATexture2D(config.DefaultBorderTexture, game.Content);
            pixel = new XNATexture2D();
            Cup = new XNATexture2D("circle", game.Content);

            game.factory = this;
            GameEngineTypes.Register(registry, this);
        }
        public T CreateGraphicComponent<T>() where T : IGraphicComponent
        {
            if (typeof(T) == typeof(Line) 
                || typeof(T) == typeof(ItemBox) 
                || typeof(T) == typeof(TextBox)
                || typeof(T) == typeof(Dialog)
                || typeof(T) == typeof(MessageBox)
                )
                return registry.ResolveType<T>();

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
                renderer = new DefaultTableRenderer<T>(registry);
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
    }
}
