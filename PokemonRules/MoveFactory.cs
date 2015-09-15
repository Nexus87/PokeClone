using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using Base;
using System.IO;

namespace PokemonRules
{
    public class MoveFactory
    {
        DataContractJsonSerializer _serializer = new DataContractJsonSerializer(typeof(MoveData[]));
        Dictionary<string, MoveData> _moves;

        public MoveFactory(string file)
        {
            using (FileStream fileIn = new FileStream(file, FileMode.Open))
            {
                _moves = new Dictionary<string, MoveData>();

                foreach (var move in (MoveData[])_serializer.ReadObject(fileIn))
                    _moves.Add(move.Name, move);

            }
        }

        public Move GetMove(string name)
        {
            MoveData data = new MoveData();
            if (!_moves.TryGetValue(name, out data))
                return null;

            return new Move(data);
        }

        public IEnumerable<string> Names
        {
            get
            {
                return _moves.Keys;
            }
        }
    }
}
