using System.Linq.Expressions;
using Bananagrams.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace Bananagrams.Dal.Specifications.Games;

public class GameByUserIdSpec : Specification<Game>
{
    private readonly int _userId;
    public GameByUserIdSpec(int userId) => _userId = userId;

    public override Expression<Func<Game, bool>> BuildExpression()
    {
        return x => x.GameUsers.FirstOrDefault().UserId == _userId;
    }
}