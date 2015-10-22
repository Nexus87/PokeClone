using Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components
{
    public class ItemMenuModel : AbstractListModel<Item>
    {
        public ItemMenuModel()
        {
            var tmpItems = new List<Item>();
            for (int i = 0; i < 10; i++)
                tmpItems.Add(new Item { Name = "Item" + (i + 1) });
            items = tmpItems;
        }

        public override MenuType Type{ get { return MenuType.Item; } }

        public override MenuType Select()
        {
            throw new NotImplementedException();
        }

        public override MenuType Back()
        {
            return MenuType.Main;
        }

        public override void Init()
        {
            SelectIndex(0);

        }

    }
}
