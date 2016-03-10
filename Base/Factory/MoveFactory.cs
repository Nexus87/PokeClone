using Base.Data;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Base.Factory
{
    public class MoveFactory
    {
        private Dictionary<string, MoveData> _moves;
        private DataContractJsonSerializer _serializer = new DataContractJsonSerializer(typeof(MoveData[]));

        public MoveFactory(string file)
        {
            using (FileStream fileIn = new FileStream(file, FileMode.Open))
            {
                _moves = new Dictionary<string, MoveData>();

                foreach (var move in (MoveData[])_serializer.ReadObject(fileIn))
                    _moves.Add(move.Name, move);
            }
        }

        public IEnumerable<string> Names
        {
            get
            {
                return _moves.Keys;
            }
        }

        public Move GetMove(string name)
        {
            MoveData data = new MoveData();
            if (!_moves.TryGetValue(name, out data))
                return null;

            return new Move(data);
        }
    }
}