using GameEngine.Graphics;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Util
{
    public class DrawnObject
    {
        public Vector2 Position;
        public Vector2 Size;

        public Color Color;
        public bool IsInConstraints(float X, float Y, float Width, float Height)
        {
            float realWidth = Math.Max(0, Width);
            float realHeight = Math.Max(0, Height);
            bool ret = true;
            ret &= (realWidth.CompareTo(0) == 0) || (Size.X.CompareTo(0) == 0) || 
                (Position.X.CompareTo(X) >= 0 && Position.X.CompareTo(X + realWidth) <= 0);
            Assert.IsTrue(ret);
            ret &= (realHeight.CompareTo(0) == 0) || (Size.Y.CompareTo(0) == 0) || 
                (Position.Y.CompareTo(Y) >= 0 && Position.Y.CompareTo(Y + realHeight) <= 0);
            Assert.IsTrue(ret);

            ret &= Size.X.CompareTo(realWidth) <= 0;
            Assert.IsTrue(ret);
            ret &= Size.Y.CompareTo(realHeight) <= 0;
            Assert.IsTrue(ret);

            return ret;
        }

        public bool IsInConstraints(IGraphicComponent component)
        {
            return IsInConstraints(component.XPosition, component.YPosition, component.Width, component.Height);
        }
    }

    public class SpriteBatchMock : ISpriteBatch
    {
        public readonly List<DrawnObject> Objects = new List<DrawnObject>();
        public readonly LinkedList<string> DrawnStrings = new LinkedList<string>();

        private void SetData(Vector2 position, Vector2 size, Color color)
        {
            SetData(position, size, Vector2.One, color);
        }

        private void SetData(Vector2 position, Texture2D texture, Color color)
        {
            SetData(position, texture, Vector2.One, color);   
        }

        private void SetData(Vector2 position, Texture2D texture, Vector2 scale, Color color)
        {
            SetData(position, texture.Bounds.Size.ToVector2(), scale, color);
        }

        private void SetData(Vector2 position, Vector2 size, Vector2 scale, Color color)
        {
            var obj = new DrawnObject();
            obj.Position = position;
            obj.Size = size * scale;
            obj.Color = color != null ? color : Color.Black;
            Objects.Add(obj);
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

        public GraphicsDevice GraphicsDevice
        {
            get
            {
                var dev = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.Reach, new PresentationParameters());
                return dev;
            }
        }

        public void Begin(SpriteSortMode sortMode = SpriteSortMode.Deferred, BlendState blendState = null, SamplerState samplerState = null, DepthStencilState depthStencilState = null, RasterizerState rasterizerState = null, Effect effect = null, Matrix? transformMatrix = null)
        {
            throw new NotImplementedException();
        }

        public void Draw(Texture2D texture, Rectangle destinationRectangle, Color color)
        {
            SetData(destinationRectangle.Location.ToVector2(), destinationRectangle.Size.ToVector2(), color);
        }

        public void Draw(Texture2D texture, Vector2 position, Color color)
        {
            SetData(position, texture, color);
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
            SetData(position, texture, new Vector2(scale), color);
        }

        public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            SetData(position, texture, scale, color);
        }

        public void Draw(Texture2D texture, Vector2? position = null, Rectangle? destinationRectangle = null, Rectangle? sourceRectangle = null, Vector2? origin = null, float rotation = 0f, Vector2? scale = null, Color? color = null, SpriteEffects effects = SpriteEffects.None, float layerDepth = 0f)
        {
            var scaling = scale != null ? scale.Value : Vector2.One;
            Color realColor = color.HasValue ? color.Value : Color.Black;
            if (position == null && destinationRectangle != null)
                SetData(destinationRectangle.Value.Location.ToVector2(), destinationRectangle.Value.Size.ToVector2(), scaling, realColor);
            else if (destinationRectangle == null && position != null)
                SetData(position.Value, texture, scaling, realColor);
            else
                throw new InvalidOperationException("Either position or destinationRectangle must be not null");

        }

        public void DrawString(ISpriteFont spriteFont, string text, Vector2 position, Color color)
        {
            SetData(position, spriteFont, text, color);
        }

        public void DrawString(ISpriteFont spriteFont, StringBuilder text, Vector2 position, Color color)
        {
            SetData(position, spriteFont, text.ToString(), color);
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

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
