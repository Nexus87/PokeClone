﻿using MainMode.Entities;
using MainMode.Globals;
using MainMode.Graphic;
using Microsoft.Xna.Framework;

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
                    new Point(args.NewX, args.NewY) * Constants.Size,
                    () => gameStateEntity.UnlockSprite(args.SpriteId));
            };
        }
    }
}