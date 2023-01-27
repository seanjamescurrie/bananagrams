using System.Linq.Expressions;
using Bananagrams.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace Bananagrams.Dal.Specifications.Games;

public class GameByTitleSpec : Specification<Game>
{
    private readonly string? _title;
    public GameByTitleSpec(string? title) => _title = title;

    public override Expression<Func<Game, bool>> BuildExpression()
    {
        if (string.IsNullOrWhiteSpace(_title)) return ShowAll;
        
        return x => x.Title.Contains(_title);
    }
}