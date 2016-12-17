﻿using GameEngine.GUI.Graphics.GUI;
using GameEngine.GUI.Graphics.TableView;
using GameEngine.Registry;

namespace BattleLib.Components.GraphicComponents
{
    [GameType]
    public class MainMenuWidget : AbstractMenuWidget<MainMenuEntries>
    {
        public MainMenuWidget(TableView<MainMenuEntries> tableView, Dialog dialog) :
            base(new TableWidget<MainMenuEntries>(null, null, tableView), dialog )
        {
            TableWidget.Model.SetDataAt(MainMenuEntries.Attack, 0, 0);
            TableWidget.Model.SetDataAt(MainMenuEntries.PKMN, 0, 1);
            TableWidget.Model.SetDataAt(MainMenuEntries.Item, 1, 0);
            TableWidget.Model.SetDataAt(MainMenuEntries.Run, 1, 1);
        }
    }
}
