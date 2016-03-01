﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Wrapper
{
    /// <summary>
    /// This interface represents a SpriteFont
    /// </summary>
    /// <remarks>
    /// Like ISpriteBatch, this interface is used to replace XNAs SpriteFont class
    /// which can't be mocked in tests.
    /// </remarks>
    /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteFont"/>
    public interface ISpriteFont
    {
        /// <summary>
        /// Returns an instance of Microsoft.Xna.Framework.Graphics.SpriteFont
        /// </summary>
        /// <remarks>
        /// This property is used to "convert" the class back to the XNA SpriteFont,
        /// which is need to draw it.
        /// </remarks>
        SpriteFont SpriteFont { get; }
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteFont.Characters"/>
        ReadOnlyCollection<char> Characters { get; }
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteFont.DefaultCharacter"/>
        char? DefaultCharacter { get; set; }
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteFont.LineSpacing"/>
        int LineSpacing { get; set; }
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteFont.Spacing"/>
        float Spacing { get; set; }
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteFont.Texture"/>
        Texture2D Texture { get; }
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteFont.MeasureString(StringBuilder)"/>
        Vector2 MeasureString(StringBuilder text);
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteFont.MeasureString(string)"/>
        Vector2 MeasureString(string text);

        /// <summary>
        /// Loads the font from the content manager
        /// </summary>
        /// <param name="content">Content Manager</param>
        /// <param name="fontName">Font name</param>
        void Load(ContentManager content, string fontName);
    }
}
