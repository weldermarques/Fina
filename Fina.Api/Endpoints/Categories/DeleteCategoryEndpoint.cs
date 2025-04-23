﻿using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Categories;

public class DeleteCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Categories: Delete")
            .WithSummary("Remove uma categoria")
            .WithDescription("Remove uma categoria")
            .WithOrder(3)
            .Produces<Response<Category?>>();

    private async static Task<IResult> HandleAsync(
        ICategoryHandler handler, 
        long id)
    {
        var request = new DeleteCategoryRequest
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
