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
            var attackModel = new AttackMenuModel();
            var menu = new MenuComponent();
            var menuGraphics = new MenuGraphics();

            menuGraphics.Add(MenuType.Main, new MainMenuState(model));
            menuGraphics.Add(MenuType.Attack, new AttackMenu(attackModel));

            menu.AddModel(attackModel);
            menu.AddModel(model);
            menu.SetMenu(MenuType.Main);

            menu.OnMenuChanged += menuGraphics.OnMenuChange;
            var graphic = new BattleGraphics(engine);

            graphic.MessageBox = new MessageBox();
            graphic.Menu = menuGraphics;

            var input = new InputComponent(menu, engine);
            engine.setGraphicCompomnent(graphic);
            engine.AddComponent(input);
            using (var game = new Game1())
                engine.Run();
        }
    }
#endif
}
