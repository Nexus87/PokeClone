using MainMode.Entities;
using MainMode.Globals;
using MainMode.Graphic;

namespace MainMode.Core
{
    public static class Connector
    {
        public static void Connect(GameStateEntity gameStateEntity, GraphicController graphicController)
        {

            gameStateEntity.SpriteTurned +=
                (sender, args) => graphicController.TurnSprite(args.SpriteId, args.Direction);

            gameStateEntity.SpritePositionChanged += (sender, args) =>
            {
                graphicController.MoveSprite(
                    args.SpriteId,
                    args.Position * Constants.Size,
                    () => gameStateEntity.UnlockSprite(args.SpriteId));
            };
        }
    }
}