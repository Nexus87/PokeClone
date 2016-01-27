using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameEngine.Graphics
{
    public class GUIManager : IInputHandler
    {
        private GUI gui;

        public GUIManager()
        {
            IsActive = false;
        }

        public event EventHandler GUIClose = delegate { };

        public GUI GUI
        {
            set
            {
                if (gui != null)
                    gui.OnCloseGUI -= gui_OnRequestExit;

                if (value != null)
                    value.OnCloseGUI += gui_OnRequestExit;

                gui = value;
            }
        }

        internal bool IsActive { get; private set; }

        public bool HandleInput(Keys key)
        {
            if (gui == null)
                throw new InvalidOperationException("No GUI set");

            gui.HandleInput(key);

            return true;
        }

        internal void Close()
        {
            IsActive = false;
        }

        internal void Draw(GameTime time, ISpriteBatch batch)
        {
            if (IsActive)
                gui.Draw(time, batch);
        }

        internal void Show()
        {
            if (gui == null)
                throw new InvalidOperationException("No GUI set");

            IsActive = true;
        }

        private void gui_OnRequestExit(object sender, EventArgs e)
        {
            GUIClose(this, null);
        }

        public void Setup(ContentManager Content)
        {
            if (gui != null)
                gui.Setup(Content);
        }
    }
}