using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Business.Exceptions.TableEx
{
    public class TableCapacityException : Exception
    {
        public TableCapacityException(string name,string? message) : base(message)
        {
            MyProperty = name;
        }

        public string MyProperty { get; set; }
        
    }
}
