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
    public CreateGameValidator()
    {
        RuleFor(game => game.TotalAnagrams)
            .NotNull()
            .NotEmpty()
            .WithMessage("Please select the number of anagrams for the game");
    }
}