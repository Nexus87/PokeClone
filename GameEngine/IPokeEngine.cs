using GameEngine.Graphics;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
namespace GameEngine
{
    public interface IPokeEngine
    {
        float AspectRation { get; }
        float ScreenHeight { get; }
        float ScreenWidth { get; }
        Color BackgroundColor { get; }
        ContentManager Content { get; }
        ISpriteFont DefaultFont { get; }
        ITexture2D DefaultArrowTexture { get; }
        ITexture2D DefaultBorderTexture { get; }
        GameServiceContainer Services { get; }
        GUIManager GUIManager { get; }
        void Exit();
        void AddGameComponent(IGameComponent component);
        void RemoveGameComponent(IGameComponent component);
        IGraphicComponent Graphic { get; set; }
    }
}
