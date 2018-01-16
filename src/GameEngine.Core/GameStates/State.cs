using System.Collections.Generic;
using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Actions;
using GameEngine.Core.ECS.Entities;
using GameEngine.Core.ECS.Systems;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Loader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Core.GameStates
{
    public abstract class State
    {
        private readonly EntityManager _entityManager;

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

        private InputSystem InputSystem { get; }
        protected GuiSystem GuiSystem { get; }
        private RenderSystem RenderSystem { get; }
        protected IMessageBus MessageBus { get; set; }
        protected Entity StateEntity { get; private set; }
        protected Screen Screen { get; private set; }

        protected ISkin Skin { get; private set; }

        protected abstract void Init();

        public void Init(Screen screen, IReadOnlyDictionary<Keys, CommandKeys> keyMap, ISkin skin)
        {
            StateEntity = GameStateEntity.Create(_entityManager, screen, keyMap);
            //TODO
            //GuiSystem.GuiLoader = new GuiLoader(Screen.Constants);
            Screen = screen;
            Skin = skin;
            Init();
        }

        public void Update(GameTime gameTime)
        {
            MessageBus.SendAction(new TimeAction {Time = gameTime});
            MessageBus.StartProcess();
        }

        public abstract void Pause();
        public abstract void Resume();
        public abstract void Stop();
    }
}