using GameEngine.Graphics.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    public class GUIManager : IInputHandler
    {
        public event EventHandler GUIClose = delegate { };

        IInputHandler handler;

        LinkedList<IWidget> visibleWidgets = new LinkedList<IWidget>();
        LinkedList<IWidget> invisibleWidgets = new LinkedList<IWidget>();

        public IWidget StartupWidget { private get; set; }
        public void HandleInput(Keys key)
        {
            handler.HandleInput(key);
        }


        internal void Show()
        {
            if (StartupWidget == null)
                return;

            foreach (var widget in visibleWidgets)
                widget.IsVisible = false;

            StartupWidget.IsVisible = true;
            widget_GetFocus(StartupWidget, null);
        }
        
        public void AddWidget(IWidget widget)
        {
            widget.VisiblityChanged += widget_VisiblityChanged;
            widget.GetFocus += widget_GetFocus;
            if (widget.IsVisible)
            {
                visibleWidgets.AddLast(widget);
                if (handler == null)
                    handler = widget;
            }
            else
                invisibleWidgets.AddLast(widget);
        }

        private void widget_VisiblityChanged(object sender, VisibiltyChangeArgs e)
        {
            var widget = (IWidget)sender;
            
            if (widget.IsVisible)
            {
                invisibleWidgets.Remove(widget);
                visibleWidgets.AddFirst(widget);
                return;
            }

            visibleWidgets.Remove(widget);
            invisibleWidgets.AddLast(widget);

            if (visibleWidgets.Count == 0)
                GUIClose(this, null);
        }

        private void widget_GetFocus(object sender, EventArgs e)
        {
            var widget = (IWidget)sender;
            if (!widget.IsVisible)
                throw new InvalidOperationException("Invisible widget can't have the focus");

            visibleWidgets.Remove(widget);
            visibleWidgets.AddFirst(widget);
            handler = widget;
        }

        internal void Draw(GameTime time, SpriteBatch batch)
        {
            foreach (var widget in visibleWidgets)
                widget.Draw(time, batch);
        }
    }
}
