using System.Linq.Expressions;
using Bananagrams.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace Bananagrams.Dal.Specifications.GameUserGameAnagrams;

public class GameUserGameAnagramAnagramIdSpec : Specification<GameUserGameAnagram>
{
    private readonly int _anagramId;

    public GameUserGameAnagramAnagramIdSpec(int anagramId) => 
        (_anagramId) = (anagramId);

    public override Expression<Func<GameUserGameAnagram, bool>> BuildExpression()
    {
        return x => x.GameAnagram.Id == _anagramId;
    }
}