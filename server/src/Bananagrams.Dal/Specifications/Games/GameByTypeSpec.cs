using System.Linq.Expressions;
using Bananagrams.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace Bananagrams.Dal.Specifications.Games;

public class GameByTypeSpec : Specification<Game>
{
    private readonly int _gameTypeId;
    public GameByTypeSpec(int gameTypeId) => _gameTypeId = gameTypeId;

    public override Expression<Func<Game, bool>> BuildExpression()
    {
        return x => x.GameAnagrams.FirstOrDefault().GameAnagramTypeId == _gameTypeId;
    }
}