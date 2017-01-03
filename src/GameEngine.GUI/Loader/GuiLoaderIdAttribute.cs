using System;

namespace GameEngine.GUI.Loader
{
    [AttributeUsage(AttributeTargets.Field)]
    public class GuiLoaderIdAttribute : Attribute
    {
        public string Name { get; }

        public GuiLoaderIdAttribute(string name)
        {
            Name = name;
        }
    }
}