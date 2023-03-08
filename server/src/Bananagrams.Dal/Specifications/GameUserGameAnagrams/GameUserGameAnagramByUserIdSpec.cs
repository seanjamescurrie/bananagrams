using System.Linq.Expressions;
using Bananagrams.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace Bananagrams.Dal.Specifications.GameUserGameAnagrams;

public class GameUserGameAnagramByUserIdSpec : Specification<GameUserGameAnagram>
{
    private readonly int _userId;

    public GameUserGameAnagramByUserIdSpec(int userId) => 
        (_userId) = (userId);

    public override Expression<Func<GameUserGameAnagram, bool>> BuildExpression()
    {
        return x => x.GameUser.UserId == _userId;
    }
}