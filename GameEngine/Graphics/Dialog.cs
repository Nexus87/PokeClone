using GameEngine.Graphics.Layouts;
using GameEngine.Graphics.Widgets;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    public class Dialog : AbstractGraphicComponent
    {
        public ILayout Layout { get; set; }
        private readonly List<IWidget> widgets = new List<IWidget>();

        public Dialog(PokeEngine game) : base(game) { }

        public void AddWidget(IWidget widget)
        {
            widgets.Add(widget);
        }

        public void RemoveWidget(IWidget widget)
        {
            widgets.Remove(widget);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            foreach (var w in widgets)
                w.Draw(time, batch);
        }

        public override void Setup(ContentManager content)
        {
            foreach (var w in widgets)
                w.Setup(content);
        }
    }
}
