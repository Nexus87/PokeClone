namespace MainModule
{
    public interface IGameStateComponent
    {
        void PlaceSprite(int spriteId, FieldCoordinate fieldCoordinate);
        void Move(int spriteId, Direction direction);
        void Turn(int spriteId, Direction direction);
        void SetMap(Map map);
    }
}