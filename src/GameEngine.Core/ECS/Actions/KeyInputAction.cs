﻿using GameEngine.Globals;

namespace GameEngine.Core.ECS.Actions
{
    public class KeyInputAction
    {

        public readonly CommandKeys Key;

        public KeyInputAction(CommandKeys key)
        {
            Key = key;
        }
    }
}