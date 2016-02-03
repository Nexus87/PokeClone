using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics.Layouts
{
    public interface ILayout
    {
        void Init(IGraphicComponent component);
        void AddComponent(IGraphicComponent component);
        void RemoveComponent(IGraphicComponent component);

        void SetMargin(int left = 0, int right = 0, int top = 0, int bottom = 0);
        void Setup(ContentManager content);
        void Draw(GameTime time, ISpriteBatch batch);

        int Rows { get; }
        int Columns { get; }
        void ForceUpdateComponents();
    }
}
