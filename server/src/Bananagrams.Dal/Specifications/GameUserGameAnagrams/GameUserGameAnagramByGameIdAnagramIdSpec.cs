using System.Linq.Expressions;
using Bananagrams.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace Bananagrams.Dal.Specifications.GameUserGameAnagrams;

public class GameUserGameAnagramByGameIdAnagramIdSpec : Specification<GameUserGameAnagram>
{
    private readonly int _gameId;
    private readonly int _anagramId;

    public GameUserGameAnagramByGameIdAnagramIdSpec(int gameId, int anagramId) => 
        (_gameId, _anagramId) = (gameId, anagramId);

    public override Expression<Func<GameUserGameAnagram, bool>> BuildExpression()
    {
        return x => x.GameAnagram.GameId == _gameId && x.GameAnagram.Id == _anagramId;
    }
}