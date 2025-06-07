using AutoMapper;
using CafeAPI.Application.Dtos.MenuItemDtos;
using CafeAPI.Application.Dtos.ResponseDtos;
using CafeAPI.Application.Interfaces;
using CafeAPI.Application.Services.Abstracts;
using CafeAPI.Domain.Entities;
using FluentValidation;

namespace CafeAPI.Application.Services.Concretes;
public class MenuItemService : IMenuItemService
{
    private readonly IRepository<MenuItem> _menuItemRepository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateMenuItemDto> _createMenuItemValidator;
    private readonly IValidator<UpdateMenuItemDto> _updateMenuItemValidator;
    public MenuItemService(IRepository<MenuItem> menuItemRepository, IMapper mapper, IValidator<CreateMenuItemDto> createMenuItemValidator, IValidator<UpdateMenuItemDto> updateMenuItemValidator, IRepository<Category> categoryRepository)
    {
        _menuItemRepository = menuItemRepository;
        _mapper = mapper;
        _createMenuItemValidator = createMenuItemValidator;
        _updateMenuItemValidator = updateMenuItemValidator;
        _categoryRepository = categoryRepository;
    }
    public async Task<ResponseDto<MenuItemResponseDto>> AddMenuItemAsync(CreateMenuItemDto menuItemDto)
    {
        try
        {
            var validate = await _createMenuItemValidator.ValidateAsync(menuItemDto);
            if (!validate.IsValid)
            {
                return new ResponseDto<MenuItemResponseDto>
                {
                    Success = false,
                    Data = null,
                    Message = string.Join(" | ", validate.Errors.Select(c => c.ErrorMessage)),
                    ErrorCode = ErrorCodes.ValidationError
                };
            }
            var checkCategoryId = await _categoryRepository.GetByIdAsync(menuItemDto.CategoryId);
            if (checkCategoryId is null)
                return new ResponseDto<MenuItemResponseDto> { Success = false, 
                    Data = null, Message = "Eklemek İstediğiniz Kategori Bulunamadı!",
                ErrorCode = ErrorCodes.NotFound};
            var menuItem = _mapper.Map<MenuItem>(menuItemDto);
            await _menuItemRepository.AddAsync(menuItem);
            var responseDtoData = _mapper.Map<MenuItemResponseDto>(menuItem);
            responseDtoData.CategoryId = checkCategoryId.Id;
            responseDtoData.CategoryName = checkCategoryId.Name;
            return new ResponseDto<MenuItemResponseDto> { Success = true, Data = responseDtoData, Message = $"{responseDtoData.Name} isimli menü başarı ile oluşturuldu" };
        }
        catch (Exception)
        {
            return new ResponseDto<MenuItemResponseDto> { Success = false, Message = "Bir Hata Oluştu", ErrorCode = ErrorCodes.Exception };
        }
        
    }

    public async Task<ResponseDto<List<ResultMenuItemDto>>> GetAllMenuItemsAsync()
    {
        try
        {
            var menuItems = await _menuItemRepository.GetAllAsync();
            if(menuItems.Count == 0)
                return new ResponseDto<List<ResultMenuItemDto>> { Success = false, Message = "Menü Bulunamadı", ErrorCode = ErrorCodes.NotFound };
            var categories = await _categoryRepository.GetAllAsync();
            var result = _mapper.Map<List<ResultMenuItemDto>>(menuItems);
            return new ResponseDto<List<ResultMenuItemDto>> { Success = true, Data = result };
        }
        catch (Exception)
        {
            return new ResponseDto<List<ResultMenuItemDto>> { Success = false, Message = "Bir Hata Oluştu", ErrorCode = ErrorCodes.Exception };
        }
    }

    public async Task<ResponseDto<DetailMenuItemDto>> GetByIdMenuItemAsync(int id)
    {
        try
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(id);
            if (menuItem is null)
                return new ResponseDto<DetailMenuItemDto> { Success = false, Message = "Menü Bulunamadı", ErrorCode = ErrorCodes.NotFound };
            var category = await _categoryRepository.GetByIdAsync(menuItem.CategoryId);
            var result = _mapper.Map<DetailMenuItemDto>(menuItem);
            return new ResponseDto<DetailMenuItemDto> { Success = true, Data = result };
        }
        catch (Exception)
        {
            return new ResponseDto<DetailMenuItemDto> { Success = false, Message = "Bir Hata Oluştu", ErrorCode = ErrorCodes.Exception };
        }
    }

    public async Task<ResponseDto<object>> RemoveMenuItemAsync(int id)
    {
        try
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(id);
            if (menuItem is null)
                return new ResponseDto<object> { Success = false, Message = "Menü Bulunamadı", ErrorCode = ErrorCodes.NotFound };
            await _menuItemRepository.RemoveAsync(menuItem);
            return new ResponseDto<object> { Success = true, Message = $"{menuItem.Name} isimli Menü Silindi" };
        }
        catch (Exception)
        {
            return new ResponseDto<object> { Success = false, Message = "Bir Hata Oluştu", ErrorCode = ErrorCodes.Exception };
        }
    }

    public async Task<ResponseDto<object>> UpdateMenuItemAsync(UpdateMenuItemDto menuItemDto)
    {
        try
        {
            var validate = await _updateMenuItemValidator.ValidateAsync(menuItemDto);
            if (!validate.IsValid)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = string.Join(" | ", validate.Errors.Select(c => c.ErrorMessage)),
                    ErrorCode = ErrorCodes.ValidationError
                };
            }
            var checkCategoryId = await _categoryRepository.GetByIdAsync(menuItemDto.CategoryId);
            if (checkCategoryId is null)
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = menuItemDto,
                    Message = "Eklemek İstediğiniz Kategori Bulunamadı!",
                    ErrorCode = ErrorCodes.NotFound
                };
            var result = _mapper.Map<MenuItem>(menuItemDto);
            if (result is null)
                return new ResponseDto<object> { Success = false, Message = "Menü Bulunamadı", ErrorCode = ErrorCodes.NotFound };
            await _menuItemRepository.UpdateAsync(result);
            return new ResponseDto<object> { Success = true, Message = $"{result.Name} isimli Menü başarı ile güncellendi" };

        }
        catch (Exception)
        {
            return new ResponseDto<object> { Success = false, Message = "Bir Hata Oluştu", ErrorCode = ErrorCodes.Exception };

        }
    }
}