using RMS.Core.Entities;
using RMS.Data.DAL;
using RMS.Data.Repositories.Abstractions;

namespace RMS.Data.Repositories.Implementations
{
    public class SlideRepository : Repository<Slide>, ISlideRepository
    {
        public SlideRepository(RMSAppContext context) : base(context)
        {
        }
    }
}
