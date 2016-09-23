using GameEngine;
using GameEngine.Graphics;
using GameEngine.Registry;
using Microsoft.Xna.Framework;

namespace MainModule
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
            this.TextureSize = textureSize;
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
            loader.LoadMap();
            var fieldTextures = loader.GetFieldTextures();
            FieldSize = new FieldSize(fieldTextures.Columns, fieldTextures.Rows);

            TotalHeight = fieldTextures.Rows * TextureSize;
            TotalWidth = fieldTextures.Columns * TextureSize;

            container.SetCoordinates(0, 0, TotalWidth, TotalHeight);
            foreach (var component in fieldTextures.EnumerateAlongRows())
                container.AddComponent(component);

            container.Layout = new GridLayout(fieldTextures.Rows, fieldTextures.Columns);

            container.Setup();
            Height = TotalHeight;
            Width = TotalWidth;
        }

        public float GetXPositionOfColumn(int column)
        {
            return column * TextureSize;
        }

        public float GetYPositionOfRow(int row)
        {
            return row * TextureSize;
        }

        private float TotalWidth { get; set; }
        private float TotalHeight { get; set; }

        public FieldSize FieldSize { get; private set; }
    }
}