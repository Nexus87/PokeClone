using Microsoft.Xna.Framework;
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
    public interface ISpriteFont
    {
        SpriteFont SpriteFont { get; }
        ReadOnlyCollection<char> Characters { get; }
        char? DefaultCharacter { get; set; }
        int LineSpacing { get; set; }
        float Spacing { get; set; }
        Texture2D Texture { get; }
        Vector2 MeasureString(StringBuilder text);
        Vector2 MeasureString(string text);

        void Load(ContentManager content, string fontName);
    }
}
