using GameEngine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Util
{
    public class SelectableGraphicComponentMock : GraphicComponentMock, ISelectableGraphicComponent
    {
        public bool IsSelected { get; set; }

        public void Select()
        {
            IsSelected = true;
        }

        public void Unselect()
        {
            IsSelected = false;
        }
    }
}
