﻿using GameEngine.Graphics.Views;
using GameEngine.Graphics.Widgets;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    public class GUIManager : IInputHandler
    {
        public event EventHandler GUIClose = delegate { };

        IInputHandler handler;
        public IWidget GUIWidget { private get; set; }

        public void HandleInput(Keys key)
        {
            handler.HandleInput(key);
        }


        internal void Show()
        {
            if (GUIWidget == null)
                return;

            handler = GUIWidget;
        }

        internal void Draw(GameTime time, ISpriteBatch batch)
        {
            GUIWidget.Draw(time, batch);
        }
    }
}