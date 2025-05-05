using CafeAPI.Application.Dtos.MenuItemDtos;
using CafeAPI.Application.Dtos.ResponseDtos;
using CafeAPI.Application.Services.Abstracts;
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
        if (!result.Success)
        {
            if (result.ErrorCodes == ErrorCodes.NotFound)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        return Ok(result);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMenuItemById([FromRoute(Name = "id")] int id)
    {
        var result = await _menuItemService.GetByIdMenuItemAsync(id);
        if (!result.Success)
        {
            if (result.ErrorCodes == ErrorCodes.NotFound)
                return Ok(result);
            return BadRequest(result);
        }
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> CreateOneMenuItem([FromBody] CreateMenuItemDto menuItemDto)
    {
        var result = await _menuItemService.AddMenuItemAsync(menuItemDto);
        if (!result.Success)
        {
            if (result.ErrorCodes is ErrorCodes.ValidationError or ErrorCodes.NotFound)
                return Ok(result);
            return BadRequest(result);
        }
        return StatusCode(201, result);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateOneMenuItem([FromBody] UpdateMenuItemDto menuItemDto)
    {
        var result =await _menuItemService.UpdateMenuItemAsync(menuItemDto);
        if (!result.Success)
        {
            if (result.ErrorCodes is ErrorCodes.NotFound or ErrorCodes.ValidationError)
                return Ok(result);
            return BadRequest(result);
        }
        return StatusCode(200, result);
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOneMenuItem([FromRoute(Name = "id")] int id)
    {
        var result = await _menuItemService.RemoveMenuItemAsync(id);
        if (!result.Success)
        {
            if (result.ErrorCodes == ErrorCodes.NotFound)
                return Ok(result);
            return BadRequest(result);
        }
        return Ok(result);
    }
}
