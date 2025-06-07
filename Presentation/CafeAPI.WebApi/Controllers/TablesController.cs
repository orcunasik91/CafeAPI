using CafeAPI.Application.Dtos.ResponseDtos;
using CafeAPI.Application.Dtos.TableDtos;
using CafeAPI.Application.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace CafeAPI.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TablesController : BaseController
{
    private readonly ITableService _tableService;

    public TablesController(ITableService tableService)
    {
        _tableService = tableService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllTables()
    {
        var result = await _tableService.GetAllTablesAsync();
        return CreateResponse(result);
    }
    [HttpGet("GetActiveTables")]
    public async Task<IActionResult> GetAllActiveTables()
    {
        var result = await _tableService.GetAllActiveTablesAsync();
        return CreateResponse(result);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTableById([FromRoute(Name = "id")] int id)
    {
        var result = await _tableService.GetByIdTableAsync(id);
        return CreateResponse(result);
    }
    [HttpGet("GetTableByTableNumber/{tableNumber:int}")]
    public async Task<IActionResult> GetTableByTableNumber([FromRoute(Name = "tableNumber")] int tableNumber)
    {
        var result = await _tableService.GetByTableNumberTableAsync(tableNumber);
        return CreateResponse(result);
    }
    [HttpPost]
    public async Task<IActionResult> CreateOneTable([FromBody] CreateTableDto tableDto)
    {
        var result = await _tableService.AddTableAsync(tableDto);
        return CreateResponse(result);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateOneTable([FromBody] UpdateTableDto tableDto)
    {
        var result = await _tableService.UpdateTableAsync(tableDto);
        return CreateResponse(result);
    }
    [HttpPut("UpdateTableStatusByTableNumber/{tableNumber:int}")]
    public async Task<IActionResult> UpdateOneTableStatusByTableNumber([FromRoute(Name = "tableNumber")] int tableNumber)
    {
        var result = await _tableService.UpdateTableStatusByTableNumberAsync(tableNumber);
        return CreateResponse(result);
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOneTable([FromRoute(Name = "id")] int id)
    {
        var result = await _tableService.RemoveTableAsync(id);
        return CreateResponse(result);
    }
}