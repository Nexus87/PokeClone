
using Base;
using BattleLib.Components.Menu;
using System;

namespace BattleLib.Components.Input
{
    public class AttackMenuController : AbstractListController<DefaultModel<Move>>
    {
        public AttackMenuController(DefaultModel<Move> model) : base(model)
        {
        }

        public override MenuType Type { get { return MenuType.Attack; } }

        public override event EventHandler<SelectedEventArgs<Move>> OnMoveSelected;

        public override MenuType Select()
        {
            if (OnMoveSelected != null)
                OnMoveSelected(this, new SelectedEventArgs<Move> { SelectedValue = model.Get(selectedIndex) });

            return MenuType.None;
        }

        public override void Setup()
        {
            UpdateIndex(0);
        }
    }
}
