using Base;
using BattleLib.Components;
using BattleLib.Components.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib
{
    public class ModelFactory
    {
        public IMenuModel GetModel(MenuType type)
        {
            switch (type)
            {
                case MenuType.Attack:
                    MoveData data1 = new MoveData { Name = "Attack1" };
                    MoveData data2 = new MoveData { Name = "Attack2" };
                    return new DefaultModel<Move>(new List<Move> { new Move(data1), new Move(data2) }, type);
                case MenuType.Item:
                    var tmpItems = new List<Item>();
                    for (int i = 0; i < 10; i++)
                        tmpItems.Add(new Item { Name = "Item" + (i + 1) });
                    return new DefaultModel<Item>(tmpItems, type);
                case MenuType.Main:
                    var mainMenu = new List<String> {
                        MenuType.Attack.ToString(),
                        MenuType.PKMN.ToString(),
                        MenuType.Item.ToString(),
                        "Run"
                    };
                    return new DefaultModel<String>(mainMenu, type);
                case MenuType.PKMN:
                    var pkmn = new List<Pokemon>();
                    PKData data = new PKData();
                    Stats stats = new Stats();

                    for (int i = 0; i < 6; i++)
                    {
                        pkmn.Add(new Pokemon(data, 10, "Name" + (i + 1), stats, stats));
                    }

                    return new DefaultModel<Pokemon>(pkmn, type);
                default:
                    return null;
            }
        }
    }
}
