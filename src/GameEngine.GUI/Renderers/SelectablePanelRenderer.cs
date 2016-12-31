using GameEngine.GUI.Panels;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Renderers
{
    public abstract class SelectablePanelRenderer : AbstractRenderer<SelectablePanel>
    {
        public abstract Rectangle GetContentArea(SelectablePanel selectablePanel);
    }
}