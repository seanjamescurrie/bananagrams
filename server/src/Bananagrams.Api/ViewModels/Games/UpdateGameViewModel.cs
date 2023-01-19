using FluentValidation;

namespace Bananagrams.Api.Models.Games;

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
            .NotEmpty();
        RuleFor(game => game.DatePlayed)
            .NotNull()
            .NotEmpty();
    }
}