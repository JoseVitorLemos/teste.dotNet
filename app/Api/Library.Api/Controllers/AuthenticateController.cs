using Library.Application.MediatR.Authenticate.Commands.SignIn;
using Library.Application.MediatR.Authenticate.Commands.SignUp;
using Library.Application.MediatR.Books.Commands.Create;
using Library.Application.MediatR.Books.Commands.Delete;
using Library.Application.MediatR.Books.Commands.Update;
using Library.Application.MediatR.Books.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticateController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Lista todos os livros cadastrados na Library.
    /// </summary>
    /// <param name="query">
    /// Parâmetros de consulta para paginação e filtro:
    /// <list type="bullet">
    /// <item><description><c>Page</c> – número da página (padrão: 1).</description></item>
    /// <item><description><c>PageSize</c> – quantidade de registros por página (padrão: 50).</description></item>
    /// <item><description><c>OrderBy</c> – ordenação dos resultados ("asc" ou "desc").</description></item>
    /// <item><description><c>Name</c> – filtro opcional pelo nome do livro.</description></item>
    /// </list>
    /// </param>
    /// <returns>
    /// Retorna um <see cref="OkObjectResult"/> contendo um objeto do tipo 
    /// <see cref="PagedResult{BooksGetAllResult}"/> com os dados paginados dos livros,
    /// incluindo informações de total de registros e número de páginas.
    /// </returns>
    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn([FromBody] SignInCommand query)
        => Ok(await _mediator.Send(query));

    /// <summary>
    /// Cadastrar um novo livro na Library.
    /// </summary>
    /// <param name="command">
    /// Objeto contendo os dados necessários para criar o livro,
    /// incluindo <see cref="BookCreateCommand.Name"/>, 
    /// <see cref="BookCreateCommand.Author"/> e 
    /// <see cref="BookCreateCommand.Summary"/>.
    /// </param>
    /// <returns>
    /// Retorna um <see cref="CreatedAtActionResult"/>.
    /// O objeto retornado contém o identificador único do livro.
    /// </returns>
    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
        => CreatedAtAction(nameof(SignUp), await _mediator.Send(command));
}