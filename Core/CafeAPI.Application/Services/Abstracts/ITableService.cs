using CafeAPI.Application.Dtos.ResponseDtos;
using CafeAPI.Application.Dtos.TableDtos;

namespace CafeAPI.Application.Services.Abstracts;
public interface ITableService
{
    Task<ResponseDto<List<ResultTableDto>>> GetAllTablesAsync();
    Task<ResponseDto<List<ResultTableDto>>> GetAllActiveTablesAsync();
    Task<ResponseDto<DetailTableDto>> GetByIdTableAsync(int id);
    Task<ResponseDto<DetailTableDto>> GetByTableNumberTableAsync(int tableNumber);
    Task<ResponseDto<object>> AddTableAsync(CreateTableDto createTableDto);
    Task<ResponseDto<object>> UpdateTableAsync(UpdateTableDto updateTableDto);
    Task<ResponseDto<object>> UpdateTableStatusByTableNumberAsync(int tableNumber);
    Task<ResponseDto<object>> RemoveTableAsync(int id);
}