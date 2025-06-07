using CafeAPI.Application.Dtos.CategoryDtos;
using CafeAPI.Application.Dtos.MenuItemDtos;
using CafeAPI.Application.Dtos.TableDtos;
using CafeAPI.Application.Interfaces;
using CafeAPI.Application.Mappings;
using CafeAPI.Application.Services.Abstracts;
using CafeAPI.Application.Services.Concretes;
using CafeAPI.Persistence.Context;
using CafeAPI.Persistence.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer
    (builder.Configuration.GetConnectionString("SqlConnection"),
    sqlOptions => sqlOptions.EnableRetryOnFailure()));

builder.Services.AddAutoMapper(typeof(GeneralMapping));
builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateCategoryDto>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateMenuItemDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateMenuItemDto>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateTableDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateTableDto>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repositoy<>));
builder.Services.AddScoped(typeof(ITableRepository), typeof(TableRepository));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IMenuItemService, MenuItemService>();
builder.Services.AddScoped<ITableService, TableService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(opt =>
    {
        opt.Title = "Cafe API v1";
        opt.Theme = ScalarTheme.Purple;
        opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http1);
    });
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();