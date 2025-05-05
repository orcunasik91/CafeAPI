using CafeAPI.Application.Dtos.CategoryDtos;
using FluentValidation;

namespace CafeAPI.Application.Validators.Category;
public class AddCategoryValidator : AbstractValidator<CreateCategoryDto>
{
    public AddCategoryValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Kategori Adı Boş Geçilmemelidir!")
            .Length(3, 30).WithMessage("Kategori Adı Uzunluğu 3 ile 30 Karakter Arasında Olmalıdır!");
    }
}