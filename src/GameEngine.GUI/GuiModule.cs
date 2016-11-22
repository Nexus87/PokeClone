using System.Reflection;
using GameEngine.Registry;

namespace GameEngine.GUI
{
    public class GuiModule : IModule
    {
        public string ModuleName => nameof(GuiModule);

        public void RegisterTypes(IGameTypeRegistry registry)
        {
            registry.ScanAssembly(Assembly.GetExecutingAssembly());
        }

        public void Start(IGameComponentManager manager, IGameTypeRegistry registry)
        {

        }

        public void Stop(IGameComponentManager engine)
        {
            throw new System.NotImplementedException();
        }
    }
}