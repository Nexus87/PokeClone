using System.Collections.Generic;
using Pokemon.Models;

namespace Pokemon.Services.Factory
{
    public class MoveFactory
    {
        private readonly IMoveRepository repository;

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