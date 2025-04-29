using AutoMapper;
using CafeAPI.Application.Dtos.CategoryDtos;
using CafeAPI.Application.Dtos.ResponseDtos;
using CafeAPI.Application.Interfaces;
using CafeAPI.Application.Services.Abstracts;
using CafeAPI.Domain.Entities;

namespace CafeAPI.Application.Services.Concretes;
public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;
    public CategoryService(IRepository<Category> categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    public async Task AddCategoryAsync(CreateCategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        await _categoryRepository.AddAsync(category);
    }
    public async Task<ResponseDto<List<ResultCategoryDto>>> GetAllCategoriesAsync()
    {
        try
        {
            var categories = await _categoryRepository.GetAllAsync();
            if (categories.Count == 0)
                return new ResponseDto<List<ResultCategoryDto>> { Success = false, Message = "Kategori Bulunamadı", ErrorCodes = ErrorCodes.NotFound };
            var result = _mapper.Map<List<ResultCategoryDto>>(categories);
            return new ResponseDto<List<ResultCategoryDto>> { Success = true, Data = result };
        }
        catch (Exception ex)
        {
            return new ResponseDto<List<ResultCategoryDto>> { Success = false, Message = ex.Message, ErrorCodes = ErrorCodes.Exception };
        }
    }
    public async Task<DetailCategoryDto> GetByIdCategoryAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        var result = _mapper.Map<DetailCategoryDto>(category);
        return result;
    }
    public async Task RemoveCategoryAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        await _categoryRepository.RemoveAsync(category);
    }
    public async Task UpdateCategoryAsync(UpdateCategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        await _categoryRepository.UpdateAsync(category);
    }
}