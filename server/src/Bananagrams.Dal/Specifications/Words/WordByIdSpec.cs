using System.Linq.Expressions;
using Bananagrams.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace Bananagrams.Dal.Specifications.Words;

public class WordByGameIdSpec : Specification<Word>
{
    private readonly int _id;
    public WordByGameIdSpec(int id) => _id = id;

    public override Expression<Func<Word, bool>> BuildExpression()
    {
        return x => x.Id == _id;
    }
}