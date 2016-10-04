using GameEngine;
using GameEngine.Graphics;
using GameEngine.Registry;
using GameEngine.Utils;
using Microsoft.Xna.Framework;

namespace MainModule.Graphics
{
    [GameType]
    public class MapGraphic : AbstractGraphicComponent, IMapGraphic
    {
        public float TextureSize { get; private set; }
        private readonly Container container = new Container();


        public MapGraphic(ITable<IGraphicComponent> fieldTextures, float textureSize = 128.0f)
        {
            TextureSize = textureSize;
            InitContainer(fieldTextures);
            Rows = fieldTextures.Rows;
            Columns = fieldTextures.Columns;
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

            var totalHeight = Rows * TextureSize;
            var totalWidth = Columns * TextureSize;

            Height = totalHeight;
            Width = totalWidth;
        }

        private float Rows { get;  set; }
        private float Columns { get; set; }

        private void InitContainer(ITable<IGraphicComponent> fieldTextures)
        {

            foreach (var component in fieldTextures.EnumerateAlongRows())
                container.AddComponent(component);

            container.Layout = new GridLayout(fieldTextures.Rows, fieldTextures.Columns);
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