using GameEngine.Globals;
using GameEngine.Graphics.General;
using GameEngine.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Core
{
    public class Screen : IScreen
    {
        public ScreenConstants Constants { get; }
        private readonly GraphicsDeviceManager _graphicsDeviceManager;

        private RenderTarget2D _guiRenderTarget;
        private RenderTarget2D _sceneRenderTarget;
        private RenderTarget2D _target;

        private Rectangle _targetRectangle;
        private GraphicsDevice _device;
        private XnaSpriteBatch _spriteBatch;

        public ISkin Skin { get; set; }
        public ISpriteBatch GuiSpriteBatch
        {
            get
            {
                _spriteBatch.GraphicsDevice.SetRenderTarget(_guiRenderTarget);
                return _spriteBatch;
            }
        }

        public ISpriteBatch SceneSpriteBatch
        {
            get
            {
                _spriteBatch.GraphicsDevice.SetRenderTarget(_sceneRenderTarget);
                Device.Clear(Constants.BackgroundColor);
                return _spriteBatch;
            }
        }

        private XnaSpriteBatch DefaultSpriteBatch
        {
            get
            {
                _spriteBatch.GraphicsDevice.SetRenderTarget(null);
                return _spriteBatch;
            }
        }

        private GraphicsDevice Device
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

            var scaleX = bufferX / Constants.ScreenWidth;
            var scaleY = displayRatio * invBufferRatio * scaleX;

            if (scaleY * Constants.ScreenHeight > Device.PresentationParameters.BackBufferHeight)
            {
                scaleY = bufferY / Constants.ScreenHeight;
                scaleX = scaleY / (displayRatio * invBufferRatio);
            }

            _targetRectangle.Width = (int)(scaleX * Constants.ScreenWidth);
            _targetRectangle.Height = (int)(scaleY * Constants.ScreenHeight);
            _targetRectangle.X = (int)((bufferX - _targetRectangle.Width) / 2.0f);
            _targetRectangle.Y = (int)((bufferY - _targetRectangle.Height) / 2.0f);
        }

        public Screen(ScreenConstants screenConstants, GraphicsDeviceManager graphicsDeviceManager)
        {
            Constants = screenConstants;
            _graphicsDeviceManager = graphicsDeviceManager;
        }


        private void Init()
        {
            _device = _graphicsDeviceManager.GraphicsDevice;
            
            _target = CreateRenderTarget();
            _guiRenderTarget = CreateRenderTarget();
            _sceneRenderTarget = CreateRenderTarget();

            _spriteBatch = new XnaSpriteBatch(_graphicsDeviceManager.GraphicsDevice);
        }

        private RenderTarget2D CreateRenderTarget()
        {
            return new RenderTarget2D(Device, 
                (int) Constants.ScreenWidth,
                (int) Constants.ScreenHeight);
        }

        public void Draw()
        {
            DefaultSpriteBatch.GraphicsDevice.SetRenderTarget(null);
            DefaultSpriteBatch.Begin();
            DefaultSpriteBatch.Draw(_sceneRenderTarget, _targetRectangle);
            DefaultSpriteBatch.Draw(_guiRenderTarget, _targetRectangle);
            DefaultSpriteBatch.End();
        }
    }
}
