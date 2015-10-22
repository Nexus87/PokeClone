using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components
{
    public class CharacterMenuModel : AbstractListModel<Pokemon>
    {
        public CharacterMenuModel(List<Pokemon> list)
        {
            items = list;
        }

        public CharacterMenuModel()
        {
            var pkmn = new List<Pokemon>();
            PKData data = new PKData();
            Stats stats = new Stats();

            for (int i = 0; i < 6; i++)
            {
                pkmn.Add(new Pokemon(data, 10, "Name" + (i + 1), stats, stats));
            }

            items = pkmn;
        }

        public override MenuType Type
        {
            get { return MenuType.PKMN; }
        }

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
