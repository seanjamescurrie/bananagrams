using System.Linq.Expressions;
using Bananagrams.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace Bananagrams.Dal.Specifications.Games;

public class GameByDateSpec : Specification<Game>
{
    private readonly DateTime _date;
    public GameByDateSpec(DateTime date) => _date = date;

    public override Expression<Func<Game, bool>> BuildExpression()
    {
        return x => x.DateCreated.ToString().Contains(_date.ToString("yyyy-MM-dd"));
    }
}