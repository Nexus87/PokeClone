using System;
using GameEngine;
using BattleLib.GraphicComponent;
using BattleLib.Components;

namespace PokemonGame
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var engine = new Engine();
            var model = new MainMenuModel();
            var menu = new MenuComponent();
            menu.AddModel(model);
            menu.SetMenu(MenuType.Main);
            var graphic = new BattleGraphics(model, engine);
            var input = new InputComponent(menu, engine);
            engine.setGraphicCompomnent(graphic);
            engine.AddComponent(input);
            using (var game = new Game1())
                engine.Run();
        }
    }
#endif
}
