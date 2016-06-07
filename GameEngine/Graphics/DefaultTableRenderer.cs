﻿using GameEngine.Utils;
using System;

namespace GameEngine.Graphics
{
    public class DefaultTableRenderer<T> : ITableRenderer<T>
    {
        Table<ISelectableTextComponent> boxes = new Table<ISelectableTextComponent>();

        public string DefaultString { get; set; }


        public DefaultTableRenderer(GraphicComponentFactory factory)
        {
            this.factory = factory;
            DefaultString = "";
        }

        public ISelectableGraphicComponent GetComponent(int row, int column, T data, bool IsSelected)
        {
            CreateComponent(row, column);

            var ret = boxes[row, column];
            ret.Text = data == null ? DefaultString : data.ToString();
            
            if (IsSelected)
                ret.Select();
            else
                ret.Unselect();

            return ret;
        }


        private void CreateComponent(int row, int column)
        {
            if (boxes[row, column] == null)
            {
                var box = CreateComponent(); 
                box.Setup();
                boxes[row, column] = box;
            }
        }

        protected virtual ISelectableTextComponent CreateComponent()
        {
            return factory.CreateGraphicComponent<ItemBox>();
        }


        public GraphicComponentFactory factory { get; set; }
    }
}