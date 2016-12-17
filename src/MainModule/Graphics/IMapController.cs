﻿using GameEngine.Graphics;
using GameEngine.GUI.Graphics;

namespace MainModule.Graphics
{
    public interface IMapController : IGraphicComponent
    {
        void CenterField(int fieldX, int fieldY);
        void MoveMap(Direction moveDirection);
        void LoadMap(Map map);
    }
}