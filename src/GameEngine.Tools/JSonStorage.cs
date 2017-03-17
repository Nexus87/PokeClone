using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Tools.Storages;

namespace GameEngine.Tools
{
    public class JSonStorage<T> : IStorage<T>
    {
        private readonly string _file;
        private List<T> _data;

        public JSonStorage(string file)
        {
            _file = "Conten/" + file;
        }

        private void Init()
        {
            _data = JsonNetStorage.LoadObject<T[]>(_file).ToList();
        }

        public IEnumerator<T> GetEnumerator()
        {
            if(_data == null)
                Init();
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}