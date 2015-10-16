using BattleLib.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.GraphicComponent
{
    public class ItemMenuState : AbstractMenuState
    {
        ItemMenuModel model;

        List<MenuItem> internalItems = new List<MenuItem>();
        int itemViewStart = 0;
        readonly int maxItems = 8;

        public ItemMenuState(ItemMenuModel model) : base(model)
        {
            this.model = model;

            XPosition = 0.5f;
            YPosition = 1.0f / 3.0f;

            Width = 1.0f - XPosition;
            Heigth = 1.0f - YPosition;
        }

        protected override void model_OnSelectionChanged(object sender, SelectionEventArgs e)
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

            foreach (var item in model.Items)
                internalItems.Add(new MenuItem(font, arrow) { Text = item.Name });

            for (int i = 0; i < maxItems; i++)
                itemOffsets.Add(margin + i * ySpacing);

            for (int i = 0; i < Math.Min(maxItems, internalItems.Count); i++)
                items.Add(internalItems[i]);
        }
    }
}
