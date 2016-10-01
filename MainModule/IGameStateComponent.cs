namespace MainModule
{
    public interface IGameStateComponent
    {
        void Move(int spriteId, Direction direction);
        void Turn(int spriteId, Direction direction);
    }
}