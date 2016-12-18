﻿using FakeItEasy;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Test
{
    public static class TestExtensions
    {
        public static void Draw(this IGraphicComponent component)
        {
            var spriteBatch =  A.Fake<ISpriteBatch>();
            component.Draw(new GameTime(), spriteBatch);
        }
    }
}