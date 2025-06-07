using AutoMapper;
using CafeAPI.Application.Dtos.ResponseDtos;
using CafeAPI.Application.Dtos.TableDtos;
using CafeAPI.Application.Interfaces;
using CafeAPI.Application.Services.Abstracts;
using CafeAPI.Domain.Entities;
using FluentValidation;

namespace CafeAPI.Application.Services.Concretes;
public class TableService : ITableService
{
    private readonly IRepository<Table> _repository;
    private readonly ITableRepository _tableRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateTableDto> _createTableValidator;
    private readonly IValidator<UpdateTableDto> _updateTableValidator;

    public TableService(IRepository<Table> repository, IMapper mapper, IValidator<CreateTableDto> createTableValidator, IValidator<UpdateTableDto> updateTableValidator, ITableRepository tableRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _createTableValidator = createTableValidator;
        _updateTableValidator = updateTableValidator;
        _tableRepository = tableRepository;
    }

    public async Task<ResponseDto<object>> AddTableAsync(CreateTableDto createTableDto)
    {
        try
        {
            var validate = await _createTableValidator.ValidateAsync(createTableDto);
            if (!validate.IsValid)
                return new ResponseDto<object> { Success = false, Data = null, Message = string.Join(", ", validate.Errors.Select(t => t.ErrorMessage)), ErrorCode = ErrorCodes.ValidationError };
            var checkTable = await _tableRepository.GetByTableNumberAsync(createTableDto.TableNumber);
            if (checkTable is not null)
                return new ResponseDto<object> { Success = false, Message = "Eklemek İstediğiniz Masa Numarası Zaten Kayıtlı", ErrorCode = ErrorCodes.DuplicateError };
            var table = _mapper.Map<Table>(createTableDto);
            await _repository.AddAsync(table);
            return new ResponseDto<object> { Success = true, Data = table, Message = $"{table.TableNumber} nolu Masa Başarı ile Oluşturuldu" };
        }
        catch (Exception)
        {
            return new ResponseDto<object> { Success = false, Message = "Bir Hata ile Karşılaşıldı!", ErrorCode = ErrorCodes.Exception };

        }
    }

    public async Task<ResponseDto<List<ResultTableDto>>> GetAllActiveTablesAsync()
    {
        try
        {
            var tables = await _tableRepository.GetActiveTablesAsync();
            if (tables.Count is 0)
                return new ResponseDto<List<ResultTableDto>> { Success = false, Message = "Aktif Masa Bulunamadı", ErrorCode = ErrorCodes.NotFound };
            var result = _mapper.Map<List<ResultTableDto>>(tables);
            return new ResponseDto<List<ResultTableDto>> { Success = true, Data = result };
        }
        catch (Exception)
        {
            return new ResponseDto<List<ResultTableDto>> { Success = false, Message = "Bir Hata ile Karşılaşıldı!", ErrorCode = ErrorCodes.Exception };
        }
    }

    public async Task<ResponseDto<List<ResultTableDto>>> GetAllTablesAsync()
    {
        try
        {
            var tables = await _repository.GetAllAsync();
            if (tables.Count is 0)
                return new ResponseDto<List<ResultTableDto>> { Success = false, Message = "Masalar Bulunamadı", ErrorCode = ErrorCodes.NotFound };
            var result = _mapper.Map<List<ResultTableDto>>(tables);
            return new ResponseDto<List<ResultTableDto>> { Success = true, Data = result };
        }
        catch (Exception)
        {
            return new ResponseDto<List<ResultTableDto>> { Success = false, Message = "Bir Hata ile Karşılaşıldı!", ErrorCode = ErrorCodes.Exception };
        }
    }

    public async Task<ResponseDto<DetailTableDto>> GetByIdTableAsync(int id)
    {
        try
        {
            var table = await _repository.GetByIdAsync(id);
            if (table is null)
                return new ResponseDto<DetailTableDto> { Success = false, Message = "Masa Bulunamadı", ErrorCode = ErrorCodes.NotFound };
            var result = _mapper.Map<DetailTableDto>(table);
            return new ResponseDto<DetailTableDto> { Success = true, Data = result };
        }
        catch (Exception)
        {
            return new ResponseDto<DetailTableDto> { Success = false, Message = "Bir Hata ile Karşılaşıldı!", ErrorCode = ErrorCodes.Exception };
        }
    }

