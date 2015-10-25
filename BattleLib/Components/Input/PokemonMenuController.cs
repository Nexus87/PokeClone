using Base;
using BattleLib.Components.Menu;
using System;

namespace BattleLib.Components.Input
{
    public class PokemonMenuController : AbstractListController<DefaultModel<Pokemon>>
    {
        public PokemonMenuController(DefaultModel<Pokemon> model) : base(model)
        {
        }

        public override event EventHandler<SelectedEventArgs<Pokemon>> OnPKMNSelected;
        public override MenuType Type
        {
            get
            {
                return MenuType.PKMN;
            }
        }

        public override MenuType Select()
        {
            if (OnPKMNSelected != null)
                OnPKMNSelected(this, new SelectedEventArgs<Pokemon> { SelectedValue = model.Get(selectedIndex) });

            return MenuType.None;
        }

        public override void Setup()
        {
            UpdateIndex(0);
        }
    }
}
