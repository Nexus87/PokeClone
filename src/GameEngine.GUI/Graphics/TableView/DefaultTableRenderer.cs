using GameEngine.Registry;
using GameEngine.Utils;

namespace GameEngine.Graphics.TableView
{
    [GameType]
    public class DefaultTableRenderer<T> : ITableRenderer<T>
    {
        private readonly Table<ISelectableTextComponent> boxes = new Table<ISelectableTextComponent>();

        public string DefaultString { get; set; }


        public DefaultTableRenderer(IGameTypeRegistry registry)
        {
            Registry = registry;
            DefaultString = "";
        }

        public ISelectableGraphicComponent GetComponent(int row, int column, T data, bool isSelected)
        {
            CreateComponent(row, column);

            var ret = boxes[row, column];
            ret.Text = data == null ? DefaultString : data.ToString();
            
            if (isSelected)
                ret.Select();
            else
                ret.Unselect();

            return ret;
        }


        private void CreateComponent(int row, int column)
        {
            if (boxes[row, column] != null)
                return;

            var box = CreateComponent();
            box.Setup();
            boxes[row, column] = box;
        }

        protected virtual ISelectableTextComponent CreateComponent()
        {
            return Registry.ResolveType<ItemBox>();
        }


        public IGameTypeRegistry Registry { get; set; }
    }
}
