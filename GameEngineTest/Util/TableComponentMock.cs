using GameEngine.Graphics;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Util
{
    public class TableComponentMock<T> : TextGraphicComponentMock, ISelectableTextComponent
    {
        public T Data { get; set; }
        public bool IsSelected { get; set; }



        public void Select() { IsSelected = true; }
        public void Unselect() { IsSelected = false; }

    }
}
