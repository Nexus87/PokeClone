using System;
using System.Collections.Generic;

namespace GameEngine.Core.ECS.Components
{
    public class ActionQueueComponent : Component
    {
        public ActionQueueComponent(Guid entityId) : base(entityId)
        {
        }

        public bool IsBlocked { get; set; }
        public Queue<object> Actions {get;} = new Queue<object>();
    }
}