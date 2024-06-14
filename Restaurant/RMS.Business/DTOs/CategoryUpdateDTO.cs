using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Business.DTOs
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
                .MaximumLength(50).WithMessage("Name uzunlugu maximum 50 ola biler!")
                .NotEmpty().WithMessage("Name bosh ola bilmez!!");
        }
    }
}
