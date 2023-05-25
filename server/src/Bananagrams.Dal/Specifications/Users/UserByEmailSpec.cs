using System.Linq.Expressions;
using Bananagrams.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace Bananagrams.Dal.Specifications.Users;

public class UserByEmailSpec : Specification<User>
{
    private readonly string? _email;
    public UserByEmailSpec(string? email) => _email = email;

    public override Expression<Func<User, bool>> BuildExpression()
    {
        if (string.IsNullOrWhiteSpace(_email)) return ShowAll;
        
        return x => x.EmailAddress.StartsWith(_email);
    }
}