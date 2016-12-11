using System;
using GameEngine.Globals;

namespace GameEngine.GUI.Controlls
{
    public interface IListCell : IGuiComponent
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