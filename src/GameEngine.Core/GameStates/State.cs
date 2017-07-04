using System.Collections.Generic;
using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Systems;
using GameEngine.Graphics.General;
using GameEngine.GUI;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Core.GameStates
{
    public abstract class State
    {
        public ScreenState ScreenState { get; set; }
        public EntityManager EntityManager{ get; }
        public MessagingSystem MessagingSystem { get; }

        protected List<ISystem> Systems { get; } = new List<ISystem>();

        protected State() : this(new EntityManager(), new MessagingSystem())
        {
            
        }

        protected State(EntityManager entityManager, MessagingSystem messagingSystem)
        {
            EntityManager = entityManager;
            MessagingSystem = messagingSystem;
        }

        protected virtual void InitDefault()
        {
            Systems.Add(new GuiSystem(ScreenState));
            Systems.Add(new RenderSystem(ScreenState, EntityManager));
        }

        protected abstract void InitCustom();

        public void Init()
        {
            InitDefault();
            InitCustom();
            Systems.ForEach(x => x.Init(MessagingSystem));
        }

        public abstract void Pause();
        public abstract void Resume();
        public abstract void Stop();
    }

    public class ScreenState
    {
        public RenderTarget2D Scene { get; set; }
        public RenderTarget2D Gui { get; set; }
        public GuiManager GuiManager { get; set; } = new GuiManager();
        public bool IsGuiVisible { get; set; }
        public ISpriteBatch SpriteBatch { get; set; }
    }
}