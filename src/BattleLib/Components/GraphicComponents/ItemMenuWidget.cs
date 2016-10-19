using Base;
using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using GameEngine.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.GraphicComponents
{
    [GameType]
    public class ItemMenuWidget : AbstractMenuWidget<Item>
    {
        public ItemMenuWidget(TableView<Item> tableView, Dialog dialog) :
            base(new TableWidget<Item>(8, null, tableView), dialog)
        {
            //TODO use ItemModel instead
            var model = tableWidget.Model;
            for (int i = 0; i < 20; i++)
                model.SetDataAt(new Item { Name = "Item" + i }, i, 0);
        }
    }
}
