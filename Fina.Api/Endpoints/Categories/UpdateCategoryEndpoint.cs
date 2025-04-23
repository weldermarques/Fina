﻿using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.Endpoints.Categories;

public class UpdateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Categories: Update")
            .WithSummary("Atualiza uma categoria")
            .WithDescription("Atualiza uma categoria")
            .WithOrder(2)
            .Produces<Response<Category?>>();

    private async static Task<IResult> HandleAsync(
        ICategoryHandler handler, 
        UpdateCategoryRequest request, 
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
