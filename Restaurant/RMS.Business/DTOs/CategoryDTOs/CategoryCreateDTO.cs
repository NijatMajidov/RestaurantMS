using FluentValidation;

namespace RMS.Business.DTOs.CategoryDTOs
{
    public class CategoryCreateDTO
    {
        public string Name { get; set; } = null!;
    }
    public class CategoryCreateDTOValidator : AbstractValidator<CategoryCreateDTO>
    {
        public CategoryCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(3).WithMessage("Uzunluq minimum 3 olmalidir")
                .MaximumLength(50).WithMessage("Uzunlugu maximum 50 ola biler!")
                .NotEmpty().WithMessage("Ad bos ola bilmez!!");
        }
    }
}
