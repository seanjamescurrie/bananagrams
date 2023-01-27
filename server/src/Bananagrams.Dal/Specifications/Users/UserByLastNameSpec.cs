using System.Linq.Expressions;
using Bananagrams.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace Bananagrams.Dal.Specifications.Users;

public class UserByLastNameSpec : Specification<User>
{
    private readonly string? _lastName;
    public UserByLastNameSpec(string? lastName) => _lastName = lastName;

    public override Expression<Func<User, bool>> BuildExpression()
    {
        if (string.IsNullOrWhiteSpace(_lastName)) return ShowAll;
        
        return x => x.LastName.StartsWith(_lastName);
    }
}