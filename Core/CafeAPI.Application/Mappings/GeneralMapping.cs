using AutoMapper;
using CafeAPI.Application.Dtos.CategoryDtos;
using CafeAPI.Application.Dtos.MenuItemDtos;
using CafeAPI.Application.Dtos.TableDtos;
using CafeAPI.Domain.Entities;

namespace CafeAPI.Application.Mappings;
public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        #region CategoryMapping
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Category, ResultCategoryDto>().ReverseMap();
        CreateMap<Category, UpdateCategoryDto>().ReverseMap();
        CreateMap<Category, DetailCategoryDto>().ReverseMap();
        #endregion
        #region MenuItemMapping
        CreateMap<MenuItem, CreateMenuItemDto>().ReverseMap();
        CreateMap<MenuItem, ResultMenuItemDto>().ReverseMap();
        CreateMap<MenuItem, UpdateMenuItemDto>().ReverseMap();
        CreateMap<MenuItem, DetailMenuItemDto>().ReverseMap();
        CreateMap<MenuItem, MenuItemResponseDto>().ReverseMap();
        #endregion
        #region TableMapping
        CreateMap<Table, CreateTableDto>().ReverseMap();
        CreateMap<Table, ResultTableDto>().ReverseMap();
        CreateMap<Table, UpdateTableDto>().ReverseMap();
        CreateMap<Table, DetailTableDto>().ReverseMap();
        #endregion
    }
}