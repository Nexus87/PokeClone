using System;
using Autofac;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Loader;

namespace GameEngine.Core
{
    public class GuiSystem
    {
        public ClassicSkin ClassicSkin { get; } = new ClassicSkin();

        public void AddGuiElement(string componentName, Func<IContainer, ScreenConstants, IBuilder> factory)
        {
            GuiLoader.AddBuilder(componentName, factory);
        }
    }
}
