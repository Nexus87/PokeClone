using System;
using System.Linq;
using GameEngine.Globals;
using GameEngine.Graphics.General;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics.Textures
{
    [GameService(typeof(TextureBuilder))]
    public class TextureBuilder
    {
        private readonly GraphicsDeviceManager _deviceManager;

        public TextureBuilder(GraphicsDeviceManager deviceManager)
        {
            _deviceManager = deviceManager;
        }

        public ITexture2D BuildTexture(Table<Tuple<Rectangle, ITexture2D>> table)
        {
            var graphicsDevice = _deviceManager.GraphicsDevice;
            var left = table.Min(x => x.Item1.Left);
            var right = table.Max(x => x.Item1.Right);
            var top = table.Min(x => x.Item1.Top);
            var bottom = table.Max(x => x.Item1.Bottom);

            var renderTarget = new RenderTarget2D(graphicsDevice, right - left, bottom - top);
            ISpriteBatch spriteBatch = new XnaSpriteBatch(graphicsDevice);

            var backup = graphicsDevice.GetRenderTargets();

            graphicsDevice.SetRenderTarget(renderTarget);

            spriteBatch.Begin();
            foreach (var texture in table)
            {
                spriteBatch.Draw(texture.Item2, texture.Item1, Color.White);
            }
            spriteBatch.End();

            graphicsDevice.SetRenderTargets(backup);

            return new XnaTexture2D(renderTarget);
        }
    }
}