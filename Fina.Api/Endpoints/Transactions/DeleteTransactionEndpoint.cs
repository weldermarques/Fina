﻿using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Transactions;

public class DeleteTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Transactions: Delete")
            .WithSummary("Remove uma transação")
            .WithDescription("Remove uma transação")
            .WithOrder(3)
            .Produces<Response<Transaction?>>();

    private async static Task<IResult> HandleAsync(
        ITransactionHandler handler, 
        long id)
    {
        var request = new DeleteTransactionRequest
        {
            UserId = ApiConfiguration.UserId,
            Id = id
        };
        
        var response = await handler.DeleteAsync(request);
        
        return response.IsSuccess?
            TypedResults.Ok(response) :
            TypedResults.BadRequest(response);
    }
}
