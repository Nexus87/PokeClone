using System;

namespace GameEngine.GUI.Controlls
{
    public interface IListCell : IGraphicComponent
    {
        event EventHandler CellSelected;
    }

    public class ListCell : GameEngine.GUI.AbstractGraphicComponent, IListCell
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