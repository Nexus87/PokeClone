namespace GameEngine.Core.GameStates
{
    public interface IState
    {
        void Init();
        void Pause();
        void Resume();
        void Stop();
    }
}
