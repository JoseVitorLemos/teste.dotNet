using MediatR;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Shared.Extensions;

namespace Library.Application.MediatR.Books.Commands.Update;

public class BookUpdateHandler(IBookRepository bookRepository) : IRequestHandler<BookUpdateCommand, Unit>
{
    private readonly IBookRepository _bookRepository = bookRepository;

    public async Task<Unit> Handle(BookUpdateCommand command, CancellationToken cancellationToken)
    {
        await Book.ExistsBookByName(_bookRepository, command.Name);
        await Book.Update(command.Id.GuidParse(), command.Name, command.Author, command.Summary, _bookRepository);
        return Unit.Value;
    }
}