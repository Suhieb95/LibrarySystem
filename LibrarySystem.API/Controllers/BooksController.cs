using LibrarySystem.API.Common.Constants;
using LibrarySystem.Application.Books;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.DTOs.Books;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.API.Controllers;
public class BooksController(IBookApplicationService _bookApplicationService) : BaseController
{
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetBooks([FromQuery] BookPaginationParam param, CancellationToken cancellationToken)
    {
        Result<PaginatedResponse<BookResponse>>? result = await _bookApplicationService.GetBooks(param, cancellationToken);
        return result.Map(
                      onSuccess: Ok,
                      onFailure: Problem);
    }
    [AllowAnonymous]
    [HttpGet(Books.GetById)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        Result<BookResponse>? result = await _bookApplicationService.GetBookById(id, cancellationToken);
        return result.Map(
                      onSuccess: Ok,
                      onFailure: Problem);
    }
    [HttpDelete(Books.DeleteBookImages)]
    [Authorized(Policy = CustomPolicies.ExcludeNewUserPolicy)]
    [HasPermission(Permission.Delete, EntityName.BookImages)]
    public async Task<IActionResult> DeleteBookImages(DeleteBookImageRequest request, CancellationToken cancellationToken)
    {
        Result<bool>? result = await _bookApplicationService.DeleteBookImages(request, cancellationToken);
        return result.Map(
                      onSuccess: _ => NoContent(),
                      onFailure: Problem);
    }
    [HttpPut(Books.UpdateBookImages)]
    [Authorized(Policy = CustomPolicies.ExcludeNewUserPolicy)]
    [HasPermission(Permission.Update, EntityName.BookImages)]
    public async Task<IActionResult> UpdateBookImages([FromForm] UpdateBookImageRequest request, CancellationToken cancellationToken)
    {
        Result<bool>? result = await _bookApplicationService.UpdateBookImages(request, cancellationToken);
        return result.Map(
                      onSuccess: _ => NoContent(),
                      onFailure: Problem);
    }
    [HttpDelete(Books.Delete)]
    [Authorized(Policy = CustomPolicies.ExcludeNewUserPolicy)]
    [HasPermission(Permission.Delete, EntityName.Books)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        Result<bool>? result = await _bookApplicationService.DeleteBook(id, cancellationToken);
        return result.Map(
                      onSuccess: _ => NoContent(),
                      onFailure: Problem);
    }
    [HttpPut]
    [Authorized(Policy = CustomPolicies.ExcludeNewUserPolicy)]
    [HasPermission(Permission.Update, EntityName.Books)]
    public async Task<IActionResult> Update(UpdateBookRequest request, CancellationToken cancellationToken)
    {
        Result<bool>? result = await _bookApplicationService.UpdateBook(request, cancellationToken);
        return result.Map(
                      onSuccess: _ => NoContent(),
                      onFailure: Problem);
    }
    [HttpPost]
    [Authorized(Policy = CustomPolicies.ExcludeNewUserPolicy)]
    [HasPermission(Permission.Create, EntityName.Books)]
    public async Task<IActionResult> Create([FromForm] CreateBookRequest request, CancellationToken cancellationToken)
    {
        Result<int>? result = await _bookApplicationService.CreateBook(request, cancellationToken);
        return result.Map(
                      onSuccess: _ => Ok(_),
                      onFailure: Problem);
    }
}