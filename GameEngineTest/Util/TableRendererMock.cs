using GameEngine.Graphics;
using GameEngine.Graphics.Views;
using GameEngine.Utils;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using System;

namespace GameEngineTest.Util
{
    public class TableComponentMock<T> : ISelectableGraphicComponent
    {
        public bool WasDrawn { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public GameEngine.PokeEngine Game { get; set; }

        public void PlayAnimation(GameEngine.Graphics.Basic.IAnimation animation)
        {
            throw new System.NotImplementedException();
        }

        public event EventHandler<GraphicComponentSizeChangedEventArgs> SizeChanged;
        public event EventHandler<GraphicComponentPositionChangedEventArgs> PositionChanged;

        public float XPosition { get; set; }
        public float YPosition { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }
        public T Data { get; set; }
        public void Draw(GameTime time, ISpriteBatch batch)
        {
            WasDrawn = true;
        }

        public void Setup(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            
        }

        public bool IsSelected { get; set; }
        public void Select() { IsSelected = true; } 
        public void Unselect() { IsSelected = false; }
    }

    public class TableRendererMock<T> : ITableRenderer<T>
    {
        public T[,] entries = new T[0, 0];
        public bool[,] selections = new bool[0,0];
        public TableComponentMock<T>[,] components = new TableComponentMock<T>[0,0];

        public GameEngine.Graphics.ISelectableGraphicComponent GetComponent(int row, int column, T data, bool isSelected)
        {
            entries = Resize(row, column, entries);
            selections = Resize(row, column, selections);
            components = Resize(row, column, components);

            entries[row, column] = data;
            selections[row, column] = isSelected;

            if (components[row, column] == null)
                components[row, column] = new TableComponentMock<T> { Row = row, Column = column, WasDrawn = false };

            var component = components[row, column];
            component.Data = data;
            component.IsSelected = isSelected;

            return component;
        }

        public void ClearDrawnComponents()
        {
            foreach (var c in components)
            {
                if (c != null)
                    c.WasDrawn = false;
            }
        }

        private S[,] Resize<S>(int row, int column, S[,] source)
        {
            if (row >= source.Rows() || column >= source.Columns())
            {
                // Always grow
                int newRows = Math.Max(source.Rows(), row + 1);
                int newColumns = Math.Max(source.Columns(), column + 1);

                var tmp = new S[newRows, newColumns];
                source.Copy(tmp);
                return tmp;
            }
            else
                return source;
        }

        public void Reset()
        {
            entries = new T[0, 0];
            selections = new bool[0, 0];
        }
    }
}