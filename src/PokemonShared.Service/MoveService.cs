using System.Collections.Generic;
using System.Linq;
using GameEngine.Tools;
using PokemonShared.Data;
using PokemonShared.Models;

namespace PokemonShared.Service
{
    public class MoveService
    {
        private readonly IStorage<MoveData> _moveDatas;

        public MoveService(IStorage<MoveData> moveDatas)
        {
            _moveDatas = moveDatas;
        }

        public IEnumerable<Move> GetMoves(IEnumerable<int> moveIds)
        {
            return _moveDatas
                .Where(x => moveIds.Contains(x.Id))
                .Select(x => new Move(x));
        }
    }
}
