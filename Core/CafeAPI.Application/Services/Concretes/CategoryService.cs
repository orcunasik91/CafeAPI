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
    public async Task<ResponseDto<object>> AddCategoryAsync(CreateCategoryDto categoryDto)
    {
        try
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.AddAsync(category);
            return new ResponseDto<object> { Success = true, Data = category, Message = $"{category.Name} başarı ile oluştruldu" };
        }
        catch (Exception)
        {
            return new ResponseDto<object> { Success = false, Message = "Bir Hata Oluştu", ErrorCodes = ErrorCodes.Exception };
        }
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
        catch (Exception)
        {
            return new ResponseDto<List<ResultCategoryDto>> { Success = false, Message = "Bir Hata Oluştu", ErrorCodes = ErrorCodes.Exception };
        }
    }
    public async Task<ResponseDto<DetailCategoryDto>> GetByIdCategoryAsync(int id)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
                return new ResponseDto<DetailCategoryDto> { Success = false, Message = "Kategori Bulunamadı", ErrorCodes = ErrorCodes.NotFound };
            var result = _mapper.Map<DetailCategoryDto>(category);
            return new ResponseDto<DetailCategoryDto> { Success = true, Data = result};
        }
        catch (Exception)
        {
            return new ResponseDto<DetailCategoryDto> { Success = false, Message = "Bir Hata Oluştu", ErrorCodes = ErrorCodes.Exception };
        }
    }
    public async Task<ResponseDto<object>> RemoveCategoryAsync(int id)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
                return new ResponseDto<object> { Success = false, Message = "Kategori Bulunamadı", ErrorCodes = ErrorCodes.NotFound };
            await _categoryRepository.RemoveAsync(category);
            return new ResponseDto<object> { Success = true, Message = $"{category.Name} isimli Kategori Silindi"};
        }
        catch (Exception)
        {
            return new ResponseDto<object> { Success = false, Message = "Bir Hata Oluştu", ErrorCodes = ErrorCodes.Exception };
        }
    }
    public async Task<ResponseDto<object>> UpdateCategoryAsync(UpdateCategoryDto categoryDto)
    {
        try
        {
            var result = await _categoryRepository.GetByIdAsync(categoryDto.Id);
            if (result is null)
                return new ResponseDto<object> { Success = false, Message = "Kategori Bulunamadı", ErrorCodes = ErrorCodes.NotFound };
            var category = _mapper.Map(categoryDto,result);
            await _categoryRepository.UpdateAsync(category);
            return new ResponseDto<object> { Success = true, Message = $"{category.Name} isimli kategori başarı ile güncellendi" };
        }
        catch (Exception)
        {
            return new ResponseDto<object> { Success = false, Message = "Bir Hata Oluştu", ErrorCodes = ErrorCodes.Exception};
        }
    }
}