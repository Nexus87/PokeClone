using System.Collections.Generic;

namespace Base.Factory
{
    public class MoveFactory
    {
        readonly IMoveRepository repository;

        public MoveFactory(IMoveRepository repository)
        {
            this.repository = repository;
        }

        public IReadOnlyCollection<int> Ids { get { return repository.Ids; } }

        public Move GetMove(int id)
        {
            var data = repository.GetMoveData(id);
            return new Move(data);
        }
    }
}