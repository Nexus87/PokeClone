
using Base;
using BattleLib.Components.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleLib.Components.Menu;

namespace BattleLib.Components.Input
{
    public class AttackMenuController : AbstractListController<DefaultModel<Move>>
    {
        public AttackMenuController(DefaultModel<Move> model) : base(model)
        {
        }

        public override MenuType Type { get { return MenuType.Attack; } }

        public override event EventHandler<MoveSelectedEventArgs> OnMoveSelected;

        public override MenuType Select()
        {
            if (OnMoveSelected != null)
                OnMoveSelected(this, new MoveSelectedEventArgs { SelectedMove = model.Get(selectedIndex) });

            return MenuType.None;
        }

        public override void Setup()
        {
            UpdateIndex(0);
        }
    }
}
