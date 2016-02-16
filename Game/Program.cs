﻿using BattleLib;
using BattleLib.Components.BattleState;
using BattleLib.GraphicComponents;
using BattleLib.GraphicComponents.GUI;
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
            var config = new Configuration();
            PokeEngine.Init(config);
            var engine = PokeEngine.GetInstance();
            var graphic = new BattleGraphics(engine);
            var battleState = new BattleStateComponent(new ClientIdentifier(), new ClientIdentifier(), engine);
            
            engine.Graphic = graphic;
            var gui = new BattleGUI(config, engine);
            PokeEngine.ShowGUI();
            using (var game = new Game1())
                engine.Run();
        }
    }
#endif
}
