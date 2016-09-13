﻿using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using GameEngine.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.GraphicComponents
{
    [GameType]
    public class MainMenuWidget : AbstractMenuWidget<MainMenuEntries>
    {
        public MainMenuWidget(TableView<MainMenuEntries> tableView, Dialog dialog) :
            base(new TableWidget<MainMenuEntries>(null, null, tableView), dialog )
        {
            tableWidget.Model.SetDataAt(MainMenuEntries.Attack, 0, 0);
            tableWidget.Model.SetDataAt(MainMenuEntries.PKMN, 0, 1);
            tableWidget.Model.SetDataAt(MainMenuEntries.Item, 1, 0);
            tableWidget.Model.SetDataAt(MainMenuEntries.Run, 1, 1);
        }
    }
}
