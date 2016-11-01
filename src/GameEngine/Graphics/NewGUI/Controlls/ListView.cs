using System.Collections.ObjectModel;
using System.IO;
using GameEngine.Graphics.NewGUI.Panels;
using Microsoft.Xna.Framework;

namespace GameEngine.Graphics.NewGUI.Controlls
{
    public delegate IListCell ListCellFactory(int row);

    public class ListView<T> : AbstractGraphicComponent
    {
        private ListCellFactory _listCellFactory = row => new ListCell();
        private Grid _grid;
        private bool _needsUpdate = true;

        public ObservableCollection<T> Model { get; set; }

        public ListCellFactory ListCellFactory
        {
            get { return _listCellFactory; }
            set
            {
                _listCellFactory = value;
                _needsUpdate = true;
            }
        }

        public override void HandleKeyInput(CommandKeys key)
        {

        }

        public override void Update(GameTime time)
        {
            base.Update(time);
            if(!_needsUpdate)
                return;

            _grid = new Grid {Constraints = Constraints};
            _grid.AddColumn(new ColumnProperty{Type = ValueType.Percent, Share = 1});
            for(var i = 0; i < Model.Count; i++)
            {
                _grid.AddRow(new RowProperty{Type = ValueType.Absolute, Height = 100});
                _grid.SetComponent(ListCellFactory(i), i, 0);
            }
            _grid.Update(time);
        }
    }
}