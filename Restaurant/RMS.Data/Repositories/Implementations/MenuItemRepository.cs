using RMS.Core.Entities;
using RMS.Data.DAL;
using RMS.Data.Repositories.Abstractions;

namespace RMS.Data.Repositories.Implementations
{
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        public MenuItemRepository(RMSAppContext context) : base(context)
        {
        }
    }
}
