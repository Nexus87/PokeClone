using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI
{
    public class Scene
    {
        public IGraphicComponent Root { get; set; }

        public void Draw(ISpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            DrawComponent(spriteBatch, gameTime, Root);
            spriteBatch.End();
        }


        private static void DrawComponent(ISpriteBatch spriteBatch, GameTime gameTime, IGraphicComponent component)
        {
            foreach (var graphicComponent in component.Children)
            {
                graphicComponent.Draw(gameTime, spriteBatch);
            }
        }
    }
}