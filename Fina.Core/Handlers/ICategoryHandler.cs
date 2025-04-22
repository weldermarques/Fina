using Fina.Core.Models;
using Fina.Core.Requests;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;

namespace Fina.Core.Handlers;

public interface ICategoryHandler
{
    Task<Response<Category?>> CreateAsync(CreateCategoryRequest createCategoryRequest);
    Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest updateCategoryRequest);
    Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest deleteCategoryRequest);
    Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest getCategoryByIdRequest);
    Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest getAllCategoriesRequest);
}
