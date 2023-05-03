using System.Linq.Expressions;
using Bananagrams.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace Bananagrams.Dal.Specifications.GameUserGameAnagrams;

public class GameUserGameAnagramByGameIdSpec : Specification<GameUserGameAnagram>
{
    private readonly int _gameId;

    public GameUserGameAnagramByGameIdSpec(int gameId) => 
        (_gameId) = (gameId);

    public override Expression<Func<GameUserGameAnagram, bool>> BuildExpression()
    {
        return x => x.GameAnagram.GameId == _gameId;
    }
}