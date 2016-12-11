using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using GameEngine.Globals;
using GameEngine.Graphics.General;
using GameEngine.GUI.Panels;
using Microsoft.Xna.Framework;
using ValueType = GameEngine.GUI.Panels.ValueType;

namespace GameEngine.GUI.Controlls
{
    public delegate IListCell ListCellFactory(int row);

    public class ListView<T> : AbstractGraphicComponent
    {
        private ListCellFactory _listCellFactory = row => new ListCell();
        private Grid _grid;
        private ObservableCollection<T> _model;

        public ObservableCollection<T> Model
        {
            get { return _model; }
            set
            {
                if(value == null)
                    throw new ArgumentNullException(nameof(value));

                if (_model != null)
                    _model.CollectionChanged -= ModelOnCollectionChanged;
                _model = value;
                _model.CollectionChanged += ModelOnCollectionChanged;
            }
        }

        private void ModelOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            Invalidate();
        }

        public ListCellFactory ListCellFactory
        {
            get { return _listCellFactory; }
            set
            {
                _listCellFactory = value;
                Invalidate();
            }
        }

        public override void HandleKeyInput(CommandKeys key)
        {

        }

        protected override void Update(GameTime time)
        {
            _grid = new Grid {Constraints = Constraints};
            _grid.AddColumn(new ColumnProperty{Type = ValueType.Percent, Share = 1});
            for(var i = 0; i < Model.Count; i++)
            {
                _grid.AddRow(new RowProperty{Type = ValueType.Absolute, Height = 100});
                _grid.SetComponent(ListCellFactory(i), i, 0);
            }
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch spriteBatch)
        {
            _grid.Draw(time, spriteBatch);
        }
    }
}