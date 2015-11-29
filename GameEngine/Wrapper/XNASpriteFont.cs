using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.Wrapper
{

    public class XNASpriteFont : ISpriteFont
    {
        public SpriteFont SpriteFont { get { return Font; } }
        public SpriteFont Font { get; set; }
        public ReadOnlyCollection<char> Characters { get { return Font.Characters; } }
        public char? DefaultCharacter { get { return Font.DefaultCharacter; }  set { Font.DefaultCharacter = value; } }
        public int LineSpacing { get { return Font.LineSpacing; }  set { Font.LineSpacing = value; } }
        public float Spacing { get { return Font.Spacing; }  set { Font.Spacing = value; } }
        public Texture2D Texture { get { return Font.Texture; } }
        public Vector2 MeasureString(string text) { return Font.MeasureString(text); }
        public Vector2 MeasureString(StringBuilder text) { return Font.MeasureString(text); }

        public void Load(ContentManager content, string fontName)
        {
            Font = content.Load<SpriteFont>(fontName);
        }

        public static implicit operator XNASpriteFont( SpriteFont font)
        {
            return new XNASpriteFont { Font = font };
        }

        public static implicit operator SpriteFont(XNASpriteFont font)
        {
            return font.SpriteFont;
        }
    }
}
