using Base;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BattleLib.Components
{
    public class AttackMenuModel : AbstractListModel<Move>
    {
        public AttackMenuModel(Pokemon pkm)
        {
            items = pkm.Moves;
        }

        //TODO remove this constructor
        public AttackMenuModel()
        {
            MoveData data1 = new MoveData{Name = "Attack1"};
            MoveData data2 = new MoveData{Name = "Attack2"};
            items = new List<Move> { new Move(data1), new Move(data2) };
        }
        public override MenuType Type{ get { return MenuType.Attack; } }
        public override MenuType Back()
        {
            return MenuType.Main;
        }

        public override MenuType Select()
        {
            throw new NotImplementedException();
        }


        public override void Init()
        {
            SelectIndex(0);
        }
    }
}