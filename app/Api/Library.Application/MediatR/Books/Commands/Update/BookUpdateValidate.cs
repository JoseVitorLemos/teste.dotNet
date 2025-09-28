using FluentValidation;
using Library.Shared.Messages;
using Library.Shared.Validators;

namespace Library.Application.MediatR.Books.Commands.Update;

public class BookUpdateValidate : AbstractValidator<BookUpdateCommand>
{
    public BookUpdateValidate()
    {
        RuleFor(x => x.Id)
            .Must(StringValidators.IsGuid)
            .WithMessage(FluentValidationMessages.INVALID_VALUE);

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