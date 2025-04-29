using CafeAPI.Application.Dtos.CategoryDtos;
using CafeAPI.Application.Dtos.ResponseDtos;

namespace CafeAPI.Application.Services.Abstracts;
public interface ICategoryService
{
    Task<ResponseDto<List<ResultCategoryDto>>> GetAllCategoriesAsync();
    Task<DetailCategoryDto> GetByIdCategoryAsync(int id);
    Task AddCategoryAsync(CreateCategoryDto categoryDto);
    Task UpdateCategoryAsync(UpdateCategoryDto categoryDto);
    Task RemoveCategoryAsync(int id);
}