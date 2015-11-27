using GameEngine.Graphics;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Util
{
    class DrawnObject
    {
        public Vector2 Position;
        public Vector2 Size;

        public bool IsInConstraints(float X, float Y, float Width, float Height)
        {
            bool ret = true;
            ret &= X.CompareTo(Position.X) >= 0 && X.CompareTo(Position.X + Size.X) <= 0;
            ret &= Y.CompareTo(Position.Y) >= 0 && Y.CompareTo(Position.Y + Size.Y) <= 0;

            ret &= Width.CompareTo(Size.X) <= 0;
            ret &= Height.CompareTo(Size.Y) <= 0;

            return ret;
        }

        public bool IsInConstraints(IGraphicComponent component)
        {
            return IsInConstraints(component.X, component.Y, component.Width, component.Height);
        }
    }

    class SpriteBatchMock : ISpriteBatch
    {
        public readonly LinkedList<DrawnObject> Objects = new LinkedList<DrawnObject>();

        private void SetData(Vector2 position, Vector2 size)
        {
            SetData(position, size, Vector2.One);
        }

        private void SetData(Vector2 position, Texture2D texture)
        {
            SetData(position, texture, Vector2.One);   
        }

        private void SetData(Vector2 position, Texture2D texture, Vector2 scale)
        {
            SetData(position, texture.Bounds.Size.ToVector2(), scale);
        }

        private void SetData(Vector2 position, Vector2 size, Vector2 scale)
        {
            var obj = new DrawnObject();
            obj.Position = position;
            obj.Size = size * scale;
        }
        public GraphicsDevice GraphicsDevice
        {
            get { throw new NotImplementedException(); }
        }

        public void Begin(SpriteSortMode sortMode = SpriteSortMode.Deferred, BlendState blendState = null, SamplerState samplerState = null, DepthStencilState depthStencilState = null, RasterizerState rasterizerState = null, Effect effect = null, Matrix? transformMatrix = null)
        {
            throw new NotImplementedException();
        }

        public void Draw(Texture2D texture, Rectangle destinationRectangle, Color color)
        {
            SetData(destinationRectangle.Location.ToVector2(), destinationRectangle.Size.ToVector2());
        }

        public void Draw(Texture2D texture, Vector2 position, Color color)
        {
            SetData(position, texture);
        }

        public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color)
        {
            Draw(texture, destinationRectangle, color);
        }

        public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color)
        {
            Draw(texture, position, color);   
        }

        public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
        {
            Draw(texture, destinationRectangle, color);
        }

        public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            SetData(position, texture, new Vector2(scale));
        }

        public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            SetData(position, texture, scale);
        }

        public void Draw(Texture2D texture, Vector2? position = null, Rectangle? destinationRectangle = null, Rectangle? sourceRectangle = null, Vector2? origin = null, float rotation = 0f, Vector2? scale = null, Color? color = null, SpriteEffects effects = SpriteEffects.None, float layerDepth = 0f)
        {
            if (position == null && destinationRectangle != null)
                SetData(destinationRectangle.Value.Location.ToVector2(), destinationRectangle.Value.Size.ToVector2(), scale != null ? scale.Value : Vector2.One);
            else if (destinationRectangle == null && position != null)
                SetData(position.Value, texture, scale != null ? scale.Value : Vector2.One);
            else
                throw new InvalidOperationException("Either position or destinationRectangle must be not null");

        }

        public void DrawString(Microsoft.Xna.Framework.Graphics.SpriteFont spriteFont, string text, Microsoft.Xna.Framework.Vector2 position, Microsoft.Xna.Framework.Color color)
        {
            throw new NotImplementedException();
        }

        public void DrawString(Microsoft.Xna.Framework.Graphics.SpriteFont spriteFont, StringBuilder text, Microsoft.Xna.Framework.Vector2 position, Microsoft.Xna.Framework.Color color)
        {
            throw new NotImplementedException();
        }

        public void DrawString(Microsoft.Xna.Framework.Graphics.SpriteFont spriteFont, string text, Microsoft.Xna.Framework.Vector2 position, Microsoft.Xna.Framework.Color color, float rotation, Microsoft.Xna.Framework.Vector2 origin, float scale, Microsoft.Xna.Framework.Graphics.SpriteEffects effects, float layerDepth)
        {
            throw new NotImplementedException();
        }

        public void DrawString(Microsoft.Xna.Framework.Graphics.SpriteFont spriteFont, string text, Microsoft.Xna.Framework.Vector2 position, Microsoft.Xna.Framework.Color color, float rotation, Microsoft.Xna.Framework.Vector2 origin, Microsoft.Xna.Framework.Vector2 scale, Microsoft.Xna.Framework.Graphics.SpriteEffects effects, float layerDepth)
        {
            throw new NotImplementedException();
        }

        public void DrawString(Microsoft.Xna.Framework.Graphics.SpriteFont spriteFont, StringBuilder text, Microsoft.Xna.Framework.Vector2 position, Microsoft.Xna.Framework.Color color, float rotation, Microsoft.Xna.Framework.Vector2 origin, float scale, Microsoft.Xna.Framework.Graphics.SpriteEffects effects, float layerDepth)
        {
            throw new NotImplementedException();
        }

        public void DrawString(SpriteFont spriteFont, StringBuilder text, Microsoft.Xna.Framework.Vector2 position, Microsoft.Xna.Framework.Color color, float rotation, Microsoft.Xna.Framework.Vector2 origin, Microsoft.Xna.Framework.Vector2 scale, Microsoft.Xna.Framework.Graphics.SpriteEffects effects, float layerDepth)
        {
            throw new NotImplementedException();
        }

        public void End()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
