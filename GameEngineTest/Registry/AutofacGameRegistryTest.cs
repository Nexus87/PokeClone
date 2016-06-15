using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Registry
{
    public class AutofacGameRegistryTest : IGameRegistryTest
    {
        protected override IGameRegistry CreateRegistry()
        {
            return new AutofacGameRegistry();
        }
    }
}
