using RMS.Business.DTOs.EmployeeDTOs;
using RMS.Business.DTOs.SlideDTOs;

namespace Restaurant.ViewModels
{
    public class HomeViewModel
    {
        
            public List<SlideGetDto> Slides { get; set; }
            public List<EmployeeGetDto> Employees { get; set; }
        
    }
}
