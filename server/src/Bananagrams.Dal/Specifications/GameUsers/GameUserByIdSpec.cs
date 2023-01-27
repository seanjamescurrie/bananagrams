using System.Linq.Expressions;
using Bananagrams.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace Bananagrams.Dal.Specifications.GameUsers;

public class GameUserById : Specification<GameUser>
{
    private readonly int _id;
    public GameUserById(int id) => _id = id;

    public override Expression<Func<GameUser, bool>> BuildExpression()
    {
        return x => x.Id == _id;
    }
}