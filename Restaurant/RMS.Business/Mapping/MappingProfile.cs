using AutoMapper;
using RMS.Business.DTOs.CategoryDTOs;
using RMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<CategoryCreateDTO, Category>().ReverseMap();
            CreateMap<Category, CategoryGetDTO>().ReverseMap();
            CreateMap<Category, CategoryUpdateDTO>().ReverseMap();
        }
    }
}
