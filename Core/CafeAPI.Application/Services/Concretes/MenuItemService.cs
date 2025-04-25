using AutoMapper;
using CafeAPI.Application.Dtos.CategoryDtos;
using CafeAPI.Application.Dtos.MenuItemDtos;
using CafeAPI.Application.Interfaces;
using CafeAPI.Application.Services.Abstracts;
using CafeAPI.Domain.Entities;

namespace CafeAPI.Application.Services.Concretes;
public class MenuItemService : IMenuItemService
{
    private readonly IRepository<MenuItem> _menuItemRepository;
    private readonly IMapper _mapper;
    public MenuItemService(IRepository<MenuItem> menuItemRepository, IMapper mapper)
    {
        _menuItemRepository = menuItemRepository;
        _mapper = mapper;
    }
    public async Task AddMenuItemAsync(CreateMenuItemDto menuItemDto)
    {
        var menuItem = _mapper.Map<MenuItem>(menuItemDto);
        await _menuItemRepository.AddAsync(menuItem);
    }

    public async Task<List<ResultMenuItemDto>> GetAllMenuItemsAsync()
    {
        var menuItems = await _menuItemRepository.GetAllAsync();
        var result = _mapper.Map<List<ResultMenuItemDto>>(menuItems);
        return result;
    }

    public async Task<DetailMenuItemDto> GetByIdMenuItemAsync(int id)
    {
        var menuItem = await _menuItemRepository.GetByIdAsync(id);
        var result = _mapper.Map<DetailMenuItemDto>(menuItem);
        return result;
    }

    public async Task RemoveMenuItemAsync(int id)
    {
        var menuItem = await _menuItemRepository.GetByIdAsync(id);
        await _menuItemRepository.RemoveAsync(menuItem);
    }

    public async Task UpdateMenuItemAsync(UpdateMenuItemDto menuItemDto)
    {
        var menuItem = _mapper.Map<MenuItem>(menuItemDto);
        await _menuItemRepository.UpdateAsync(menuItem);
    }
}