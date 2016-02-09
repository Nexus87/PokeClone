using GameEngine;
using GameEngine.Graphics;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Util
{
    class TestGraphicComponent : AbstractGraphicComponent
    {
        Texture2D texture;
        public TestGraphicComponent()
            : base(new PokeEngine())
        {
            var dev = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.Reach, new PresentationParameters());
            texture = new Texture2D(dev, 1, 1);
        }
        public override void Setup(ContentManager content)
        {
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            var batchMock = (SpriteBatchMock)batch;
            batch.Draw(texture: texture, position: Position, destinationRectangle: null, scale: Size);
        }
    }
}
