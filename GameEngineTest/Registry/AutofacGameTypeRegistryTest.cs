using GameEngine;
using GameEngine.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Registry
{
    public class AutofacGameTypeRegistryTest : IGameTypeRegistryTest
    {
        protected override IGameTypeRegistry CreateRegistry()
        {
            return new AutofacGameTypeRegistry();
        }
    }
}
