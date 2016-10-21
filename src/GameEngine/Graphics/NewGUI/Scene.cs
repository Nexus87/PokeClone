using System.Collections.Generic;
using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.Graphics.NewGUI
{
    public class Scene
    {
        public IGraphicComponent Root { get; set; }
        private readonly ISpriteBatch spriteBatch;

        public Scene(ISpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            DrawComponent(gameTime, Root);
            spriteBatch.End();
        }


        private void DrawComponent(GameTime gameTime, IGraphicComponent component)
        {
            foreach (var graphicComponent in component.Children)
            {
                graphicComponent.Update(gameTime);
                graphicComponent.Renderer.Render(spriteBatch, component.Constraints);
            }
        }
    }
}