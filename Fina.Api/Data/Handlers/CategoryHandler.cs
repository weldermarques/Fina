using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Fina.Api.Data.Handlers;

public class CategoryHandler(AppDbContext context) : ICategoryHandler
{
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        try
        {
            var category = new Category
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
            };

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category, 201, "Categoria criada com sucesso");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return new Response<Category?>(null, 500, "Erro ao criar categoria");
        }
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category = await context
                .Categories
                .FirstOrDefaultAsync(s 
                    => s.Id == request.Id && 
                       s.UserId == request.UserId);

            if (category is null)
                return new Response<Category?>(null, 404, "Categoria não encontrada");
            
            category.Title = request.Title;
            category.Description = request.Description;
            
            context.Categories.Update(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category, message: "Categoria atualizada com sucesso");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return new Response<Category?>(null, 500, "Não foi possível atualizar categoria");
        }
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var category = await context
                .Categories
                .FirstOrDefaultAsync(s 
                    => s.Id == request.Id && 
                       s.UserId == request.UserId);

            if (category is null)
                return new Response<Category?>(null, 404, "Categoria não encontrada");
            
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
   
            return new Response<Category?>(category, message: "Categoria removida com sucesso");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return new Response<Category?>(null, 500, "Erro ao remover categoria");
        }
    }

    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            var category = await context
                .Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(s 
                    => s.Id == request.Id && 
                       s.UserId == request.UserId);

            return category is null ? 
                new Response<Category?>(null, 404, "Categoria não encontrada") : 
                new Response<Category?>(category);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return new Response<Category?>(null, 500, "Erro ao buscar categoria");
        }
    }

    public async Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoriesRequest request)
    {
        try
        {
            var query = context
                .Categories
                .AsNoTracking()
                .Where(s => s.UserId == request.UserId)
                .OrderBy(s => s.Title);

            var categories = await query
                .Skip((request.PageNumber -1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();

            return new PagedResponse<List<Category>?>(categories, 
                count, request.PageNumber, request.PageSize);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return new PagedResponse<List<Category>?>(null, code: 500, message: "Erro ao obter categorias");
        }
    }
}
