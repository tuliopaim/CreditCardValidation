using System.Net;
using CreditCardValidation.Commands;

namespace CreditCardValidation.Tests.IntegrationTests.Base;

public class HttpResult<TResponse>
{
    public HttpResult(
        HttpStatusCode statusCode,
        CommandResponse<TResponse>? successResponse,
        ErrorCommandResponse? errorResponse)
    {
        StatusCode = statusCode;
        SuccessResponse = successResponse;
        ErrorResponse = errorResponse;
    }

    public HttpStatusCode StatusCode { get; set; }
    public CommandResponse<TResponse>? SuccessResponse { get; set; }
    public ErrorCommandResponse? ErrorResponse { get; set; }
}
