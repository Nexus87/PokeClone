using System.Collections.ObjectModel;

namespace GameEngine.Graphics.NewGUI.Controlls
{
    public class ListView<T> : AbstractGraphicComponent
    {
        public ObservableCollection<T> Model { get; set; }

        public override void HandleKeyInput(CommandKeys key)
        {

        }
    }
}