using RMS.Business.DTOs.SlideDTOs;
using RMS.Core.Entities;
using System.Linq.Expressions;

namespace RMS.Business.Services.Abstracts
{
    public interface ISlideService
    {
        Task Create(SlideCreateDto CreateDTO);
        Task DeleteSlide(int id);
        Task SoftDeleteSlide(int id);
        Task<SlideUpdateDto> GetSlideForUpdate(int id);
        Task Update(int id, SlideUpdateDto UpdateDTO);
        Task<SlideGetDto> GetSlide(Func<Slide, bool>? func = null);
        Task<List<SlideGetDto>> GetAllSlides(
            Expression<Func<Slide, bool>>? func = null,
            Expression<Func<Slide, object>>? orderBy = null,
            bool isOrderByDesting = false,
            params string[]? includes);
    }
}
