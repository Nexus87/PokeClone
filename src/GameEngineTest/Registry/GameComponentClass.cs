using System;
using Microsoft.Xna.Framework;
using IGameComponent = GameEngine.Core.IGameComponent;

namespace GameEngineTest.Registry
{
    public class GameComponentClass : IGameComponent
    {
        public static int Instances;
        public GameComponentClass() { Instances++; }
        public void Initialize() { throw new NotImplementedException(); }
        public void Update(GameTime time) { throw new NotImplementedException(); }
    }
}