using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using RMS.Business.DTOs.SlideDTOs;
using RMS.Business.Exceptions;
using RMS.Business.Services.Abstracts;
using RMS.Core.Entities;
using RMS.Data.Repositories.Abstractions;
using System.Linq.Expressions;

namespace RMS.Business.Services.Concretes
{
    public class SlideService : ISlideService
    {
        readonly ISlideRepository _slideRepository;
        readonly IMapper _mapper;
        readonly IWebHostEnvironment _webHostEnvironment;
        public SlideService(ISlideRepository slideRepository,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment)
        {
            _slideRepository = slideRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task Create(SlideCreateDto CreateDTO)
        {
            if (CreateDTO == null)
                throw new EntityNullReferenceException("", "Slide null reference");
            if (CreateDTO.ImageFile == null)
                throw new Exceptions.FileNotFoundException("ImageFile", "Image file not found");

            if (!CreateDTO.ImageFile.ContentType.Contains("image/"))
                throw new FileContentypeException("ImageFile", "Image file contenttype exception");
            if (CreateDTO.ImageFile.Length > 2097152)
                throw new FileSizeException("ImageFile", "Image file Size error!!");
            
            if (CreateDTO.Title.Length >50)
                throw new NameSizeException("Title", "Slide Title maximum uzunlugu 50 char olmalidir");
            if(CreateDTO.SubTitle.Length >50)
                throw new NameSizeException("SubTitle", "Slide SubTitle maximum uzunlugu 50 char olmalidir");
            if(CreateDTO.Description.Length >200)
                throw new NameSizeException("Description", "Slide Description maximum uzunlugu 200 char olmalidir");

            string filename = Guid.NewGuid().ToString() + Path.GetExtension(CreateDTO.ImageFile.FileName);
            string path = _webHostEnvironment.WebRootPath + @"\uploads\Sliders\" + filename;
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                CreateDTO.ImageFile.CopyTo(stream);
            }
            var entity = _mapper.Map<Slide>(CreateDTO);
            entity.ImageUrl = filename;
            _slideRepository.Add(entity);
            await _slideRepository.CommitAsync();
        }

        public async Task DeleteSlide(int id)
        {
            var slide = await _slideRepository.GetAsync(x => x.Id == id);
            if (slide == null)
                throw new EntityNotFoundException("", "Slide not found");
            string oldpath = _webHostEnvironment.WebRootPath + @"\uploads\Sliders\" + slide.ImageUrl;
            if (!File.Exists(oldpath)) throw new Exceptions.FileNotFoundException("ImageFile", "Image File not found!");
            {
                File.Delete(oldpath);
            }
            _slideRepository.Delete(slide);
            await _slideRepository.CommitAsync();
        }

        public async Task<List<SlideGetDto>> GetAllSlides(
            Expression<Func<Slide, bool>>? func = null,
            Expression<Func<Slide, object>>? orderBy = null,
            bool isOrderByDesting = false, params string[]? includes)
        {
            var queryable = await _slideRepository.GetAllAsync(func, orderBy, isOrderByDesting, includes);
            return _mapper.Map<List<SlideGetDto>>(queryable);
        }

        public async Task<SlideGetDto> GetSlide(Func<Slide, bool>? func = null)
        {
            var entity = _slideRepository.Get(func);
            if (entity == null) throw new EntityNotFoundException("", "slide not Found");

            return _mapper.Map<SlideGetDto>(entity);
        }

        public async Task<SlideUpdateDto> GetSlideForUpdate(int id)
        {
            var entity = await _slideRepository.GetAsync(x=>x.Id==id);
            if (entity == null) throw new EntityNotFoundException("", "slide not Found");

            return _mapper.Map<SlideUpdateDto>(entity);
        }

        public async Task SoftDeleteSlide(int id)
        {
            var entity = await _slideRepository.GetAsync(x => x.Id == id);
            if (entity == null) throw new EntityNotFoundException("", "Slide not found");
            _slideRepository.SoftDelete(entity);
            await _slideRepository.CommitAsync();
        }

        public async Task Update(int id, SlideUpdateDto UpdateDTO)
        {
            var oldSlide = await _slideRepository.GetAsync(x=>x.Id== id);

            if (oldSlide == null)
                throw new EntityNotFoundException("", "Slide data not found!");
            if (UpdateDTO == null)
                throw new EntityNullReferenceException("", "Slide Update data null reference!");
            if (UpdateDTO.Title.Length > 50)
                throw new NameSizeException("Title", "Slide Title uzunlugu 50 char olmalidir");
            if (UpdateDTO.SubTitle.Length > 50)
                throw new NameSizeException("SubTitle", "Slide SubTitle uzunlugu 50 char olmalidir");
            if (UpdateDTO.Description.Length > 200)
                throw new NameSizeException("Description", "Slide Description maximum uzunlugu 200 char olmalidir");

            if(UpdateDTO.ImageFile != null)
            {
                if (!UpdateDTO.ImageFile.ContentType.Contains("image/"))
                    throw new FileContentypeException("ImageFile", "Image file contenttype exception");
                if (UpdateDTO.ImageFile.Length > 2097152)
                    throw new FileSizeException("ImageFile", "Image file Size error!!");
                string oldpath = _webHostEnvironment.WebRootPath + @"\uploads\Sliders\" + oldSlide.ImageUrl;
                if (!File.Exists(oldpath)) throw new Exceptions.FileNotFoundException("ImageFile", "Image File not found!");
                {
                    File.Delete(oldpath);
                }
                
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(UpdateDTO.ImageFile.FileName);
                string path = _webHostEnvironment.WebRootPath + @"\uploads\Sliders\" + filename;
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    UpdateDTO.ImageFile.CopyTo(stream);
                }

                oldSlide.ImageUrl = filename;
            }
            oldSlide.Title = UpdateDTO.Title;
            oldSlide.SubTitle = UpdateDTO.SubTitle;
            oldSlide.Description = UpdateDTO.Description;
            await _slideRepository.CommitAsync();

        }
    }
}
