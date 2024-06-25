﻿namespace RMS.Business.Exceptions
{
    public class DuplicateException:Exception
    {
        public string MyProperty { get; set; }
        public DuplicateException(string name, string? message) : base(message)
        {
            MyProperty = name;
        }
    }
}
