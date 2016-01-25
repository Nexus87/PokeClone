using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.Basic;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BattleLib.GraphicComponents
{
    internal class HPLine : AbstractGraphicComponent
    {
        private const int border = 10;
        private int currentHp = 0;
        private Line hpLine = new Line();
        private Line innerLine = new Line();
        private int maxHp = 0;
        private Line outerLine = new Line();

        public HPLine()
        {
            outerLine.Color = Color.Black;
            innerLine.Color = Engine.BackgroundColor;
        }

        public int Current { get { return currentHp; } set { currentHp = value; Invalidate(); } }
        public int MaxHP { get { return maxHp; } set { maxHp = value; Invalidate(); } }

        public override void Setup(ContentManager content)
        {
            outerLine.Setup(content);
            innerLine.Setup(content);
            hpLine.Setup(content);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            outerLine.Draw(time, batch);
            innerLine.Draw(time, batch);
        }

        protected override void Update()
        {
            outerLine.X = X;
            outerLine.Y = Y;
            outerLine.Width = Width;
            outerLine.Height = Height;

            innerLine.X = X + border;
            innerLine.Y = Y + border;
            innerLine.Width = Width - 2 * border;
            innerLine.Height = Height - 2 * border;

            float factor = ((float)currentHp) / ((float)maxHp);

            hpLine.X = X + border;
            hpLine.Y = Y + border;
            hpLine.Width = factor * (Width - 2 * border);
            hpLine.Height = Height - 2 * border;

            if (factor >= 0.50f)
                hpLine.Color = Color.Green;
            else if (factor >= 0.25f)
                hpLine.Color = Color.Yellow;
            else
                hpLine.Color = Color.Red;
        }
    }
}