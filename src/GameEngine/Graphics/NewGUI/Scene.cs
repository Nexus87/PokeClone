using System.Collections.Generic;
using GameEngine.Graphics.General;

namespace GameEngine.Graphics.NewGUI
{
    public class Scene
    {
        public IGraphicComponent Root
        {
            get { return root; }
            set
            {
                root = value;
                root.UpdateRequested += (sender, args) => components.Add(args);
            }
        }

        private readonly ISpriteBatch spriteBatch;
        private IGraphicComponent root;
        private List<UpdateRequestedArgs> components = new List<UpdateRequestedArgs>();

        public Scene(ISpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        public void Draw()
        {
            Update();
            spriteBatch.Begin();
            DrawComponent(Root);
            spriteBatch.End();
        }

        private void Update()
        {
            while (components.Count != 0)
            {
                // Updating components may trigger more updates
                var currentComponents = components;
                components = new List<UpdateRequestedArgs>();

                foreach (var updateRequestedArg in currentComponents)
                {
                    updateRequestedArg.RequestingComponent.Update();
                }
            }
        }

        private void DrawComponent(IGraphicComponent component)
        {
            if (component.Drawable != null)
                component.Drawable.Draw(spriteBatch, component);

            foreach (var graphicComponent in component.Children)
            {
                DrawComponent(graphicComponent);
            }
        }
    }
}