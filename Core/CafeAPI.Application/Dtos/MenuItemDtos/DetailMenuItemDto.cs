﻿using CafeAPI.Application.Dtos.CategoryDtos;

namespace CafeAPI.Application.Dtos.MenuItemDtos;
public class DetailMenuItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public bool IsAvailable { get; set; }
    public int CategoryId { get; set; }
    public ResultCategoryDto Category { get; set; }
}