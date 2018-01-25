using System;
using GameEngine.Core.ECS;

namespace BattleMode.Core.Actions
{
    public class HpChangeFinishedAction
    {
        public readonly Guid Entity;
        public HpChangeFinishedAction(Guid entity)
        {
            Entity = entity;
        }
    }
}