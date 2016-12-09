using GameEngine.Graphics;
using GameEngine.Utils;
using System;

namespace GameEngineTest.TestUtils
{
    public class TextGraphicContainerFake : GraphicComponentMock, ITextGraphicContainer
    {
        private float _height;
        private float _width;
        private float _x;
        private float _y;
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
                return _height;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("value");

                if (!_height.AlmostEqual(value))
                {
                    _height = value;
                    OnSizeChanged();
                }
                else
                    _height = value;

            }
        }

        public override float Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("value");

                if (_width.AlmostEqual(value))
                    return;

                    _width = value;
                    OnSizeChanged();
            }
        }

        public override float XPosition
        {
            get
            {
                return _x;
            }
            set
            {
                if (_x.AlmostEqual(value))
                    return;
                _x = value;
            }
        }

        public override float YPosition
        {
            get
            {
                return _y;
            }
            set
            {
                if (_y.AlmostEqual(value))
                    return;
                _y = value;
            }
        }
    }
}
