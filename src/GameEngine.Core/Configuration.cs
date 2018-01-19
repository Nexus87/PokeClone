﻿using System.Collections.Generic;
using GameEngine.Core.GameStates;
using GameEngine.Globals;
using GameEngine.Graphics.Textures;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Core
{
    public class GameConfiguration
    {
        public GameConfiguration(string contentRoot)
        {
            TextureConfigurationBuilder = new TextureConfigurationBuilder(contentRoot);
            keyMap.Add(Keys.Up, CommandKeys.Up);
            keyMap.Add(Keys.Down, CommandKeys.Down);
            keyMap.Add(Keys.Left, CommandKeys.Left);
            keyMap.Add(Keys.Right, CommandKeys.Right);
            keyMap.Add(Keys.Enter, CommandKeys.Select);
            keyMap.Add(Keys.Escape, CommandKeys.Back);
        }

        private Dictionary<Keys, CommandKeys> keyMap = new Dictionary<Keys, CommandKeys>();

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
            return (T)ret;
        }

        public State InitialState { get; set; }
        public IReadOnlyDictionary<Keys, CommandKeys> KeyMap => keyMap;
        public TextureConfigurationBuilder TextureConfigurationBuilder { get; }
    }
}
