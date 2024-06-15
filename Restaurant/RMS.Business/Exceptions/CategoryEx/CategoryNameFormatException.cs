using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Business.Exceptions.CategoryEx
{
    public class CategoryNameFormatException: Exception
    {
        public string MyProperty { get; set; }
        public CategoryNameFormatException(string name, string? message) : base(message)
        {
            MyProperty = name;
        }
    }
}
