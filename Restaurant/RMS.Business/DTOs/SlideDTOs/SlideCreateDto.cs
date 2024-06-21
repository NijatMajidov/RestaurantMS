using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace RMS.Business.DTOs.SlideDTOs
{
    public class SlideCreateDto
    {
        public string Title { get; set; } = null!;
        public string SubTitle { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IFormFile ImageFile { get; set; } = null!;
    }
    public class SlideCreateDtoValidator : AbstractValidator<SlideCreateDto>
    {
        public SlideCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .MaximumLength(50).WithMessage("Uzunlugu maximum 50 ola biler!")
                .NotEmpty().WithMessage("Title bos ola bilmez!!");
           RuleFor(x=>x.SubTitle)
                .MaximumLength(50).WithMessage("Uzunlugu maximum 50 ola biler!")
                .NotEmpty().WithMessage("SubTitle bos ola bilmez!!");
            RuleFor(x => x.Description)
                .MaximumLength(100).WithMessage("Uzunlugu Maximum 100")
                .NotEmpty().WithMessage("Description bos ola bilmez");
        }
    }
}
