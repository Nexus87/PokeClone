using System.Collections.Generic;
using GameEngine.Core.ECS.Components;
using GameEngine.Globals;
using GameEngine.GUI;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Core.ECS.Entities
{
    public class GameStateEntity
    {
        public static Entity Create(IEntityManager entityManager, IScreen screen, IReadOnlyDictionary<Keys, CommandKeys> keyMap, ISkin skin)
        {
            var entity = new Entity();
            entityManager.AddComponent(new RenderAreaComponent(entity.Id, screen));
            entityManager.AddComponent(new GuiComponent(entity.Id, screen, skin));
            entityManager.AddComponent(new KeyMapComponent(entity.Id, keyMap));

            return entity;
        }
    }
}