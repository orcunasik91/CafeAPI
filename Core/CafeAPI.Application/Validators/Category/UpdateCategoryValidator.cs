using CafeAPI.Application.Dtos.CategoryDtos;
using FluentValidation;

namespace CafeAPI.Application.Validators.Category;
public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Kategori Id'si '0' Olmamalıdır!");
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Kategori Adı Boş Geçilmemelidir!")
            .Length(3, 30).WithMessage("Kategori Adı Uzunluğu 3 ile 30 Karakter Arasında Olmalıdır!");
    }
}