using System.Net;
namespace BookHaven.Domain.Exceptions.FileUploadExceptions;
public class EmptyFileException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage => "Cannot Upload Empty File.";
    public string Title => "Error";
}
