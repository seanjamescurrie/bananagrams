using System.Linq.Expressions;
using Bananagrams.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace Bananagrams.Dal.Specifications.Users;

public class UserByIdSpec : Specification<User>
{
    private readonly int? _id;
    public UserByIdSpec(int? id) => _id = id;

    public override Expression<Func<User, bool>> BuildExpression()
    {
        if (!_id.HasValue) return ShowAll;
        
        return x => x.Id == _id;
    }
}