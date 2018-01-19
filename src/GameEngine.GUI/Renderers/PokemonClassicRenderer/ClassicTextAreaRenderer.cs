using System;
using System.Linq;
using GameEngine.Graphics.General;
using GameEngine.Graphics.Textures;
using GameEngine.GUI.Controlls;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Renderers.PokemonClassicRenderer
{
    public class ClassicTextAreaRenderer : TextAreaRenderer
    {
        private ISpriteFont _font;

        public ClassicTextAreaRenderer()
        {
            LineSpacing = 10;
        }

        public override void Init(ITextureProvider textureProvider)
        {
            _font = textureProvider.GetFont(ClassicSkin.DefaultFont);
        }

        protected override void RenderComponent(ISpriteBatch spriteBatch, TextArea component)
        {
            var lineHeight = GetLineHeight(component);
            var offset = new Vector2(0, component.CurrentLineIndex * lineHeight);
            foreach (var line in component.Lines)
            {
                RenderText(spriteBatch, _font, line.Text, line.Position + offset, component.TextHeight);
            }
        }

        public override int CharsPerLine(TextArea textArea)
        {
            return textArea.Area.Width / textArea.TextHeight;
        }

        public override int GetLineHeight(TextArea textArea)
        {
            return textArea.TextHeight + LineSpacing;
        }

        protected override void UpdateComponent(TextArea component)
        {
            component._allLines = component._splitter
                .SplitText(CharsPerLine(component), component.Text)
                .Select(x => new TextAreaLine { Text = x })
                .ToList();

            var lineHeight = GetLineHeight(component);
            for (var i = 0; i < component._allLines.Count; i++)
            {
                component._allLines[i].Position = new Vector2(component.Area.X, component.Area.Y + +lineHeight);
                i++;
            }
            component.CurrentLineIndex = Math.Min(component.CurrentLineIndex, component._allLines.Count - 1);
        }
    }
}