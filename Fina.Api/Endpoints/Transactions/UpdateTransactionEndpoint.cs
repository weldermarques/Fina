﻿using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Transactions;

public class UpdateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Transactions: Update")
            .WithSummary("Atualiza uma transação")
            .WithDescription("Atualiza uma transação")
            .WithOrder(2)
            .Produces<Response<Transaction?>>();

    private async static Task<IResult> HandleAsync(
        ITransactionHandler handler, 
        UpdateTransactionRequest request, 
        long id)
    {
        request.UserId = ApiConfiguration.UserId;
        request.Id = id;
        var response = await handler.UpdateAsync(request);
        
        return response.IsSuccess?
            TypedResults.Ok(response) :
            TypedResults.BadRequest(response);
    }
}
 