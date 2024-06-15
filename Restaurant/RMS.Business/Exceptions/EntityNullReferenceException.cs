using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Business.Exceptions
{
    public class EntityNullReferenceException : Exception
    {
        public string MyProperty { get; set; }
        public EntityNullReferenceException(string name, string? message) : base(message)
        {
            MyProperty = name;
        }
    }
}
