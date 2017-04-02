using System;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Loader;
using GameEngine.TypeRegistry;

namespace GameEngine.Core
{
    public class GuiSystem
    {
        public ClassicSkin ClassicSkin { get; } = new ClassicSkin();

        public void AddGuiElement(string componentName, Func<IGameTypeRegistry, ScreenConstants, IBuilder> factory)
        {
            GuiLoader.AddBuilder(componentName, factory);
        }
    }
}
