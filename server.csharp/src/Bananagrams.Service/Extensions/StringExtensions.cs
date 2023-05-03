namespace Bananagrams.Service.Extensions;

public static class StringExtensions
{
    public static string Scramble(this string s){
        return new string(s.ToCharArray().OrderBy(x=>Guid.NewGuid()).ToArray());
    }
}