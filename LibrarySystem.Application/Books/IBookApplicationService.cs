using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.DTOs.Books;
namespace LibrarySystem.Application.Books;
public interface IBookApplicationService
{
    public Task<Result<PaginatedResponse<BooksResponse>>> GetBooks(PaginationParam param, CancellationToken? cancellationToken = null);
    public Task<Result<Book>> GetBookById(int id, CancellationToken? cancellationToken = null);
}