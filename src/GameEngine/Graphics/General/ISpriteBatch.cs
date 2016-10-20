using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics.General
{
    /// <summary>
    /// This interface represents a SpriteBatch, that is used for drawing 2D graphics
    /// </summary>
    /// <remarks>
    /// The purpose of this interface is to replace the XNA SpriteBatch class by this interface.
    /// This way it is easier to mock this class in tests.
    /// For the same reason SpriteFont was replace with ISpritFont interface in the DrawString
    /// methods.
    /// </remarks>
    /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/>
    public interface ISpriteBatch : IDisposable
    {
        /// <see cref="Microsoft.Xna.Framework.Graphics.GraphicsResource.GraphicsDevice"/>
        GraphicsDevice GraphicsDevice { get; }
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch.Begin"/>
        void Begin(SpriteSortMode sortMode = SpriteSortMode.Deferred, BlendState blendState = null, SamplerState samplerState = null, DepthStencilState depthStencilState = null, RasterizerState rasterizerState = null, Effect effect = null, Matrix? transformMatrix = null);
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch.Draw(Texture2D, Rectangle, Color)"/>
        void Draw(ITexture2D texture, Rectangle destinationRectangle, Color color);
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch.Draw(Texture2D, Vector2?, Rectangle?, Rectangle?, Vector2?, float, Vector2?, Color?, SpriteEffects, float)"/>
        void Draw(ITexture2D texture, Vector2? position = null, Rectangle? destinationRectangle = null, Rectangle? sourceRectangle = null, Vector2? origin = null, float rotation = 0f, Vector2? scale = null, Color? color = null, SpriteEffects effects = SpriteEffects.None, float layerDepth = 0f);
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch.DrawString(SpriteFont, string, Vector2, Color, float, Vector2, float, SpriteEffects, float)"/>
        void DrawString(ISpriteFont spriteFont, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth);
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch.End"/>
        void End();
    }
}
