using GameEngine.Globals;
using GameEngine.Graphics.General;
using GameEngine.GUI;
using GameEngine.GUI.Panels;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace MainMode.Core.Graphics
{
    [GameType]
    public class MapGraphic : AbstractGraphicComponent, IMapGraphic
    {
        public float TextureSize { get; }
        private readonly Grid _container = new Grid();


        public MapGraphic(ITable<IGraphicComponent> fieldTextures, float textureSize = 128.0f)
        {
            TextureSize = textureSize;
            InitContainer(fieldTextures);
            Rows = fieldTextures.Rows;
            Columns = fieldTextures.Columns;

            Init();
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _container.Draw(time, batch);
        }

        protected override void Update()
        {
            base.Update();
            _container.SetCoordinates(this);
        }

        public void Init()
        {
            var totalHeight = Rows * TextureSize;
            var totalWidth = Columns * TextureSize;

            Area = new Rectangle(Area.X, Area.Y, (int) totalWidth, (int) totalHeight);
        }

        private float Rows { get; }
        private float Columns { get; }

        private void InitContainer(ITable<IGraphicComponent> fieldTextures)
        {
            for (var i = 0; i < fieldTextures.Rows; i++)
                _container.AddPercentRow();
            for (var j = 0; j < fieldTextures.Columns; j++)
                _container.AddPercentColumn();

            for (var i = 0; i < fieldTextures.Rows; i++)
            {
                for (var j = 0; j < fieldTextures.Columns; j++)
                {
                    _container.SetComponent(fieldTextures[i, j], i, j);
                }
            }
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