using Base;
using BattleLib.Components.Menu;
using System;

namespace BattleLib.Components.Input
{
    public class ItemMenuController : AbstractListController<DefaultModel<Item>>
    {
        public ItemMenuController(DefaultModel<Item> model) : base(model)
        {
        }

        public override event EventHandler<SelectedEventArgs<Item>> OnItemSelection;

        public override MenuType Type { get { return MenuType.Item; } }

        public override MenuType Select()
        {
            if (OnItemSelection != null)
                OnItemSelection(this, new SelectedEventArgs<Item> { SelectedValue = model.Get(selectedIndex) });

            return MenuType.None;
        }

        public override void Setup()
        {
            UpdateIndex(0);
        }
    }
}