    public async Task<ResponseDto<DetailTableDto>> GetByTableNumberTableAsync(int tableNumber)
    {
        try
        {
            var table = await _tableRepository.GetByTableNumberAsync(tableNumber);
            if (table is null)
                return new ResponseDto<DetailTableDto> { Success = false, Message = "Masa Bulunamadı", ErrorCode = ErrorCodes.NotFound };
            var result = _mapper.Map<DetailTableDto>(table);
            return new ResponseDto<DetailTableDto> { Success = true, Data = result };
        }
        catch (Exception)
        {
            return new ResponseDto<DetailTableDto> { Success = false, Message = "Bir Hata Meydana Geldi", ErrorCode = ErrorCodes.Exception };
        }
    }

    public async Task<ResponseDto<object>> RemoveTableAsync(int id)
    {
        try
        {
            var table = await _repository.GetByIdAsync(id);
            if (table is null)
                return new ResponseDto<object> { Success = false, Message = "Masa Bulunamadı", ErrorCode = ErrorCodes.NotFound };
            await _repository.RemoveAsync(table);
            return new ResponseDto<object> { Success = true, Message = $"{table.TableNumber} Başarı ile Kaldırıldı" };
        }
        catch (Exception)
        {
            return new ResponseDto<object> { Success = false, Message = "Bir Hata Oluştu", ErrorCode = ErrorCodes.Exception };
        }
    }

    public async Task<ResponseDto<object>> UpdateTableAsync(UpdateTableDto updateTableDto)
    {
        try
        {
            var validate = await _updateTableValidator.ValidateAsync(updateTableDto);
            if(!validate.IsValid)
                return new ResponseDto<object> { Success = false, Data = null, Message = string.Join(", ", validate.Errors.Select(t => t.ErrorMessage)), ErrorCode = ErrorCodes.ValidationError };
            //var checkTable = await _repository.GetByIdAsync(updateTableDto.Id);
            //if (checkTable.TableNumber == updateTableDto.TableNumber)
            //    return new ResponseDto<object> { Success = false, Message = "Güncellemek İstediğiniz Masa Numarası, Başka Bir Masa İçin Kayıtlı", ErrorCode = ErrorCodes.DuplicateError };
            var table = await _repository.GetByIdAsync(updateTableDto.Id);
            if (table is null)
                return new ResponseDto<object> { Success = false, Message = "Masa Bulunamadı", ErrorCode = ErrorCodes.NotFound };
            var result = _mapper.Map(updateTableDto,table);
            await _repository.UpdateAsync(result);
            return new ResponseDto<object> { Success = true, Message = $"{result.TableNumber} nolu Masa Başarı ile Güncellendi" };
        }
        catch (Exception)
        {
            return new ResponseDto<object> { Success = false, Message = "Bir Hata ile Karşılaşıldı!", ErrorCode = ErrorCodes.Exception };
        }
    }

    public async Task<ResponseDto<object>> UpdateTableStatusByTableNumberAsync(int tableNumber)
    {
        try
        {
            var table = await _tableRepository.GetByTableNumberAsync(tableNumber);
            if (table is null)
                return new ResponseDto<object> { Success = false, Message = "Masa Bulunamadı", ErrorCode = ErrorCodes.NotFound };
            table.IsActive = !table.IsActive;
            await _repository.UpdateAsync(table);
            return new ResponseDto<object> { Success = true, Message = $"{table.TableNumber} nolu Masa Başarı ile Güncellendi" };
        }
        catch (Exception)
        {
            return new ResponseDto<object> { Success = false, Message = "Bir Hata ile Karşılaşıldı!", ErrorCode = ErrorCodes.Exception };
        }
    }
}