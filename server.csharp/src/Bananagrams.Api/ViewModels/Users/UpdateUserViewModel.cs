using FluentValidation;

namespace Bananagrams.Api.ViewModels.Users;

public class UpdateUserViewModel
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Password { get; set; }
    public string? Username { get; set; }
}

public class UpdateUserValidator : AbstractValidator<UpdateUserViewModel>
{
    public UpdateUserValidator()
    {
        RuleFor(acc => acc)
            .Must(acc => acc.FirstName != null && acc.LastName != null && acc.Password != null && acc.Username != null)
            .WithMessage("At least one value required")
            .WithName("NoValue");
        
        When(acc => acc.FirstName != null, () =>
        {
            RuleFor(acc => acc.FirstName)
                .Length(2, 100)
                .WithMessage("First name must be between 2 and 100 characters in length");
        });
        
        When(acc => acc.LastName != null, () =>
        {
            RuleFor(acc => acc.LastName)
                .Length(2, 100)
                .WithMessage("Last name must be between 2 and 100 characters in length");
        });

        When(acc => acc.Password != null, () =>
        {
            RuleFor(acc => acc.Password)
                .Length(8, 30)
                .WithMessage("Password must be between 8 and 30 characters in length");
        });

        When(acc => acc.Username != null, () =>
        {
            RuleFor(acc => acc.Username)
                .Length(2, 30)
                .WithMessage("Username must be between 2 and 30 characters in length");
        });
    }
}