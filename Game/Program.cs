using System;
using GameEngine;
using BattleLib.GraphicComponent;
using BattleLib.Components;
using BattleLib;

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
            MenuComponentBuilder builder = new MenuComponentBuilder();

            var mainModel = new MainMenuModel();
            var attackModel = new AttackMenuModel();
            var itemModel = new ItemMenuModel();
            var pkmnModel = new CharacterMenuModel();

            builder.AddMenu(mainModel, new MainMenuState(mainModel));
            builder.AddMenu(attackModel, new AttackMenu(attackModel));
            builder.AddMenu(itemModel, new ItemMenuState(itemModel));
            builder.AddMenu(pkmnModel, new CharacterMenuState(pkmnModel));

            builder.Component.SetMenu(MenuType.Main);

            var graphic = new BattleGraphics(engine);

            graphic.MessageBox = new MessageBox();
            graphic.Menu = builder.Graphics;

            var input = new InputComponent(builder.Component, engine);
            engine.setGraphicCompomnent(graphic);
            engine.AddComponent(input);

            using (var game = new Game1())
                engine.Run();
        }
    }
#endif
}
