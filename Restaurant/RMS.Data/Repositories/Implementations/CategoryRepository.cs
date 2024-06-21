using RMS.Core.Entities;
using RMS.Data.DAL;
using RMS.Data.Repositories.Abstractions;

namespace RMS.Data.Repositories.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(RMSAppContext context) : base(context)
        {
        }
    }
}
