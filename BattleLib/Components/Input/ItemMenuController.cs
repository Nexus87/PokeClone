using Base;
using BattleLib.Components.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.Input
{
    public class ItemMenuController : AbstractListController<DefaultModel<Item>>
    {
        public ItemMenuController(DefaultModel<Item> model) : base(model)
        {
        }

        public override event EventHandler<ItemSelectedEventArgs> OnItemSelection;

        public override MenuType Type { get { return MenuType.Item; } }

        public override MenuType Select()
        {
            if (OnItemSelection != null)
                OnItemSelection(this, new ItemSelectedEventArgs { SelectedItem = model.Get(selectedIndex) });

            return MenuType.None;
        }

        public override void Setup()
        {
            UpdateIndex(0);
        }
    }
}
