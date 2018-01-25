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

            var inputSystem = new InputSystem();
            var guiSystem = new GuiSystem();

            BlockingQueueingSystem.RegisterHandler(MessageBus);
            guiSystem.RegisterHandler(MessageBus);
            inputSystem.RegisterHandler(MessageBus);
            RenderSystem.RegisterHandlers(MessageBus);
        }

        protected IMessageBus MessageBus { get; }

        protected GuiFactory GuiFactory { get; private set;}
        protected ScreenConstants ScreenConstants { get; private set; }
        protected ITextureProvider TextureProvider { get; private set; }
        private Entity StateEntity { get; set; }

        protected abstract void Init();

        public void Init(Screen screen, IReadOnlyDictionary<Keys, CommandKeys> keyMap, ISkin skin, GuiFactory factory, ITextureProvider textureProvider)
        {
            TextureProvider = textureProvider;
            StateEntity = GameStateEntity.Create(EntityManager, screen, keyMap, skin);
            GuiFactory = factory;
            ScreenConstants = screen.Constants;
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