using CafeAPI.Application.Dtos.CategoryDtos;
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
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCategoryById([FromRoute(Name = "id")] int id)
    {
        var category = await _categoryService.GetByIdCategoryAsync(id);
        if (category is null)
            return NotFound();
        return Ok(category);
    }
    [HttpPost]
    public async Task<IActionResult> CreateOneCategory([FromBody] CreateCategoryDto categoryDto)
    {
        await _categoryService.AddCategoryAsync(categoryDto);
        return StatusCode(201, categoryDto);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateOneCategory([FromBody] UpdateCategoryDto categoryDto)
    {
        await _categoryService.UpdateCategoryAsync(categoryDto);
        return Ok("Kategori Başarı ile Güncellendi");
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOneCategory([FromRoute(Name = "id")]int id)
    {
        await _categoryService.RemoveCategoryAsync(id);
        return NoContent();
    }
}