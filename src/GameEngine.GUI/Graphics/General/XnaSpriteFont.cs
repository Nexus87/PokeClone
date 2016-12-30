using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.GUI.Graphics.General
{
    /// <summary>
    /// Wrapper for XNAs SpriteFont class
    /// </summary>
    public sealed class XnaSpriteFont : ISpriteFont
    {
        private readonly string _fontName;
        private readonly ContentManager _content;

        public XnaSpriteFont(string fontName, ContentManager content)
        {
            _fontName = fontName;
            _content = content;
        }

        /// <see cref="ISpriteFont.SpriteFont"/>
        public SpriteFont SpriteFont => Font;

        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteFont.Characters"/>
        public SpriteFont Font { get; set; }
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteFont.Characters"/>
        public ReadOnlyCollection<char> Characters => Font.Characters;

        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteFont.DefaultCharacter"/>
        public char? DefaultCharacter { get { return Font.DefaultCharacter; }  set { Font.DefaultCharacter = value; } }
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteFont.LineSpacing"/>
        public int LineSpacing { get { return Font.LineSpacing; }  set { Font.LineSpacing = value; } }
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteFont.Spacing"/>
        public float Spacing { get { return Font.Spacing; }  set { Font.Spacing = value; } }
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteFont.Texture"/>
        public Texture2D Texture => Font.Texture;

        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteFont.MeasureString(string)"/>
        public Vector2 MeasureString(string text) { return Font.MeasureString(text); }
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteFont.MeasureString(StringBuilder)"/>
        public Vector2 MeasureString(StringBuilder text) { return Font.MeasureString(text); }

        /// <see cref="ISpriteFont.LoadContent"/>
        public void LoadContent()
        {
            Font = _content.Load<SpriteFont>(_fontName);
        }
    }
}
