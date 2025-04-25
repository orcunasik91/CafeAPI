using CafeAPI.Application.Dtos.CategoryDtos;
using CafeAPI.Application.Dtos.MenuItemDtos;

namespace CafeAPI.Application.Services.Abstracts;
public interface IMenuItemService
{
    Task<List<ResultMenuItemDto>> GetAllMenuItemsAsync();
    Task<DetailMenuItemDto> GetByIdMenuItemAsync(int id);
    Task AddMenuItemAsync(CreateMenuItemDto menuItemDto);
    Task UpdateMenuItemAsync(UpdateMenuItemDto menuItemDto);
    Task RemoveMenuItemAsync(int id);
}