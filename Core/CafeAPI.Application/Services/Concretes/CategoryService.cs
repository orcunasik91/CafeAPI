using AutoMapper;
using CafeAPI.Application.Dtos.CategoryDtos;
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
    public async Task<List<ResultCategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var result = _mapper.Map<List<ResultCategoryDto>>(categories);
        return result;
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