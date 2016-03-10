using Base.Data;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Base.Factory
{
    public class MoveFactory
    {
        IMoveRepository repository;

        public MoveFactory(IMoveRepository repository)
        {
            this.repository = repository;
        }

        public IReadOnlyCollection<int> Ids { get { return repository.GetIds().AsReadOnly(); } }

        public Move GetMove(int id)
        {
            MoveData data = repository.GetMoveData(id);
            return new Move(data);
        }
    }
}