using CafeAPI.Application.Dtos.CategoryDtos;
using CafeAPI.Application.Dtos.ResponseDtos;

namespace CafeAPI.Application.Services.Abstracts;
public interface ICategoryService
{
    Task<ResponseDto<List<ResultCategoryDto>>> GetAllCategoriesAsync();
    Task<ResponseDto<DetailCategoryDto>> GetByIdCategoryAsync(int id);
    Task<ResponseDto<object>> AddCategoryAsync(CreateCategoryDto categoryDto);
    Task<ResponseDto<object>> UpdateCategoryAsync(UpdateCategoryDto categoryDto);
    Task<ResponseDto<object>> RemoveCategoryAsync(int id);
}