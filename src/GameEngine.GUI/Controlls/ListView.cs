using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using GameEngine.Globals;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Controlls
{
    public delegate IGraphicComponent ListCellFactory<in T>(T value);

    [GameType]
    public sealed class ListView<T> : AbstractGraphicComponent
    {
        private ListCellFactory<T> _listCellFactory = delegate { return null; };
        private readonly List<ListCell> _listItems = new List<ListCell>();
        private ObservableCollection<T> _model = new ObservableCollection<T>(new List<T>());
        private int _lastSelectedIndex;

        public int CellHeight { get; set; } = 100;
        public int DefaultListWidth { get; set; } = 300;

        public ListView()
        {
            Area = new Rectangle(0, 0, DefaultListWidth, 0);
        }

        public ObservableCollection<T> Model
        {
            get { return _model; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                if (_model != null)
                    _model.CollectionChanged -= ModelOnCollectionChanged;
                _model = value;
                _model.CollectionChanged += ModelOnCollectionChanged;
                ReCreateItems();
            }
        }

        private void ModelOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddItems(args.NewStartingIndex, args.NewItems.Cast<T>().ToList());
                    break;
                case NotifyCollectionChangedAction.Remove:
                    RemoveItems(args.OldStartingIndex, args.OldItems.Count);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    for (var i = args.NewStartingIndex; i < args.NewItems.Count; i++)
                    {
                        _listItems[i].Component = ListCellFactory(args.NewItems.Cast<T>().ToList()[i]);
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    RemoveItems(args.OldStartingIndex, args.OldItems.Count);
                    AddItems(args.NewStartingIndex, args.NewItems.Cast<T>());
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void RemoveItems(int startingIndex, int itemCount)
        {
            _listItems.RemoveRange(startingIndex, itemCount);
        }

        private void AddItems(int argsNewStartingIndex, IEnumerable<T> newItems)
        {
            var newCells = newItems.Select(CreateCell);
            if(argsNewStartingIndex == _listItems.Count)
                _listItems.AddRange(newCells);
            else
                _listItems.InsertRange(argsNewStartingIndex, newCells);
        }

        public ListCellFactory<T> ListCellFactory
        {
            get { return _listCellFactory; }
            set
            {
                _listCellFactory = value;
                ReCreateItems();
            }
        }

        public override void HandleKeyInput(CommandKeys key)
        {
            if(_listItems.Count == 0)
                return;

            switch (key)
            {
                case CommandKeys.Down:
                {
                    SelectNextComponent();
                }
                    break;
                case CommandKeys.Up:
                {
                    SelectPreviousComponent();
                }
                    break;
                default:
                    if (_lastSelectedIndex < _listItems.Count)
                        _listItems[_lastSelectedIndex].HandleKeyInput(key);
                    break;
            }
        }

        private void SelectPreviousComponent()
        {
            var upperLimit = Math.Min(_listItems.Count, _lastSelectedIndex);
            var nextComponent = _listItems
                .Take(upperLimit)
                .Select(x => x.Component)
                .Reverse()
                .FirstOrDefault(x => x != null && x.IsSelectable);

            if (nextComponent == null)
                return;

            UnselectLastIndex();
            nextComponent.IsSelected = true;
        }

        private void SelectNextComponent()
        {
            var lowerLimit = Math.Min(_listItems.Count, _lastSelectedIndex + 1);

            var nextComponent = _listItems
                .Skip(lowerLimit)
                .Select(x => x.Component)
                .FirstOrDefault(x => x != null && x.IsSelectable);

            if (nextComponent == null)
                return;

            UnselectLastIndex();
            nextComponent.IsSelected = true;
        }

        private void UnselectLastIndex()
        {
            if (_lastSelectedIndex < _listItems.Count)
                _listItems[_lastSelectedIndex].IsSelected = false;
        }

        protected override void Update()
        {
            for (var i = 0; i < _listItems.Count; i++)
            {
                _listItems[i].Area = new Rectangle(Area.X, Area.Y + i * CellHeight, Area.Width, CellHeight);
            }

            PreferredHeight = _listItems.Count * CellHeight;
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch spriteBatch)
        {
            foreach (var listItem in _listItems)
            {
                listItem.Draw(time, spriteBatch);
            }
        }

        private void ReCreateItems()
        {
            _listItems.Clear();
            foreach (var item in Model)
            {
                var cell = CreateCell(item);
                _listItems.Add(cell);
            }
        }

        private ListCell CreateCell(T item)
        {
            var cell = new ListCell {Component = ListCellFactory(item)};
            cell.CellSelected += (sender, args) => OnComponentSelected(args);
            cell.CellSelected += (sender, args) => _lastSelectedIndex = _listItems.IndexOf(sender as ListCell);
            return cell;
        }

        public void SelectCell(int i)
        {
            UnselectLastIndex();
            _listItems[i].IsSelected = true;
        }

        public IGraphicComponent GetComponent(int i)
        {
            return _listItems[i].Component;
        }
    }
}