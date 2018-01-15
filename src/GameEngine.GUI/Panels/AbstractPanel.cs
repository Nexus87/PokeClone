namespace GameEngine.GUI.Panels
{
    public class AbstractPanel : AbstractGuiComponent
    {

        protected void AddChild(IGuiComponent child)
        {
            if(child == null)
                return;

            Children.Add(child);
            child.Parent = this;
            child.ComponentSelected += ComponentSelectedHandler;
        }

        protected void RemoveChild(IGuiComponent child)
        {
            if(child == null)
                return;

            Children.Remove(child);
            child.Parent = null;
            child.ComponentSelected -= ComponentSelectedHandler;
        }
        protected void ComponentSelectedHandler(object obj, ComponentSelectedEventArgs args)
        {
            OnComponentSelected(args);
        }
    }
}