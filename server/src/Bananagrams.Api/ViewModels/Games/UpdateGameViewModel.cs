using FluentValidation;

namespace Bananagrams.Api.ViewModels.Games;

public class UpdateGameViewModel
{
    public int Attempts { get; set; }
    public string Attempt { get; set; }
}

public class UpdateGameValidator : AbstractValidator<UpdateGameViewModel>
{
    public UpdateGameValidator()
    {
        RuleFor(game => game.Attempts)
            .NotNull()
            .NotEmpty()
            .WithMessage("Attempts must not be empty.");
        RuleFor(game => game.Attempt)
            .NotNull()
            .NotEmpty()
            .WithMessage("Attempt must not be empty.");
    }
}