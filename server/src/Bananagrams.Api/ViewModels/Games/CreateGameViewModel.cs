using FluentValidation;

namespace Bananagrams.Api.ViewModels.Games;

public class CreateGameViewModel
{
    public int GameAnagramTypeId { get; set; }
    public int[] PlayerIds { get; set; }
    public string Title { get; set; }
    public int TotalAnagrams { get; set; }
}

public class CreateGameValidator : AbstractValidator<CreateGameViewModel>
{
    // add validation for other fields
    public CreateGameValidator()
    {
        RuleFor(game => game.GameAnagramTypeId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select game type");
        RuleFor(game => game.Title)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a title for the game");
        RuleFor(game => game.TotalAnagrams)
            .NotNull()
            .NotEmpty()
            .WithMessage("Please select the number of anagrams for the game");
    }
}