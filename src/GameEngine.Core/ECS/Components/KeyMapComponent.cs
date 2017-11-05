using System;
using System.Collections.Generic;
using GameEngine.Globals;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Core.ECS.Components
{
    public class KeyMapComponent : Component
    {
        public IReadOnlyDictionary<Keys, CommandKeys> KeyMap { get; set; }

        public KeyMapComponent(Guid entityId, IReadOnlyDictionary<Keys, CommandKeys> keyMap) : base(entityId)
        {
            KeyMap = keyMap;
        }
    }
}