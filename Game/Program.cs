﻿using BattleLib;
using BattleLib.Components.BattleState;
using BattleLib.Components.Menu;
using BattleLib.GraphicComponents;
using GameEngine;
using System;

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
            Engine.Init(new Configuration());
            var engine = Engine.GetInstance();
            var graphic = new BattleGraphics();
            var battleState = new BattleStateComponent(new ClientIdentifier(), new ClientIdentifier(), engine);
            MenuComponentBuilder builder = new MenuComponentBuilder(engine, battleState);

            builder.BuildDefaultMenu(graphic);
            builder.Input.SetMenu(MenuType.Main);

            graphic.Menu = builder.Graphics;

            engine.setGraphicCompomnent(graphic);
            engine.AddComponent(builder.Input);

            using (var game = new Game1())
                engine.Run();
        }
    }
#endif
}
