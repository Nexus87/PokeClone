using System.Collections.Generic;
using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Actions;
using GameEngine.Core.ECS.Entities;
using GameEngine.Core.ECS.Systems;
using GameEngine.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Core.GameStates
{
    public abstract class State
    {
        private InputSystem InputSystem { get; }
        private GuiSystem GuiSystem { get; }
        private RenderSystem RenderSystem { get; }
        protected IMessageBus MessageBus { get; set; }

        private readonly EntityManager _entityManager;
        protected Entity StateEntity { get; private set; }

        protected State()
        {
            RenderSystem = new RenderSystem();
            InputSystem = new InputSystem();
            GuiSystem = new GuiSystem();
            _entityManager = new EntityManager();
            MessageBus = new MessageBus(_entityManager);

            MessageBus.RegisterForAction<TimeAction>(RenderSystem.Render);
            MessageBus.RegisterForAction<TimeAction>(InputSystem.Update);
            MessageBus.RegisterForAction<TimeAction>(GuiSystem.Update);
        }

        protected abstract void Init();

        public void Init(Screen screen, IReadOnlyDictionary<Keys, CommandKeys> keyMap)
        {
            StateEntity = GameStateEntity.Create(_entityManager, screen, keyMap);
            Init();
        }

        public void Update(GameTime gameTime)
        {
           MessageBus.SendAction(new TimeAction{Time = gameTime});
            MessageBus.StartProcess();
        }

        public abstract void Pause();
        public abstract void Resume();
        public abstract void Stop();
    }
}