using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Business.Exceptions
{
    public class NameFormatException:Exception
    {
        public string MyProperty { get; set; }
        public NameFormatException(string name, string? message) : base(message)
        {
            MyProperty = name;
        }
    }
}
