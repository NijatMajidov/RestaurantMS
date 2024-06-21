using RMS.Core.Entities;
using RMS.Data.DAL;
using RMS.Data.Repositories.Abstractions;

namespace RMS.Data.Repositories.Implementations
{
    public class TableRepository : Repository<Table>, ITableRepository
    {
        public TableRepository(RMSAppContext context) : base(context)
        {
        }
    }
}
