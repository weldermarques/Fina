using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Transactions;

public class GetTransactionByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Transactions: GetById")
            .WithSummary("Busca uma transação")
            .WithDescription("Busca uma transação")
            .WithOrder(4)
            .Produces<Response<Transaction?>>();

    private async static Task<IResult> HandleAsync(
        ITransactionHandler handler, 
        long id)
    {
        var request = new GetTransactionByIdRequest()
        {
            Id = id,
            UserId = ApiConfiguration.UserId
        };
        var response = await handler.GetByIdAsync(request);
        
        return response.IsSuccess ?
            TypedResults.Ok(response) :
            TypedResults.BadRequest(response);
    }
}
