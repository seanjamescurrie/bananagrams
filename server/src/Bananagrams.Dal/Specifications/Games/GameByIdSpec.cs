using System.Linq.Expressions;
using Bananagrams.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace Bananagrams.Dal.Specifications.Games;

public class GameByIdSpec : Specification<Game>
{
    private readonly int _id;
    public GameByIdSpec(int id) => _id = id;

    public override Expression<Func<Game, bool>> BuildExpression()
    {
        return x => x.Id == _id;
    }
}