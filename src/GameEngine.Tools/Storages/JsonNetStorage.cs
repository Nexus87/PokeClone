using System.IO;
using Newtonsoft.Json;

namespace GameEngine.Tools.Storages
{
    internal static class JsonNetStorage
    {
        internal static T LoadObject<T>(string path)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }

        internal static void StoreObject<T>(string path, T data, bool formated = false)
        {
            using (var file = File.CreateText(path))
            using (var writer = new JsonTextWriter(file) { Formatting = formated ? Formatting.Indented : Formatting.None })
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, data);
            }
        }
    }
}