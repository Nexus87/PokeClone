using System;
using GameEngine.Globals;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Controlls
{
    public class ListCell
    {
        private Rectangle _area;
        private IGuiComponent _component;
        private bool _isSelected;

        public IGuiComponent Component
        {
            get => _component;
            set
            {
                if (_component != null)
                    _component.ComponentSelected -= ComponentOnComponentSelected;

                _component = value;

                if (_component == null)
                    return;

                _component.ComponentSelected += ComponentOnComponentSelected;
                _component.Area = _area;
                _component.IsSelected = _isSelected;
            }
        }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                if (_component != null)
                    _component.IsSelected = _isSelected;
            }
        }

        public Rectangle Area
        {
            get => _area;
            set
            {
                _area = value;
                if (_component != null)
                    _component.Area = _area;
            }
        }

        public event EventHandler<ComponentSelectedEventArgs> CellSelected;

        private void ComponentOnComponentSelected(object sender, ComponentSelectedEventArgs componentSelectedEventArgs)
        {
            OnCellSelected(componentSelectedEventArgs);
        }


        public void HandleKeyInput(CommandKeys key)
        {
            Component?.HandleKeyInput(key);
        }


        private void OnCellSelected(ComponentSelectedEventArgs componentSelectedEventArgs)
        {
            CellSelected?.Invoke(this, componentSelectedEventArgs);
        }
    }
}