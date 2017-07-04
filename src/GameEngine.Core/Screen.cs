using GameEngine.Globals;
using GameEngine.Graphics;
using GameEngine.Graphics.General;
using GameEngine.GUI;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Core
{
    [GameService(typeof(Screen))]
    public class Screen
    {
        private readonly ScreenConstants _screenConstants;
        private readonly GraphicsDeviceManager _graphicsDeviceManager;

        private RenderTarget2D _target;
        private Rectangle _display;
        private readonly RasterizerState _rasterization = new RasterizerState {ScissorTestEnable = true};
        private GraphicsDevice _device;

        public GraphicsDevice Device
        {
            get
            {
                if (_device == null)
                    Init();
                return _device;
            }
        }

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

        public Screen(ScreenConstants screenConstants, GraphicsDeviceManager graphicsDeviceManager)
        {
            _screenConstants = screenConstants;
            _graphicsDeviceManager = graphicsDeviceManager;
        }

        public void Begin(ISpriteBatch batch)
        {
            Device.SetRenderTarget(_target);
            Device.Clear(_screenConstants.BackgroundColor);

            batch.Begin(SpriteSortMode.Immediate, rasterizerState: _rasterization);
        }

        private void Init()
        {
            _device = _graphicsDeviceManager.GraphicsDevice;
            _target = new RenderTarget2D(Device, (int) _screenConstants.ScreenWidth,
                (int) _screenConstants.ScreenHeight);
        }

        public void Draw(Scene scene, ISpriteBatch batch, GameTime gameTime)
        {
            scene.DrawScene(batch);
        }

        public void Draw(RenderTarget2D scene, RenderTarget2D gui, ISpriteBatch batch)
        {
            batch.Begin();
            batch.Draw(scene, destinationRectangle: _display);
            if (gui != null)
            {
                batch.Draw(gui, _display);
            }
            batch.End();
        }

        public void End(ISpriteBatch batch)
        {
            batch.End();

            Device.SetRenderTarget(null);
            
            batch.Begin();
            batch.Draw(_target, destinationRectangle: _display);
            batch.End();
        }

        internal void Draw(GuiManager guiManager, ISpriteBatch batch, GameTime gameTime)
        {
            guiManager.Draw(gameTime, batch);
        }
    }
}
