namespace GameEngine.Core.ModuleManager
{
    public interface IConfigurationHandler
    {
        void HandleConfiguration(object moduleKey, string file, string contentRoot);
    }
}