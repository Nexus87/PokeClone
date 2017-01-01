using GameEngine.Globals;

namespace GameEngine.Core.GameEngineComponents
{
    public interface IInputHandlerManager
    {
        void AddHandler(IInputHandler handler, bool exclusiveHandler = false);
        void RemoveHandler(IInputHandler handler);
        void ClearHandlers();
    }
}