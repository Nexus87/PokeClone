using GameEngine.Registry;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Registry
{
    [TestFixture]
    public class AutofacModuleRegistryTest : IModuleRegistryTest
    {
        protected override IModuleRegistry CreateModuleRegistration()
        {
            return new AutofacModuleRegistry();
        }
    }
}
