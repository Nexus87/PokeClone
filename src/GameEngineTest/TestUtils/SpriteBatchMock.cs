using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using GameEngine.Graphics.General;
using GameEngine.Graphics.Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace GameEngineTest.TestUtils
{
    public class SpriteBatchMock : ISpriteBatch
    {
        public readonly List<DrawnObject> DrawnObjects = new List<DrawnObject>();
        public readonly LinkedList<string> DrawnStrings = new LinkedList<string>();

        private void SetData(Vector2 position, Vector2 size, Color color)
        {
            SetData(position, size, Vector2.One, color);
        }

        private void SetData(Vector2 position, ITexture2D texture, Color color)
        {
            SetData(position, texture, Vector2.One, color);   
        }

        private void SetData(Vector2 position, ITexture2D texture, Vector2 scale, Color color)
        {
            SetData(position, texture.Bounds.Size.ToVector2(), scale, color);
        }

        private void SetData(Vector2 position, Vector2 size, Vector2 scale, Color color)
        {
            var obj = new DrawnObject
            {
                Position = position,
                Size = size * scale,
                Color = color
            };
            DrawnObjects.Add(obj);
        }

        private void SetData(Vector2 position, ISpriteFont font, string text, Vector2 scale, Color color)
        {
            DrawnStrings.AddLast(text);
            SetData(position, font.MeasureString(text), scale, color);
        }

        private void SetData(Vector2 position, ISpriteFont font, string text, Color color)
        {
            SetData(position, font, text, Vector2.One, color);
        }

        public GraphicsDevice GraphicsDevice => null;

        public void Begin(SpriteSortMode sortMode = SpriteSortMode.Deferred, BlendState blendState = null, SamplerState samplerState = null, DepthStencilState depthStencilState = null, RasterizerState rasterizerState = null, Effect effect = null, Matrix? transformMatrix = null)
        {
            throw new NotImplementedException();
        }

        public void Draw(ITexture2D texture, Rectangle destinationRectangle, Color color, SpriteEffects spriteEffects = SpriteEffects.None)
        {
            SetData(destinationRectangle.Location.ToVector2(), destinationRectangle.Size.ToVector2(), color);
        }

        public void Draw(ITexture2D texture, Vector2 position, Color color)
        {
            SetData(position, texture, color);
        }

        public void Draw(ITexture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color)
        {
            Draw(texture, destinationRectangle, color);
        }

        public void Draw(ITexture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, SpriteEffects spriteEffects = SpriteEffects.None)
        {
            Draw(texture, position, color);   
        }

        public void Draw(ITexture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
        {
            Draw(texture, destinationRectangle, color);
        }

        public void Draw(ITexture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            SetData(position, texture, new Vector2(scale), color);
        }

        public void Draw(ITexture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            SetData(position, texture, scale, color);
        }

        public void DrawString(ISpriteFont spriteFont, string text, Vector2 position, Color color)
        {
            SetData(position, spriteFont, text, color);
        }

        public void DrawString(ISpriteFont spriteFont, StringBuilder text, Vector2 position, Color color)
        {
            SetData(position, spriteFont, text.ToString(), color);
        }

        public void Draw(ITexture2D texture, Vector2 location, Vector2 scaling, Color color,
            SpriteEffects spriteEffects = SpriteEffects.None)
        {
            throw new NotImplementedException();
        }

        public void DrawString(ISpriteFont spriteFont, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            SetData(position, spriteFont, text, new Vector2(scale), color);
        }

        public void DrawString(ISpriteFont spriteFont, string text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            SetData(position, spriteFont, text, scale, color);
        }

        public void DrawString(ISpriteFont spriteFont, StringBuilder text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            SetData(position, spriteFont, text.ToString(), new Vector2(scale), color);
        }

        public void DrawString(ISpriteFont spriteFont, StringBuilder text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            SetData(position, spriteFont, text.ToString(), scale, color);
        }

        public void End()
        {
            throw new NotImplementedException();
        }

        public void Draw(RenderTarget2D texture, Rectangle destinationRectangle)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            DrawnObjects.Clear();
            DrawnStrings.Clear();
        }
    }
}
