using System;
using GameEngine;
using BattleLib.GraphicComponent;
using BattleLib.Components;
using BattleLib;
using BattleLib.Components.Menu;
using BattleLib.Components.BattleState;

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
            var graphic = new BattleGraphics(engine);
            var battleState = new BattleStateComponent(new ClientIdentifier(), new ClientIdentifier(), engine);
            MenuComponentBuilder builder = new MenuComponentBuilder(engine, battleState);

            builder.BuildDefaultMenu(graphic);
            builder.Input.SetMenu(MenuType.Main);


            graphic.MessageBox = new MessageBox();
            graphic.Menu = builder.Graphics;

            engine.setGraphicCompomnent(graphic);
            engine.AddComponent(builder.Input);

            using (var game = new Game1())
                engine.Run();
        }
    }
#endif
}
