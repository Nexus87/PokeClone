using Base;
using GameEngine.GUI.Graphics.GUI;
using GameEngine.GUI.Graphics.TableView;
using GameEngine.TypeRegistry;

namespace BattleMode.Core.Components.GraphicComponents
{
    [GameType]
    public class ItemMenuWidget : AbstractMenuWidget<Item>
    {
        public ItemMenuWidget(TableView<Item> tableView, Dialog dialog, IGameTypeRegistry registry) :
            base(new TableWidget<Item>(8, null, tableView), dialog, registry)
        {
            //TODO use ItemModel instead
            var model = TableWidget.Model;
            for (var i = 0; i < 20; i++)
                model.SetDataAt(new Item { Name = "Item" + i }, i, 0);
        }
    }
}
