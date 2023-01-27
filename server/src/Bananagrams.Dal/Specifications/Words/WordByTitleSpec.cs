using System.Linq.Expressions;
using Bananagrams.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace Bananagrams.Dal.Specifications.Words;

public class WordByTitleSpec : Specification<Word>
{
    private readonly string? _title;
    public WordByTitleSpec(string? title) => _title = title;

    public override Expression<Func<Word, bool>> BuildExpression()
    {
        if (string.IsNullOrWhiteSpace(_title)) return ShowAll;
        
        return x => x.Title.StartsWith(_title);
    }
}