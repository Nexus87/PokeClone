using GameEngine.GUI.Graphics;

namespace GameEngine.GUI.Panels
{
    public class ScrollArea : AbstractGraphicComponent
    {
        public bool Autoscrolling { get; set; }
        public void SetContent(IGraphicComponent component)
        {
            children.Clear();
            children.Add(component);
            component.Parent = this;


        }


    }
}