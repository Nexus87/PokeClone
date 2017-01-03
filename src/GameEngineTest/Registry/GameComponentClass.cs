using System;
using Microsoft.Xna.Framework;
using IGameComponent = GameEngine.Components.IGameComponent;

namespace GameEngineTest.Registry
{
    public class GameComponentClass : IGameComponent
    {
        public static int Instances;
        public GameComponentClass() { Instances++; }
        public void Update(GameTime time) { throw new NotImplementedException(); }
    }
}