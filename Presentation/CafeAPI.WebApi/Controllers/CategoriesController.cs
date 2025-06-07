using CafeAPI.Application.Dtos.CategoryDtos;
using CafeAPI.Application.Dtos.ResponseDtos;
using CafeAPI.Application.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CafeAPI.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : BaseController
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
        return CreateResponse(result);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCategoryById([FromRoute(Name = "id")] int id)
    {
        var result = await _categoryService.GetByIdCategoryAsync(id);
        return CreateResponse(result);
    }
    [HttpPost]
    public async Task<IActionResult> CreateOneCategory([FromBody] CreateCategoryDto categoryDto)
    {
        var result = await _categoryService.AddCategoryAsync(categoryDto);
        return CreateResponse(result);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateOneCategory([FromBody] UpdateCategoryDto categoryDto)
    {
        var result = await _categoryService.UpdateCategoryAsync(categoryDto);
        return CreateResponse(result);
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOneCategory([FromRoute(Name = "id")]int id)
    {
        var result = await _categoryService.RemoveCategoryAsync(id);
        return CreateResponse(result);
    }
}