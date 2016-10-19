using System;
using Microsoft.Xna.Framework;

namespace GameEngineTest.Registry
{
    public class GameComponentClass : GameEngine.IGameComponent
    {
        public static int instances;
        public GameComponentClass() { instances++; }
        public void Initialize() { throw new NotImplementedException(); }
        public void Update(GameTime time) { throw new NotImplementedException(); }
    }
}