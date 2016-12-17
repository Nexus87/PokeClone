using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.GUI.Graphics.General
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
        /// Loads the SpriteFont.
        /// <remarks>
        /// Only after calling this function, it can be ensured, that the other methods return
        /// sensible values.
        /// Should only be called after the LoadContent phase of the PokeEngine. Calling it
        /// in the Setup method of IGraphicComponents is safe.
        /// </remarks>
        /// </summary>
        void LoadContent();
    }
}
