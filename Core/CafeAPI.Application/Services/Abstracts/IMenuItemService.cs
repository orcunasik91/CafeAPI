using CafeAPI.Application.Dtos.CategoryDtos;
using CafeAPI.Application.Dtos.MenuItemDtos;
using CafeAPI.Application.Dtos.ResponseDtos;

namespace CafeAPI.Application.Services.Abstracts;
public interface IMenuItemService
{
    Task<ResponseDto<List<ResultMenuItemDto>>> GetAllMenuItemsAsync();
    Task<ResponseDto<DetailMenuItemDto>> GetByIdMenuItemAsync(int id);
    Task<ResponseDto<MenuItemResponseDto>> AddMenuItemAsync(CreateMenuItemDto menuItemDto);
    Task<ResponseDto<object>> UpdateMenuItemAsync(UpdateMenuItemDto menuItemDto);
    Task<ResponseDto<object>> RemoveMenuItemAsync(int id);
}