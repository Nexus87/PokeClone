using System;
using System.Collections.Generic;

namespace GameEngine.Graphics.NewGUI
{
    public interface IGraphicComponent : IArea
    {
        event EventHandler<UpdateRequestedArgs> UpdateRequested;

        IGraphicComponent Parent { get; }
        IEnumerable<IGraphicComponent> Children { get; }

        IDrawable Drawable { get; }
        void Update();
    }

    public class UpdateRequestedArgs : EventArgs
    {
        public UpdateRequestedArgs(IGraphicComponent requestingComponent)
        {
            RequestingComponent = requestingComponent;
        }

        public IGraphicComponent RequestingComponent { get; private set; }
    }
}