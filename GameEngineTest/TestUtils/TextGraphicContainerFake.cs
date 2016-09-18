using GameEngine.Graphics;
using GameEngine.Utils;
using System;

namespace GameEngineTest.TestUtils
{
    public class TextGraphicContainerFake : GraphicComponentMock, ITextGraphicContainer
    {
        private float height;
        private float width;
        private float x;
        private float y;
        public bool HasNext()
        {
            return true;
        }

        public void NextLine()
        {
        }

        public string Text { get; set; }

        public override float Height
        {
            get
            {
                return height;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("value");

                if (!height.AlmostEqual(value))
                {
                    height = value;
                    OnSizeChanged();
                }
                else
                    height = value;

            }
        }

        public override float Width
        {
            get
            {
                return width;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("value");

                if (width.AlmostEqual(value))
                    return;

                    width = value;
                    OnSizeChanged();
            }
        }

        public override float XPosition
        {
            get
            {
                return x;
            }
            set
            {
                if (x.AlmostEqual(value))
                    return;
                x = value;
                OnPositionChanged();
            }
        }

        public override float YPosition
        {
            get
            {
                return y;
            }
            set
            {
                if (y.AlmostEqual(value))
                    return;
                y = value;
                OnPositionChanged();
            }
        }
    }
}
