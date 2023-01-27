using System.Linq.Expressions;
using Bananagrams.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace Bananagrams.Dal.Specifications.GameUsers;

public class GameUserByGameId : Specification<GameUser>
{
    private readonly int _gameId;
    public GameUserByGameId(int gameId) => _gameId = gameId;

    public override Expression<Func<GameUser, bool>> BuildExpression()
    {
        return x => x.GameId == _gameId;
    }
}