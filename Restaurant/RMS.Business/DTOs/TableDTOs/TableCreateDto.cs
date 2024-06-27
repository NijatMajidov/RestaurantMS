using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace RMS.Business.DTOs.TableDTOs
{
    public class TableCreateDto
    {
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }
        public IFormFile ImageFile { get; set; } = null!;
        public string? WaiterId { get; set; } = null!;
    }
    public class TableCreateDTOValidator : AbstractValidator<TableCreateDto>
    {
        public TableCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(3).WithMessage("Uzunluq 3 olmalidir")
                .MaximumLength(3).WithMessage("Uzunlugu maximum 3 ola biler!")
                .NotEmpty().WithMessage("Ad bos ola bilmez!!");
            RuleFor(x => x.Capacity)
                .InclusiveBetween(1, 16).WithMessage("Capacity 1-dən 16-ya qədər olan tam ədəd olmalıdır");

        }
    }
}
