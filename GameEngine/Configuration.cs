using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Configuration
    {
        public Configuration() 
        {
            KeyUp = Keys.Up;
            KeyDown = Keys.Down;
            KeyLeft = Keys.Left;
            KeyRight = Keys.Right;
            KeySelect = Keys.Enter;
            KeyBack = Keys.Escape;
        }

        public Keys KeyUp { get; private set; }
        public Keys KeyDown { get; private set; }
        public Keys KeyLeft { get; private set; }
        public Keys KeyRight { get; private set; }
        public Keys KeySelect { get; private set; }
        public Keys KeyBack { get; private set; }

        public virtual T GetOption<T>(string optionString)
        {
            object ret = null;
            switch (optionString)
            {
                case "UpKey":
                    ret = Keys.Up;
                    break;
                case "DownKey":
                    ret = Keys.Down;
                    break;
                case "LeftKey":
                    ret = Keys.Left;
                    break;
                case "RightKey":
                    ret = Keys.Right;
                    break;
                case "SelectKey":
                    ret = Keys.Enter;
                    break;
                case "BackKey":
                    ret = Keys.Escape;
                    break;
            }
            return (T) ret;
        }
    }
}
