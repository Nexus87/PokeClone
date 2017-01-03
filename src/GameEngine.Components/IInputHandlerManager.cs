using GameEngine.Globals;

namespace GameEngine.Components
{
    public interface IInputHandlerManager
    {
        void AddHandler(IInputHandler handler, bool exclusiveHandler = false);
        void RemoveHandler(IInputHandler handler);
        void ClearHandlers();
    }
}