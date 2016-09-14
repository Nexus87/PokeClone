using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    class Screen
    {
        private ScreenConstants screenConstants;
        private GraphicsDevice device;
        private RenderTarget2D target;
        private Rectangle display = new Rectangle();

        public void WindowsResizeHandler(float windowWidth, float windowHeight)
        {
            var bufferX = (float)device.PresentationParameters.BackBufferWidth;
            var bufferY = (float)device.PresentationParameters.BackBufferHeight;

            var windowX = windowWidth;
            var windowY = windowHeight;

            var displayRatio = windowX / windowY;
            var invBufferRatio = bufferY / bufferX;

            var scaleX = bufferX / screenConstants.ScreenWidth;
            var scaleY = displayRatio * invBufferRatio * scaleX;

            if (scaleY * screenConstants.ScreenHeight > device.PresentationParameters.BackBufferHeight)
            {
                scaleY = bufferY / screenConstants.ScreenHeight;
                scaleX = scaleY / (displayRatio * invBufferRatio);
            }

            display.Width = (int)(scaleX * screenConstants.ScreenWidth);
            display.Height = (int)(scaleY * screenConstants.ScreenHeight);
            display.X = (int)((bufferX - display.Width) / 2.0f);
            display.Y = (int)((bufferY - display.Height) / 2.0f);
        }

        public Screen(ScreenConstants screenConstants, GraphicsDevice device)
        {
            this.screenConstants = screenConstants;
            this.device = device;
            target = new RenderTarget2D(device, (int)screenConstants.ScreenWidth, (int)screenConstants.ScreenHeight);

        }

        public void Begin(ISpriteBatch batch)
        {
            device.SetRenderTarget(target);
            device.Clear(screenConstants.BackgroundColor);

            batch.Begin();
        }

        public void Draw(IGraphicComponent component, ISpriteBatch batch, GameTime gameTime)
        {
            component.Draw(gameTime, batch);
        }
        public void End(SpriteBatch batch)
        {
            batch.End();

            device.SetRenderTarget(null);
            
            batch.Begin();
            batch.Draw(target, destinationRectangle: display);
            batch.End();
        }

        public void Draw(GUIManager GUIManager, ISpriteBatch batch, GameTime gameTime)
        {
            GUIManager.Draw(gameTime, batch);
        }
    }
}
