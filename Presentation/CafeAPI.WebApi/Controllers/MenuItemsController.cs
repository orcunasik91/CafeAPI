using CafeAPI.Application.Dtos.MenuItemDtos;
using CafeAPI.Application.Dtos.ResponseDtos;
using CafeAPI.Application.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CafeAPI.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MenuItemsController : BaseController
{
    private readonly IMenuItemService _menuItemService;

    public MenuItemsController(IMenuItemService menuItemService)
    {
        _menuItemService = menuItemService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllMenuItems()
    {
        var result = await _menuItemService.GetAllMenuItemsAsync();
        return CreateResponse(result);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMenuItemById([FromRoute(Name = "id")] int id)
    {
        var result = await _menuItemService.GetByIdMenuItemAsync(id);
        return CreateResponse(result);
    }
    [HttpPost]
    public async Task<IActionResult> CreateOneMenuItem([FromBody] CreateMenuItemDto menuItemDto)
    {
        var result = await _menuItemService.AddMenuItemAsync(menuItemDto);
        return CreateResponse(result);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateOneMenuItem([FromBody] UpdateMenuItemDto menuItemDto)
    {
        var result =await _menuItemService.UpdateMenuItemAsync(menuItemDto);
        return CreateResponse(result);
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOneMenuItem([FromRoute(Name = "id")] int id)
    {
        var result = await _menuItemService.RemoveMenuItemAsync(id);
        return CreateResponse(result);
    }
}
