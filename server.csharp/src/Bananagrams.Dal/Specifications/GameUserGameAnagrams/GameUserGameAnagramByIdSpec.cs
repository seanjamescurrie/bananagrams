using System.Linq.Expressions;
using Bananagrams.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace Bananagrams.Dal.Specifications.GameUserGameAnagrams;

public class GameUserGameAnagramByIdSpec : Specification<GameUserGameAnagram>
{
    private readonly int _id;

    public GameUserGameAnagramByIdSpec(int id) => _id = id;

    public override Expression<Func<GameUserGameAnagram, bool>> BuildExpression()
    {
        return x => x.Id == _id;
    }
}