using System.Collections.Generic;
using GameEngine.GUI.Renderers;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI
{
    public interface IGraphicComponent : IArea
    {
        bool IsSelectable { get; }
        IGraphicComponent Parent { get; set; }
        IEnumerable<IGraphicComponent> Children { get; }

        IRenderer Renderer { get; }
        bool IsSelected { get; set; }
        void Update(GameTime time);
        void HandleKeyInput(CommandKeys key);
        void Init();
    }

}