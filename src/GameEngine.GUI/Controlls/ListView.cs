using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using GameEngine.Globals;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Panels;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;
using ValueType = GameEngine.GUI.Panels.ValueType;

namespace GameEngine.GUI.Controlls
{
    public delegate IGraphicComponent ListCellFactory<in T>(T value);

    [GameType]
    public class ListView<T> : AbstractGraphicComponent
    {
        private ListCellFactory<T> _listCellFactory = value => new ListCell();
        private readonly Grid _grid;
        private ObservableCollection<T> _model;

        public ListView(Grid grid)
        {
            _grid = grid;
            _grid.AddColumn(new ColumnProperty{Type = ValueType.Absolute, Width = 300});
            _grid.PreferredSizeChanged += (sender, args) => OnPreferredSizeChanged(args);
            _grid.ComponentSelected += (sender, args) => OnComponentSelected(args);
        }
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

        public ListCellFactory<T> ListCellFactory
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
            _grid.HandleKeyInput(key);
        }

        protected override void Update()
        {
            _grid.SetCoordinates(this);
            _grid.RemoveAllRows();
            foreach(var value in Model)
            {
                _grid.AddRow(new RowProperty{Type = ValueType.Absolute, Height = 100});
                _grid.SetComponent(ListCellFactory(value), _grid.Rows-1, 0);
            }
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch spriteBatch)
        {
            _grid.Draw(time, spriteBatch);
        }

        public void SelectComponent(int row)
        {
            _grid.SelectComponent(row, 0);
        }

        public override float PreferredHeight => _grid.PreferredHeight;
        public override float PreferredWidth => _grid.PreferredWidth;
    }
}