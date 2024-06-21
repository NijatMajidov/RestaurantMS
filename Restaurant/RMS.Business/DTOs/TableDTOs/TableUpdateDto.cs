using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace RMS.Business.DTOs.TableDTOs
{
    public class TableUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Capacity { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
    public class TableUpdateDtoValidator : AbstractValidator<TableUpdateDto>
    {
        public TableUpdateDtoValidator()
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
