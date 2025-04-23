﻿using Fina.Api.Common.Api;
using Fina.Core;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.Endpoints.Categories;

public class GetAllCategoriesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Categories: GetAll")
            .WithSummary("Lista todas as categorias")
            .WithDescription("Lista todas as categorias")
            .WithOrder(5)
            .Produces<PagedResponse<List<Category>?>>();

    private async static Task<IResult> HandleAsync(
        ICategoryHandler handler, 
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllCategoriesRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            UserId = ApiConfiguration.UserId
        };
        var response = await handler.GetAllAsync(request);
        
        return response.IsSuccess ?
            TypedResults.Ok(response) :
            TypedResults.BadRequest(response);
    }
}
