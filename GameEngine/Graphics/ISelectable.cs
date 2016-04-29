
namespace GameEngine.Graphics
{
    public interface ISelectable
    {
        bool IsSelected { get; }
        void Select();
        void Unselect();
    }
}
