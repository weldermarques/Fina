using Fina.Api.Common.Api;
using Fina.Core;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.Endpoints.Transactions;

public class GetTransactionsByPeriodEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Transactions: GetByPeriod")
            .WithSummary("Lista todas as trasações por periodo")
            .WithDescription("Lista todas as trasações por periodo")
            .WithOrder(5)
            .Produces<PagedResponse<List<Transaction>?>>();

    private async static Task<IResult> HandleAsync(
        ITransactionHandler handler, 
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetTransactionsByPeriodRequest()
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            StartDate = startDate,
            EndDate = endDate,
            UserId = ApiConfiguration.UserId
        };
        var response = await handler.GetByPeriodAsync(request);
        
        return response.IsSuccess ?
            TypedResults.Ok(response) :
            TypedResults.BadRequest(response);
    }
}
