using GameEngine.Registry;
using GameEngine.Utils;

namespace GameEngine.GUI.Graphics.TableView
{
    [GameType]
    public class DefaultTableRenderer<T> : ITableRenderer<T>
    {
        private readonly Table<ISelectableTextComponent> _boxes = new Table<ISelectableTextComponent>();

        public string DefaultString { get; set; }


        public DefaultTableRenderer(IGameTypeRegistry registry)
        {
            Registry = registry;
            DefaultString = "";
        }

        public IGraphicComponent GetComponent(int row, int column, T data)
        {
            CreateComponent(row, column);

            var ret = _boxes[row, column];
            ret.Text = data == null ? DefaultString : data.ToString();
            
            return ret;
        }


        private void CreateComponent(int row, int column)
        {
            if (_boxes[row, column] != null)
                return;

            var box = CreateComponent();
            box.Setup();
            _boxes[row, column] = box;
        }

        protected virtual ISelectableTextComponent CreateComponent()
        {
            return Registry.ResolveType<ItemBox>();
        }


        public IGameTypeRegistry Registry { get; set; }
    }
}
