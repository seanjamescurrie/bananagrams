using System.Linq.Expressions;
using Bananagrams.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace Bananagrams.Dal.Specifications.Users;

public class UserByUsernameSpec : Specification<User>
{
    private readonly string? _username;
    public UserByUsernameSpec(string? username) => _username = username;

    public override Expression<Func<User, bool>> BuildExpression()
    {
        if (string.IsNullOrWhiteSpace(_username)) return ShowAll;
        
        return x => x.Username.StartsWith(_username);
    }
}