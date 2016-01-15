using Base;
using BattleLib.GraphicComponents.GUI;
using BattleLib.GraphicComponents.MenuView;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.Basic;
using GameEngine.Graphics.Views;
using GameEngine.Graphics.Widgets;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace BattleLib.GraphicComponents
{
    public class BattleGraphics : AbstractGraphicComponent
    {

        public MenuGraphics Menu { get; set; }
        private BattleGUI gui;
        Frame menuFrame = new Frame("border");

        public override void Setup(ContentManager content)
        {
        }


        public void DisplayText(String text)
        {
        }

        public void ClearText()
        {
            DisplayText("");
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
        }
    }
}
