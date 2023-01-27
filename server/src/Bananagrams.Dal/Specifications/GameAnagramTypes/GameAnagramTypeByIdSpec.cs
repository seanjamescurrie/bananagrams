using System.Linq.Expressions;
using Bananagrams.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace Bananagrams.Dal.Specifications.GameAnagramTypes;

public class GameAnagramTypeByGameId : Specification<GameAnagramType>
{
    private readonly int _id;
    public GameAnagramTypeByGameId(int id) => _id = id;

    public override Expression<Func<GameAnagramType, bool>> BuildExpression()
    {
        return x => x.Id == _id;
    }
}
