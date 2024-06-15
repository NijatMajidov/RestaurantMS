﻿using RMS.Core.Entities;
using RMS.Data.DAL;
using RMS.Data.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Data.Repositories.Implementations
{
    public class TableRepository : Repository<Table>, ITableRepository
    {
        public TableRepository(RMSAppContext context) : base(context)
        {
        }
    }
}
