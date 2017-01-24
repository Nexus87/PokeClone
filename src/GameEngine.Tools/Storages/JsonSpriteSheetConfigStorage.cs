using System.Collections.Generic;

namespace GameEngine.Tools.Storages
{
    public static class JsonSpriteSheetConfigStorage
    {
        public static Dictionary<string, Area> Load(string fileName)
        {
            return JsonNetStorage.LoadObject<Dictionary<string, Area>>(fileName);
        }
    }
}