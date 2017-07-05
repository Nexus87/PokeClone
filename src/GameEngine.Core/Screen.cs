using GameEngine.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Core
{
    public class Screen
    {
        private readonly ScreenConstants _screenConstants;
        private readonly GraphicsDeviceManager _graphicsDeviceManager;

        public RenderTarget2D Target { get; private set; }
        public Rectangle TargetRectangle { get; private set; }
        private GraphicsDevice _device;

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
            Target = new RenderTarget2D(Device, (int) _screenConstants.ScreenWidth,
                (int) _screenConstants.ScreenHeight);
        }
    }
}
