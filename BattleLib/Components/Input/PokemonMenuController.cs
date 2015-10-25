﻿using Base;
using BattleLib.Components.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.Input
{
    public class PokemonMenuController : AbstractListController<DefaultModel<Pokemon>>
    {
        public PokemonMenuController(DefaultModel<Pokemon> model) : base(model)
        {
        }

        public override event EventHandler<PKMNSelectedEventArgs> OnPKMNSelected;
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
                OnPKMNSelected(this, new PKMNSelectedEventArgs { SelectedPKMN = model.Get(selectedIndex) });

            return MenuType.None;
        }

        public override void Setup()
        {
            UpdateIndex(0);
        }
    }
}
