using GameEngine;
using GameEngine.Graphics;
using GameEngine.Registry;
using GameEngine.Utils;
using Microsoft.Xna.Framework;

namespace MainModule.Graphics
{
    [GameType]
    public class Map : AbstractGraphicComponent, IMap
    {
        private readonly IMapLoader loader;
        public float TextureSize { get; private set; }
        private readonly Container container = new Container();


        public Map(IMapLoader loader, float textureSize = 128.0f)
        {
            this.loader = loader;
            TextureSize = textureSize;
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            container.Draw(time, batch);
        }

        protected override void Update()
        {
            base.Update();
            container.SetCoordinates(this);
        }

        public override void Setup()
        {
            loader.LoadMap("");
            var fieldTextures = loader.GetFieldTextures();
            InitContainer(fieldTextures);

            var totalHeight = fieldTextures.Rows * TextureSize;
            var totalWidth = fieldTextures.Columns * TextureSize;

            Height = totalHeight;
            Width = totalWidth;
        }

        private void InitContainer(ITable<IGraphicComponent> fieldTextures)
        {

            foreach (var component in fieldTextures.EnumerateAlongRows())
                container.AddComponent(component);

            container.Layout = new GridLayout(fieldTextures.Rows, fieldTextures.Columns);
            container.Setup();
        }

        public float GetXPositionOfColumn(int column)
        {
            return column * TextureSize;
        }

        public float GetYPositionOfRow(int row)
        {
            return row * TextureSize;
        }

    }
}