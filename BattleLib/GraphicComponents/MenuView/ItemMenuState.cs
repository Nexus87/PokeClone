﻿using BattleLib.Components.Input;
using BattleLib.Components.Menu;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace BattleLib.GraphicComponents.MenuView
{
    public class ItemMenuState : AbstractMenuState
    {
        IMenuModel model;

        List<MenuItem> internalItems = new List<MenuItem>();
        int itemViewStart = 0;
        readonly int maxItems = 8;

        public ItemMenuState(IMenuModel model, IMenuController controller) : base(controller)
        {
            this.model = model;

            XPosition = 0.5f;
            YPosition = 1.0f / 3.0f;

            Width = 1.0f - XPosition;
            Heigth = 1.0f - YPosition;
        }

        public override void model_OnSelectionChanged(object sender, SelectedIndexChangedEvent e)
        {
            int index = e.NewSelection;

            if (index < itemViewStart)
            {
                itemViewStart = index;
                items.Clear();
                items.AddRange(internalItems.GetRange(itemViewStart, maxItems));
            }
            else if (index > itemViewStart + maxItems - 1)
            {
                itemViewStart = index - (maxItems - 1);
                items.Clear();
                items.AddRange(internalItems.GetRange(itemViewStart, maxItems));
            }

            e.NewSelection = index - itemViewStart;
            base.model_OnSelectionChanged(sender, e);
        }

        protected override void BuildMenu()
        {
            Vector2 margin = new Vector2(30, 40);
            Vector2 ySpacing = new Vector2(0, 30);

            foreach (var item in model)
                internalItems.Add(new MenuItem(font, arrow) { Text = item });

            for (int i = 0; i < maxItems; i++)
                itemOffsets.Add(margin + i * ySpacing);

            for (int i = 0; i < Math.Min(maxItems, internalItems.Count); i++)
                items.Add(internalItems[i]);
        }
    }
}
