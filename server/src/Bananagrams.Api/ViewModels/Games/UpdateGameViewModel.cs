using FluentValidation;

namespace Bananagrams.Api.ViewModels.Games;

public class UpdateGameViewModel
{
    public int Attempts { get; set; }
    public DateTime DatePlayed { get; set; }
    public DateTime? DateSolved { get; set; }
}

public class UpdateGameValidator : AbstractValidator<UpdateGameViewModel>
{
    public UpdateGameValidator()
    {
        RuleFor(game => game.Attempts)
            .NotNull()
            .NotEmpty()
            .WithMessage("Attempts must not be empty.");
        RuleFor(game => game.DatePlayed)
            .NotNull()
            .NotEmpty()
            .WithMessage("Date Played must not be empty.");
    }
}