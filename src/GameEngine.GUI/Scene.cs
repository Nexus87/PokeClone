﻿using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI
{
    public class Scene
    {
        public IGraphicComponent Root { get; set; }
        private readonly ISpriteBatch _spriteBatch;

        public Scene(ISpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            DrawComponent(gameTime, Root);
            _spriteBatch.End();
        }


        private void DrawComponent(GameTime gameTime, IGraphicComponent component)
        {
            foreach (var graphicComponent in component.Children)
            {
                graphicComponent.Draw(gameTime, _spriteBatch);
            }
        }
    }
}