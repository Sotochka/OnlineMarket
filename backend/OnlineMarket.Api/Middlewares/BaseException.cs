using System.Net;

namespace OnlineMarket.Api.Middlewares;

public class BaseException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
    : Exception(message)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
}