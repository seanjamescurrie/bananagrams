using FluentValidation;

namespace Bananagrams.Api.ViewModels.Users;

public class CreateUserViewModel
{
    public string EmailAddress { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Username { get; set; }
}

public class CreateUserValidator : AbstractValidator<CreateUserViewModel>
{
    public CreateUserValidator()
    {
        RuleFor(user => user.FirstName)
            .NotNull().WithMessage("First name must be entered")
            .Length(2, 100).WithMessage("First name must be between 2 and 100 characters in length");
        RuleFor(user => user.LastName)
            .NotNull().WithMessage("Last name must be entered")
            .Length(2, 100).WithMessage("Last name must be between 2 and 100 characters in length");
        RuleFor(user => user.Username)
            .NotNull().WithMessage("Username must be entered")
            .Length(2, 100).WithMessage("Username must be between 2 and 100 characters in length");
        RuleFor(acc => acc.EmailAddress)
            .EmailAddress().WithMessage("Invalid email address")
            .NotNull().WithMessage("Email must be entered");
        RuleFor(acc => acc.Password)
            .NotNull().WithMessage("Password must be entered")
            .Length(8, 30).WithMessage("Password must be between 8 and 30 characters");
    }
}