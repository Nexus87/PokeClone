using System.Collections.Generic;
using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Actions;
using GameEngine.Core.ECS.Entities;
using GameEngine.Core.ECS.Systems;
using GameEngine.Globals;
using GameEngine.Graphics.Textures;
using GameEngine.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Core.GameStates
{
    public abstract class State
    {
        protected readonly EntityManager EntityManager;

        protected State()
        {
            EntityManager = new EntityManager();
            MessageBus = new MessageBus(EntityManager);

            RenderSystem = new RenderSystem();
            InputSystem = new InputSystem(MessageBus);
            GuiSystem = new GuiSystem();

            MessageBus.RegisterForAction<TimeAction>(RenderSystem.Render);
            MessageBus.RegisterForAction<TimeAction>(InputSystem.Update);
            MessageBus.RegisterForAction<TimeAction>(GuiSystem.Update);
            MessageBus.RegisterForAction<SetGuiComponentVisibleAction>(GuiSystem.SetWidgetVisibility);
            MessageBus.RegisterForAction<GuiKeyInputAction>(GuiSystem.HandleInput);
            MessageBus.RegisterForAction<SetGuiVisibleAction>(GuiSystem.SetGuiVisiblity);
        }

        private InputSystem InputSystem { get; }
        protected GuiSystem GuiSystem { get; }
        private RenderSystem RenderSystem { get; }
        protected IMessageBus MessageBus { get; }
        protected ITextureProvider TextureProvider { get; private set; }
        private Entity StateEntity { get; set; }

        protected abstract void Init();

        public void Init(Screen screen, IReadOnlyDictionary<Keys, CommandKeys> keyMap, ISkin skin, GuiFactory factory, ITextureProvider textureProvider)
        {
            TextureProvider = textureProvider;
            StateEntity = GameStateEntity.Create(EntityManager, screen, keyMap, skin);
            GuiSystem.Factory = factory;
            Init();
        }

        public void Update(GameTime gameTime)
        {
            MessageBus.SendAction(new TimeAction(gameTime));
            MessageBus.StartProcess();
        }

        public abstract void Pause();
        public abstract void Resume();
        public abstract void Stop();
    }
}