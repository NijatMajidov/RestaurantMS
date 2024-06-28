
using RMS.Core.Entities;
using RMS.Data.DAL;
using RMS.Data.Repositories.Abstractions;

namespace RMS.Data.Repositories.Implementations
{
    public class RestaurantRepository : Repository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(RMSAppContext context) : base(context)
        {
        }
    }
}
