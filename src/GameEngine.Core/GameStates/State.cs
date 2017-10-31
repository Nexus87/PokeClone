using System;
using System.Collections.Generic;
using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Systems;
using GameEngine.Globals;
using GameEngine.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Core.GameStates
{
    public abstract class State
    {
        private InputSystem InputSystem { get; }
        private GuiSystem GuiSystem { get; }
        private RenderSystem RenderSystem { get; }

        protected readonly IObservable<bool> ShowGuiStream;
        public Action<GameTime> Update;
        private Screen _screen;
        public ScreenState ScreenState { get; set; }
        private readonly EntityManager _entityManager;

        protected State(IReadOnlyDictionary<Keys, CommandKeys> keyMap, ISkin skin)
        {
            InputSystem = new InputSystem(keyMap);
            GuiSystem = new GuiSystem(skin);
            RenderSystem = new RenderSystem();
            _entityManager = new EntityManager();
        }

        protected virtual void InitDefault()
        {
        }

        protected abstract void InitCustom();

        public void Init(Screen screen)
        {
            _screen = screen;
            InitDefault();
            InitCustom();
        }

        public abstract void Pause();
        public abstract void Resume();
        public abstract void Stop();
    }

    public class ScreenState
    {
        public bool IsGuiVisible { get; set; }
    }
}