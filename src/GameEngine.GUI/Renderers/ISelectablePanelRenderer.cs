using GameEngine.GUI.Graphics;
using GameEngine.GUI.Panels;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Renderers
{
    public interface ISelectablePanelRenderer : IRenderer<SelectablePanel>
    {
        Rectangle GetContentArea(SelectablePanel selectablePanel);
    }
}