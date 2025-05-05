using CafeAPI.Application.Dtos.MenuItemDtos;
using FluentValidation;

namespace CafeAPI.Application.Validators.MenuItem;
public class UpdateMenuItemValidator : AbstractValidator<UpdateMenuItemDto>
{
    public UpdateMenuItemValidator()
    {
        RuleFor(mi => mi.Id)
            .NotEmpty().WithMessage("Menü Id'si Boş Geçilmemelidir!");
        RuleFor(mi => mi.Name)
            .NotEmpty().WithMessage("Menü Adı Boş Geçilmemelidir!")
            .Length(3, 30).WithMessage("Menü Adı 2 ile 40 Karakter Arasında Olmalıdır!");
        RuleFor(mi => mi.Price)
            .NotEmpty().WithMessage("Fiyat Alanı Boş Geçilmemelidir!")
            .GreaterThan(0).WithMessage("Fiyat Bilgisi 0'dan Büyük Olmalıdır!");
        RuleFor(mi => mi.Description)
            .NotEmpty().WithMessage("Menü Açıklaması Boş Geçilmemelidir!")
            .Length(5, 100).WithMessage("Menü Açıklaması 5 ile 100 Karakter Arasında Olmalıdır!");
        RuleFor(mi => mi.ImageUrl)
            .NotEmpty().WithMessage("ResimUrl Boş Geçilmemelidir!");
        RuleFor(mi => mi.CategoryId)
            .NotEmpty().WithMessage("Kategori Id Boş Geçilmemelidir!");
    }
}