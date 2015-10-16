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
            
            var mainModel = new MainMenuModel();
            var attackModel = new AttackMenuModel();
            var itemModel = new ItemMenuModel();

            var mainMenuView = new MainMenuState(mainModel);
            var attackMenuView = new AttackMenu(attackModel);
            var itemMenuView = new ItemMenuState(itemModel);

            var menuComponent = new MenuComponent();
            var menuGraphics = new MenuGraphics();

            menuGraphics.Add(MenuType.Main, mainMenuView);
            menuGraphics.Add(MenuType.Attack, attackMenuView);
            menuGraphics.Add(MenuType.Item, itemMenuView);

            menuComponent.AddModel(attackModel);
            menuComponent.AddModel(mainModel);
            menuComponent.AddModel(itemModel);

            menuComponent.SetMenu(MenuType.Main);

            menuComponent.OnMenuChanged += menuGraphics.OnMenuChange;

            var graphic = new BattleGraphics(engine);

            graphic.MessageBox = new MessageBox();
            graphic.Menu = menuGraphics;

            var input = new InputComponent(menuComponent, engine);
            engine.setGraphicCompomnent(graphic);
            engine.AddComponent(input);
            using (var game = new Game1())
                engine.Run();
        }
    }
#endif
}
