using GameEngine;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BattleLib.GraphicComponents
{
    public class MessageBox : Frame
    {
        TextBox textBox = new TextBox("MenuFont");
        public String Text { set { textBox.Text = value; } }

        public MessageBox() : base("border")
        {
            textBox.Text = "012345678901234567890123456789012345678901234567890123456789012345678901234567890";
            Layout.AddComponent(textBox);
            SetMargins(90, 90, 80, 80);
        }
    }
}
