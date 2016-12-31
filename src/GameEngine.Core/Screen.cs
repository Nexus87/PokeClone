using GameEngine.GUI;
using GameEngine.GUI.General;
using GameEngine.GUI.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Core
{
    internal class Screen
    {
        private readonly ScreenConstants _screenConstants;
        private readonly GraphicsDevice _device;
        private readonly RenderTarget2D _target;
        private Rectangle _display;
        private readonly RasterizerState _rasterization = new RasterizerState {ScissorTestEnable = true};

        public void WindowsResizeHandler(float windowWidth, float windowHeight)
        {
            var bufferX = (float)_device.PresentationParameters.BackBufferWidth;
            var bufferY = (float)_device.PresentationParameters.BackBufferHeight;

            var windowX = windowWidth;
            var windowY = windowHeight;

            var displayRatio = windowX / windowY;
            var invBufferRatio = bufferY / bufferX;

            var scaleX = bufferX / _screenConstants.ScreenWidth;
            var scaleY = displayRatio * invBufferRatio * scaleX;

            if (scaleY * _screenConstants.ScreenHeight > _device.PresentationParameters.BackBufferHeight)
            {
                scaleY = bufferY / _screenConstants.ScreenHeight;
                scaleX = scaleY / (displayRatio * invBufferRatio);
            }

            _display.Width = (int)(scaleX * _screenConstants.ScreenWidth);
            _display.Height = (int)(scaleY * _screenConstants.ScreenHeight);
            _display.X = (int)((bufferX - _display.Width) / 2.0f);
            _display.Y = (int)((bufferY - _display.Height) / 2.0f);
        }

        public Screen(ScreenConstants screenConstants, GraphicsDevice device)
        {
            _screenConstants = screenConstants;
            _device = device;
            _target = new RenderTarget2D(device, (int)screenConstants.ScreenWidth, (int)screenConstants.ScreenHeight);

        }

        public void Begin(ISpriteBatch batch)
        {
            _device.SetRenderTarget(_target);
            _device.Clear(_screenConstants.BackgroundColor);

//            batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, rasterizerState: _rasterization);
            batch.Begin();
        }

        public void Draw(IGraphicComponent component, ISpriteBatch batch, GameTime gameTime)
        {
            component.Draw(gameTime, batch);
        }
        public void End(SpriteBatch batch)
        {
            batch.End();

            _device.SetRenderTarget(null);
            
            batch.Begin();
            batch.Draw(_target, destinationRectangle: _display);
            batch.End();
        }

        public void Draw(GuiManager guiManager, ISpriteBatch batch, GameTime gameTime)
        {
            guiManager.Draw(gameTime, batch);
        }
    }
}
