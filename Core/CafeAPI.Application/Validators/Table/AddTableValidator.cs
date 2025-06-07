using CafeAPI.Application.Dtos.TableDtos;
using FluentValidation;

namespace CafeAPI.Application.Validators.Table;
public class AddTableValidator : AbstractValidator<CreateTableDto>
{
    public AddTableValidator()
    {
        RuleFor(t => t.TableNumber)
            .NotEmpty().WithMessage("Masa Numarası Boş Olmamalıdır")
            .GreaterThan(0).WithMessage("Masa Numarası 0'dan Büyük Olmalıdır");
        RuleFor(t => t.Capacity)
            .NotEmpty().WithMessage("Masa Kapasitesi Boş Olmamalıdır")
            .GreaterThan(0).WithMessage("Masa Kapasitesi 0'dan Büyük Olmalıdır");
    }
}