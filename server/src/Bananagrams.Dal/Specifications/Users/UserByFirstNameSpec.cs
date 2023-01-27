using System.Linq.Expressions;
using Bananagrams.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace Bananagrams.Dal.Specifications.Users;

public class UserByFirstNameSpec : Specification<User>
{
    private readonly string? _firstName;
    public UserByFirstNameSpec(string? firstName) => _firstName = firstName;

    public override Expression<Func<User, bool>> BuildExpression()
    {
        if (string.IsNullOrWhiteSpace(_firstName)) return ShowAll;
        
        return x => x.FirstName.StartsWith(_firstName);
    }
}