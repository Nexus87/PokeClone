using System;
using GameEngine.Globals;
using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Controlls
{
    public class ListCell
    {
        private IGraphicComponent _component;
        private bool _isSelected;
        private Rectangle _area;
        public event EventHandler<ComponentSelectedEventArgs> CellSelected;

        public IGraphicComponent Component
        {
            get { return _component; }
            set
            {
                if (_component != null)
                {
                    _component.ComponentSelected -= ComponentOnComponentSelected;
                }

                _component = value;

                if (_component == null)
                    return;

                _component.ComponentSelected += ComponentOnComponentSelected;
                _component.Area = _area;
                _component.IsSelected = _isSelected;
            }
        }

        private void ComponentOnComponentSelected(object sender, ComponentSelectedEventArgs componentSelectedEventArgs)
        {
            OnCellSelected(componentSelectedEventArgs);
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                if (_component != null)
                    _component.IsSelected = _isSelected;
            }
        }

        public Rectangle Area
        {
            get { return _area; }
            set
            {
                _area = value;
                if (_component != null)
                    _component.Area = _area;
            }
        }


        public void HandleKeyInput(CommandKeys key)
        {
            Component?.HandleKeyInput(key);
        }


        protected virtual void OnCellSelected(ComponentSelectedEventArgs componentSelectedEventArgs)
        {
            CellSelected?.Invoke(this, componentSelectedEventArgs);
        }

        public void Draw(GameTime time, ISpriteBatch batch)
        {
            _component?.Draw(time, batch);
        }

        public void Setup()
        {
            _component?.Setup();
        }
    }
}