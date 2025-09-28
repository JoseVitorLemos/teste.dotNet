using MediatR;
using Library.Domain.Entities;
using Library.Domain.Interfaces;

namespace Library.Application.MediatR.Books.Commands.Create;

public class BookCreateHandler(IBookRepository bookRepository) : IRequestHandler<BookCreateCommand, BookCreateResult>
{
    private readonly IBookRepository _bookRepository = bookRepository;

    public async Task<BookCreateResult> Handle(BookCreateCommand command, CancellationToken cancellationToken)
    {
        await Book.ExistsBookByName(_bookRepository, command.Name);
        var entity = await _bookRepository.Insert(command);
        return new(entity.Id);
    }
}