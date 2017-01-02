﻿using GameEngine.Graphics.General;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Renderers;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Controlls
{
    [GameType]
    public class ImageBox : AbstractGraphicComponent, IImageBox
    {
        private readonly ImageBoxRenderer _renderer;
        private ITexture2D _image;

        public ITexture2D Image
        {
            get { return _image; }
            set
            {
                _image = value;
                Invalidate();
            }
        }


        public ImageBox(ImageBoxRenderer renderer)
        {
            _renderer = renderer;
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _renderer.Render(batch, this);
        }

    }
}