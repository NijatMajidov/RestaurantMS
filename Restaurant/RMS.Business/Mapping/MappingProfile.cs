using AutoMapper;
using RMS.Business.DTOs.CategoryDTOs;
using RMS.Business.DTOs.ReservationDTOs;
using RMS.Business.DTOs.SlideDTOs;
using RMS.Business.DTOs.TableDTOs;
using RMS.Core.Entities;

namespace RMS.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Category, CategoryGetDTO>().ReverseMap();
            CreateMap<CategoryCreateDTO, Category>().ReverseMap();
            CreateMap<Category, CategoryUpdateDTO>().ReverseMap();

            CreateMap<Table, TableGetDto>().ReverseMap();
            CreateMap<Table, TableCreateDto>().ReverseMap();
            CreateMap<Table, TableUpdateDto>().ReverseMap();


            CreateMap<Slide, SlideGetDto>().ReverseMap();
            CreateMap<Slide, SlideCreateDto>().ReverseMap();
            CreateMap<Slide, SlideUpdateDto>().ReverseMap();

            CreateMap<Reservation, ReservGetDto>().ReverseMap();
            CreateMap<Reservation, ReservCreateDto>().ReverseMap();
            CreateMap<Reservation, ReservUpdateDto>().ReverseMap();
        }
    }
}
