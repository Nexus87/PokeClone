using GameEngine.Globals;
using GameEngine.Graphics.General;
using GameEngine.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Core
{
    public class Screen : IScreen
    {
        private readonly ScreenConstants _screenConstants;
        private readonly GraphicsDeviceManager _graphicsDeviceManager;

        private RenderTarget2D _guiRenderTarget;
        private RenderTarget2D _sceneRenderTarget;
        private RenderTarget2D _target;

        private Rectangle TargetRectangle { get; set; }
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

            var scaleX = bufferX / _screenConstants.ScreenWidth;
            var scaleY = displayRatio * invBufferRatio * scaleX;

            if (scaleY * _screenConstants.ScreenHeight > Device.PresentationParameters.BackBufferHeight)
            {
                scaleY = bufferY / _screenConstants.ScreenHeight;
                scaleX = scaleY / (displayRatio * invBufferRatio);
            }

            TargetRectangle = new Rectangle
            {
                X = (int)((bufferX - TargetRectangle.Width) / 2.0f),
                Y= (int)((bufferY - TargetRectangle.Height) / 2.0f),
                Width = (int)(scaleX * _screenConstants.ScreenWidth),
                Height = (int)(scaleY * _screenConstants.ScreenHeight)

            };
        }

        public Screen(ScreenConstants screenConstants, GraphicsDeviceManager graphicsDeviceManager)
        {
            _screenConstants = screenConstants;
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
                (int) _screenConstants.ScreenWidth,
                (int) _screenConstants.ScreenHeight);
        }

        public void Draw()
        {
            DefaultSpriteBatch.GraphicsDevice.SetRenderTarget(_target);

            DefaultSpriteBatch.Begin();
            DefaultSpriteBatch.Draw(_sceneRenderTarget);
            DefaultSpriteBatch.Draw(_guiRenderTarget);
            DefaultSpriteBatch.End();

            DefaultSpriteBatch.GraphicsDevice.SetRenderTarget(null);
            DefaultSpriteBatch.Begin();
            DefaultSpriteBatch.Draw(_target, TargetRectangle);
            DefaultSpriteBatch.End();
        }
    }
}
