// using System.Linq.Expressions;
// using Bananagrams.Dal.Models;
// using Bananagrams.Dal.Specifications.Users;
// using Bananagrams.Service.Dtos;
// using Unosquare.EntityFramework.Specification.Common.Extensions;
// using Unosquare.EntityFramework.Specification.Common.Primitive;
//
// namespace Bananagrams.Service.Selectors;
//
// public class GameToGameDtoSelector : Selector<Game, GameDto>
// {
//     private readonly int? _userId;
//
//     public GameToGameDtoSelector(int? userId = null) => _userId = userId;
//     
//     public override Expression<Func<Game, GameDto>> BuildExpression() => 
//         x => new GameDto
//         {
//             Id = x.Id,
//             GameUsers = x.GameUsers.Where(y => new UserByIdSpec(_userId).Not().Embed() (y.User))
//                 .Select(y => new GameUserDto
//                 {
//                     Id = y.Id,
//                     GameId = y.GameId,
//                     UserId = y.UserId,
//                     User = new UserDto
//                     {
//                         Id = y.User.Id,
//                         EmailAddress = y.User.EmailAddress,
//                         FirstName = y.User.FirstName,
//                         LastName = y.User.LastName,
//                         Username = y.User.Username
//                     }
//                 })
//                 .ToList(),
//             DateCreated = x.DateCreated,
//             GameAnagrams = x.GameAnagrams.Select(y => new GameAnagramDto
//             {
//                 Id = y.Id,
//                 GameAnagramTypeId = y.GameAnagramTypeId,
//                 AnagramWord = y.AnagramWord,
//                 DateCreated = y.DateCreated,
//                 GameAnagramType = new GameAnagramTypeDto
//                 {
//                     Id = y.GameAnagramTypeId,
//                     MaxAttempts = y.GameAnagramType.MaxAttempts,
//                     TimeAllowed = y.GameAnagramType.TimeAllowed,
//                     Title = y.GameAnagramType.Title
//                 },
//                 Order = y.Order,
//                 WordId = y.WordId,
//                 Word = new WordDto
//                 {
//                     Id = y.Word.Id,
//                     Description = y.Word.Description,
//                     ImageLocation = y.Word.ImageLocation,
//                     Title = y.Word.Title
//                 }
//             }).ToList(),
//             Title = x.Title,
//             TotalAnagrams = x.GameAnagrams.Count
//         };
// }