using FluentValidation;

namespace Bananagrams.Api.Models.Games;

public class CreateGameViewModel
{
    public int GameAnagramType { get; set; }
    public int PlayerOneId { get; set; }
    public int? PlayerTwoId { get; set; }
    public string Title { get; set; }
    public int TotalAnagrams { get; set; }
}

public class CreateGameValidator : AbstractValidator<CreateGameViewModel>
{
    public CreateGameValidator()
    {
        RuleFor(game => game.TotalAnagrams)
            .NotNull()
            .NotEmpty()
            .WithMessage("Please select the number of anagrams for the game");
    }
}