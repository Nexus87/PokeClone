using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Configuration
{
    public class Configuration
    {
        public Configuration() 
        {
            keyMap.Add(Keys.Up, CommandKeys.Up);
            keyMap.Add(Keys.Down, CommandKeys.Down);
            keyMap.Add(Keys.Left, CommandKeys.Left);
            keyMap.Add(Keys.Right, CommandKeys.Right);
            keyMap.Add(Keys.Enter, CommandKeys.Select);
            keyMap.Add(Keys.Escape, CommandKeys.Back);
            DefaultFont = "MenuFont";
            DefaultBorderTexture = "border";
            DefaultArrowTexture = "arrow";
        }

        protected Dictionary<Keys, CommandKeys> keyMap = new Dictionary<Keys, CommandKeys>();

        public string DefaultBorderTexture { get; private set; }
        public string DefaultFont { get; private set; }
        public string DefaultArrowTexture { get; private set; }

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

        public IReadOnlyDictionary<Keys, CommandKeys> KeyMap { get { return keyMap; } }
    }
}
