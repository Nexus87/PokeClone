using System;
using GameEngine.Globals;
using GameEngine.GUI.Graphics;

namespace GameEngine.GUI.Controlls
{
    public interface IListCell : IGraphicComponent
    {
        event EventHandler CellSelected;
    }

    public class ListCell : AbstractGraphicComponent, IListCell
    {
        public event EventHandler CellSelected;


        public override void HandleKeyInput(CommandKeys key)
        {
            if(key == CommandKeys.Select)
                OnCellSelected();

        }

        protected virtual void OnCellSelected()
        {
            CellSelected?.Invoke(this, EventArgs.Empty);
        }
    }
}