using GameEngine.Utils;
using Microsoft.Xna.Framework;
using System;

namespace GameEngine.Graphics
{
    public class ResizeAnimation : IAnimation
    {
        public float StartWidth { get; set; }
        public float StartHeight { get; set; }

        public float EndWidth { get; set; }
        public float EndHeight { get; set; }

        public float SpeedX { get; set; }
        public float SpeedY { get; set; }

        private float currentWidth;
        private float currentHeight;

        private bool IsInitialized;
        private bool shrinkX;
        private bool shrinkY;

        public void SetStartSize(IGraphicComponent component)
        {
            component.CheckNull("component");

            StartWidth = component.Width;
            StartHeight = component.Height;
        }


        public event EventHandler AnimationFinished = delegate { };

        public void Update(GameTime time, IGraphicComponent component)
        {
            component.CheckNull("component");
            if (!IsInitialized)
                Init();

            if (currentWidth.CompareTo(EndWidth) == 0 && currentHeight.CompareTo(EndHeight) == 0)
                Finished();
            if (time.ElapsedGameTime.TotalMilliseconds.AlmostEqual(0))
                return;

            if (shrinkX)
                currentWidth = Math.Max(currentWidth - SpeedX, EndWidth);
            else
                currentWidth = Math.Min(currentWidth + SpeedX, EndWidth);

            if (shrinkY)
                currentHeight = Math.Max(currentHeight - SpeedY, EndHeight);
            else
                currentHeight = Math.Max(currentWidth + SpeedY, EndHeight);

            component.Width = currentWidth;
            component.Height = currentHeight;
        }

        private void Finished()
        {
            IsInitialized = false;

            AnimationFinished(this, null);
        }

        private void Init()
        {
            currentWidth = StartWidth;
            currentHeight = StartHeight;

            shrinkX = EndWidth.CompareTo(StartWidth) < 0;
            shrinkY = EndHeight.CompareTo(StartHeight) < 0;

            IsInitialized = true;
        }
    }
}
