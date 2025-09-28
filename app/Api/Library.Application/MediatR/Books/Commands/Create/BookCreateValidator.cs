using FluentValidation;
using Library.Shared.Messages;

namespace Library.Application.MediatR.Books.Commands.Create;

public class BookCreateValidator : AbstractValidator<BookCreateCommand>
{
    public BookCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(FluentValidationMessages.NOT_EMPTY);

        RuleFor(x => x.Author)
            .NotEmpty()
            .WithMessage(FluentValidationMessages.NOT_EMPTY);

        RuleFor(x => x.Summary)
            .NotEmpty()
            .WithMessage(FluentValidationMessages.NOT_EMPTY);
    }
}