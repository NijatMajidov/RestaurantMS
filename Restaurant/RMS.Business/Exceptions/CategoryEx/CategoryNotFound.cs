using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Business.Exceptions.CategoryEx
{
    public class CategoryNotFound : Exception
    {
        public string MyProperty { get; set; }
        public CategoryNotFound(string name, string? message) : base(message)
        {
            MyProperty = name;
        }
    }
}
