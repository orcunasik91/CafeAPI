using CafeAPI.Application.Dtos.CategoryDtos;

namespace CafeAPI.Application.Services.Abstracts;
public interface ICategoryService
{
    Task<List<ResultCategoryDto>> GetAllCategoriesAsync();
    Task<DetailCategoryDto> GetByIdCategoryAsync(int id);
    Task AddCategoryAsync(CreateCategoryDto categoryDto);
    Task UpdateCategoryAsync(UpdateCategoryDto categoryDto);
    Task RemoveCategoryAsync(int id);
}