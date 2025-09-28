using Library.Application.MediatR.Books.Commands.Create;
using Library.Application.MediatR.Books.Commands.Delete;
using Library.Application.MediatR.Books.Commands.Update;
using Library.Application.MediatR.Books.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController(IMediator mediator) : ControllerBase
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
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery] BooksGetAllQuery query)
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
    [HttpPost]
    [Authorize("Administrator")]
    public async Task<IActionResult> Create([FromBody] BookCreateCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }

    /// <summary>
    /// Edita um livro existente
    /// </summary>
    /// <param name="command">
    /// Objeto contendo os dados necessários para atualziar um livro,
    /// incluindo <see cref="BookUpdateCommand.Id"/>, 
    /// <see cref="BookUpdateCommand.Name"/> e 
    /// <see cref="BookUpdateCommand.Author"/>.
    /// <see cref="BookUpdateCommand.Summary"/>.
    /// </param>
    /// <returns>
    /// Retorna um <see cref="NoContentResult"/>.
    /// Não possui um objeto retornado.
    /// </returns>
    [HttpPut]
    [Authorize("Administrator")]
    public async Task<IActionResult> Update([FromBody] BookUpdateCommand command)
    {
        var result = await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Exclui um livro existente
    /// </summary>
    /// <param name="command">
    /// Objeto contendo os dados necessários para atualziar um livro,
    /// incluindo <see cref="BookUpdateCommand.Id"/>, 
    /// <see cref="BookUpdateCommand.Name"/> e 
    /// <see cref="BookUpdateCommand.Author"/>.
    /// <see cref="BookUpdateCommand.Summary"/>.
    /// </param>
    /// <returns>
    /// Retorna um <see cref="NoContentResult"/>.
    /// Não possui um objeto retornado.
    /// </returns>
    [HttpDelete("{id}")]
    [Authorize("Administrator")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        await _mediator.Send(new BookDeleteCommand(id));
        return NoContent();
    }
}