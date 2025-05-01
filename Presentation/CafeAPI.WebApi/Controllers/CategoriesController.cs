using CafeAPI.Application.Dtos.CategoryDtos;
using CafeAPI.Application.Dtos.ResponseDtos;
using CafeAPI.Application.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CafeAPI.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var result = await _categoryService.GetAllCategoriesAsync();
        if (!result.Success)
        {
            if(result.ErrorCodes == ErrorCodes.NotFound)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        return Ok(result);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCategoryById([FromRoute(Name = "id")] int id)
    {
        var result = await _categoryService.GetByIdCategoryAsync(id);
        if (!result.Success)
        {
            if (result.ErrorCodes == ErrorCodes.NotFound)
                return Ok(result);
            return BadRequest(result);
        }
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> CreateOneCategory([FromBody] CreateCategoryDto categoryDto)
    {
        var result = await _categoryService.AddCategoryAsync(categoryDto);
        if (!result.Success)
            return BadRequest(result);
        return StatusCode(201, result);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateOneCategory([FromBody] UpdateCategoryDto categoryDto)
    {
        var result = await _categoryService.UpdateCategoryAsync(categoryDto);
        if (!result.Success)
        {
            if (result.ErrorCodes == ErrorCodes.NotFound)
                return Ok(result);
            return BadRequest(result);
        }
        return StatusCode(200, result);
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOneCategory([FromRoute(Name = "id")]int id)
    {
        var result = await _categoryService.RemoveCategoryAsync(id);
        if (!result.Success)
        {
            if (result.ErrorCodes == ErrorCodes.NotFound)
                return Ok(result);
            return BadRequest(result);
        }
        return Ok(result);
    }
}