﻿using FakeItEasy;
using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Test
{
    public static class TestExtensions
    {
        public static void Draw(this IGuiComponent component)
        {
            var spriteBatch =  A.Fake<ISpriteBatch>();
            component.Draw(new GameTime(), spriteBatch);
        }
    }
}