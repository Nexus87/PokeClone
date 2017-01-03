using System;
using GameEngine.Entities;
using Microsoft.Xna.Framework;

namespace GameEngineTest.Registry
{
    public class GameEntityClass : IGameEntity
    {
        public static int Instances;
        public GameEntityClass() { Instances++; }
        public void Update(GameTime time) { throw new NotImplementedException(); }
    }
}