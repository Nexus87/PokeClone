using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Graphics.GUI
{
    public abstract class IWidgetTest : IGraphicComponentTest
    {
        protected abstract IWidget CreateWidget();

        protected override IGraphicComponent CreateComponent()
        {
            return CreateWidget();
        }
    }
}
