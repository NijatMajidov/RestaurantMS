using FluentValidation;

namespace RMS.Business.DTOs.CategoryDTOs
{
    public class CategoryUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class CategoryUpdateDTOValidator : AbstractValidator<CategoryUpdateDTO>
    {
        public CategoryUpdateDTOValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(3).WithMessage("Uzunluq minimum 3 olmalidir")
                .MaximumLength(50).WithMessage("Uzunlugu maximum 50 ola biler!")
                .NotEmpty().WithMessage("Ad bos ola bilmez!!");
        }
    }
}
