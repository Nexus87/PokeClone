using GameEngine.GUI;
using GameEngine.GUI.General;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Core
{
    internal class Screen
    {
        private readonly ScreenConstants _screenConstants;

        public GraphicsDevice Device
        {
            get { return _device; }
            set
            {
                _device = value;
                _target = new RenderTarget2D(_device, (int) _screenConstants.ScreenWidth,
                    (int) _screenConstants.ScreenHeight);
            }
        }

        private RenderTarget2D _target;
        private Rectangle _display;
        private readonly RasterizerState _rasterization = new RasterizerState {ScissorTestEnable = true};
        private GraphicsDevice _device;

        public void WindowsResizeHandler(float windowWidth, float windowHeight)
        {
            var bufferX = (float)Device.PresentationParameters.BackBufferWidth;
            var bufferY = (float)Device.PresentationParameters.BackBufferHeight;

            var windowX = windowWidth;
            var windowY = windowHeight;

            var displayRatio = windowX / windowY;
            var invBufferRatio = bufferY / bufferX;

            var scaleX = bufferX / _screenConstants.ScreenWidth;
            var scaleY = displayRatio * invBufferRatio * scaleX;

            if (scaleY * _screenConstants.ScreenHeight > Device.PresentationParameters.BackBufferHeight)
            {
                scaleY = bufferY / _screenConstants.ScreenHeight;
                scaleX = scaleY / (displayRatio * invBufferRatio);
            }

            _display.Width = (int)(scaleX * _screenConstants.ScreenWidth);
            _display.Height = (int)(scaleY * _screenConstants.ScreenHeight);
            _display.X = (int)((bufferX - _display.Width) / 2.0f);
            _display.Y = (int)((bufferY - _display.Height) / 2.0f);
        }

        public Screen(ScreenConstants screenConstants)
        {
            _screenConstants = screenConstants;
        }

        public void Begin(ISpriteBatch batch)
        {
            Device.SetRenderTarget(_target);
            Device.Clear(_screenConstants.BackgroundColor);

            batch.Begin(SpriteSortMode.Immediate, rasterizerState: _rasterization);
        }

        public void Draw(IGraphicComponent component, ISpriteBatch batch, GameTime gameTime)
        {
            component.Draw(gameTime, batch);
        }
        public void End(SpriteBatch batch)
        {
            batch.End();

            Device.SetRenderTarget(null);
            
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
