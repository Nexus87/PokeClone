using GameEngine.Graphics.Basic;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics.Animations
{
    public class ResizeAnimation : IAnimation
    {
        public float StartWidth { get; set; }
        public float StartHeigth { get; set; }

        public float EndWidth { get; set; }
        public float EndHeigth { get; set; }

        public float SpeedX { get; set; }
        public float SpeedY { get; set; }

        private float currentWidth = 0;
        private float currentHeight = 0;

        private bool IsInitialized = false;
        private bool shrinkX;
        private bool shrinkY;
        public void SetStartSize(IGraphicComponent component)
        {
            StartWidth = component.Width;
            StartHeigth = component.Height;
        }


        public event EventHandler AnimationFinished = delegate { };

        public void Update(GameTime time, IGraphicComponent component)
        {
            if (!IsInitialized)
                Init();

            if (currentWidth.CompareTo(EndWidth) == 0 && currentHeight.CompareTo(EndHeigth) == 0)
                Finished();
            if (time.ElapsedGameTime.TotalMilliseconds != 0)
                return;

            if (shrinkX)
                currentWidth = Math.Max(currentWidth - SpeedX, EndWidth);
            else
                currentWidth = Math.Min(currentWidth + SpeedX, EndWidth);

            if (shrinkY)
                currentHeight = Math.Max(currentHeight - SpeedY, EndHeigth);
            else
                currentHeight = Math.Max(currentWidth + SpeedY, EndHeigth);

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
            currentHeight = StartHeigth;

            shrinkX = EndWidth.CompareTo(StartWidth) < 0;
            shrinkY = EndHeigth.CompareTo(StartHeigth) < 0;

            IsInitialized = true;
        }
    }
}
