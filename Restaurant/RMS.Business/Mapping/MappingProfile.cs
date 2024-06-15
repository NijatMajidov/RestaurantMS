using AutoMapper;
using RMS.Business.DTOs.CategoryDTOs;
using RMS.Business.DTOs.TableDTOs;
using RMS.Core.Entities;
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
            CreateMap<Table, TableCreateDto>().ReverseMap();
            CreateMap<Table, TableGetDto>().ReverseMap();
            CreateMap<Table, TableUpdateDto>().ReverseMap();
        }
    }
}
