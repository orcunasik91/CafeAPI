using CafeAPI.Application.Dtos.CategoryDtos;
using CafeAPI.Application.Dtos.MenuItemDtos;
using CafeAPI.Application.Services.Abstracts;
using CafeAPI.Application.Services.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace CafeAPI.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MenuItemsController : ControllerBase
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
        return Ok(result);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMenuItemById([FromRoute(Name = "id")] int id)
    {
        var result = await _menuItemService.GetByIdMenuItemAsync(id);
        if (result is null)
            return NotFound();
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> CreateOneMenuItem([FromBody] CreateMenuItemDto menuItemDto)
    {
        await _menuItemService.AddMenuItemAsync(menuItemDto);
        return StatusCode(201, menuItemDto);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateOneMenuItem([FromBody] UpdateMenuItemDto menuItemDto)
    {
        await _menuItemService.UpdateMenuItemAsync(menuItemDto);
        return Ok("MenuItem Başarı ile Güncellendi");
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOneMenuItem([FromRoute(Name = "id")] int id)
    {
        await _menuItemService.RemoveMenuItemAsync(id);
        return NoContent();
    }
}
