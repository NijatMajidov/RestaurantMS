using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace RMS.Business.DTOs.SlideDTOs
{
    public class SlideUpdateDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Description { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
    public class SlideUpdateDtoValidator : AbstractValidator<SlideUpdateDto>
    {
        public SlideUpdateDtoValidator()
        {
            RuleFor(x => x.Title)
                .MaximumLength(50).WithMessage("Uzunlugu maximum 50 ola biler!")
                .NotEmpty().WithMessage("Title bos ola bilmez!!");
            RuleFor(x => x.SubTitle)
                 .MaximumLength(50).WithMessage("Uzunlugu maximum 50 ola biler!")
                 .NotEmpty().WithMessage("SubTitle bos ola bilmez!!");
            RuleFor(x => x.Description)
                .MaximumLength(100).WithMessage("Uzunlugu Maximum 100")
                .NotEmpty().WithMessage("Description bos ola bilmez");
        }
    }
}
