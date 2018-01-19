using System;
using System.Collections.Generic;
using GameEngine.Core;
using GameEngine.Globals;
using GameEngine.Graphics.General;
using GameEngine.GUI;
using Microsoft.Xna.Framework;

namespace GameEngineTest.TestUtils
{
    public class GraphicComponentMock : IGuiComponent
    {
        public bool WasDrawn { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public IEngineInterface Game { get; set; }

        public event EventHandler<GraphicComponentSizeChangedEventArgs> PreferredSizeChanged;

        public void RaisePreferredSizeChanged()
        {
            PreferredSizeChanged?.Invoke(this, new GraphicComponentSizeChangedEventArgs(this, 0, 0));
        }

        public Action DrawCallback = null;
        public void Draw(GameTime time, ISpriteBatch batch)
        {
            WasDrawn = true;
            DrawCallback?.Invoke();

            if (batch is SpriteBatchMock)
            {
                batch.Draw(null, Area, Color);
            }
        }


        public Color Color { get; set; }

        public float PreferredHeight { get; set; }

        public float PreferredWidth { get; set; }

        public Rectangle ScissorArea { get; set; }

        public Rectangle Area { get; set; }

        public IGuiComponent Parent { get; set; }
        public List<IGuiComponent> Children { get; } = new List<IGuiComponent>();
        public bool IsSelected { get; set; }
        public bool IsSelectable { get; set; }
        public bool NeedsUpdate { get; set; }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public virtual void HandleKeyInput(CommandKeys key)
        {
            throw new NotImplementedException();
        }



        public void RaiseSizeChanged()
        {
        }


        public event EventHandler<ComponentSelectedEventArgs> ComponentSelected;

        public virtual void OnComponentSelected(ComponentSelectedEventArgs e)
        {
            ComponentSelected?.Invoke(this, e);
        }
    }
}
