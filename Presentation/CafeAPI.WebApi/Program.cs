using CafeAPI.Application.Interfaces;
using CafeAPI.Application.Mappings;
using CafeAPI.Application.Services.Abstracts;
using CafeAPI.Application.Services.Concretes;
using CafeAPI.Persistence.Context;
using CafeAPI.Persistence.Repositories;
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
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repositoy<>));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IMenuItemService, MenuItemService>();

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