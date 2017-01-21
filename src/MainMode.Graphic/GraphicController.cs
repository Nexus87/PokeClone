using System;
using System.Collections.Generic;
using GameEngine.Entities;
using GameEngine.Graphics;
using GameEngine.Graphics.Textures;
using GameEngine.TypeRegistry;
using MainMode.Globals;
using Microsoft.Xna.Framework;

namespace MainMode.Graphic
{
    [GameService(typeof(GraphicController))]
    public class GraphicController : IGameEntity
    {
        private readonly Scene _scene;
        private readonly Dictionary<SpriteId, SpriteControllerEntity> _sprites = new Dictionary<SpriteId, SpriteControllerEntity>();

        public GraphicController(Scene scene)
        {
            _scene = scene;
        }

        public void SetMapTexture(ITexture2D texture2D)
        {
            _scene.SetBackground(texture2D);
        }

        public void AddSpriteEntity(SpriteId spriteId, SpriteControllerEntity controllerEntity, Point location)
        {
            _sprites[spriteId] = controllerEntity;
            _scene.AddSprite(controllerEntity.Sprite);
            controllerEntity.PlaceAt(location);
        }

        public void MoveSprite(SpriteId spriteId, Point target, Action callback)
        {
            _sprites[spriteId].MoveToDirection(target, callback);
        }

        public void TurnSprite(SpriteId spriteId, Direction direction)
        {
            _sprites[spriteId].TurnToDirection(direction);
        }

        public void ClearScreen()
        {
            foreach (var spritesValue in _sprites.Values)
            {
                _scene.RemoveSprite(spritesValue.Sprite);
            }
            _scene.MoveSceneTo(Vector2.Zero);
            _sprites.Clear();
        }

        public void Update(GameTime time)
        {
            foreach (var spritesValue in _sprites.Values)
            {
                spritesValue.Update(time);
            }
        }
    }
}