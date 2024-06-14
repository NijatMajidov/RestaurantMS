using RMS.Core.Models;
using RMS.Data.DAL;
using RMS.Data.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Data.Repositories.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(RMSAppContext context) : base(context)
        {
        }
    }
}
