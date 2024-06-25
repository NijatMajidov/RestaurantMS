namespace RMS.Business.Exceptions
{
    public class PhoneFormatException : Exception
    {
        public string MyProperty { get; set; }
        public PhoneFormatException(string name, string? message) : base(message)
        {
            MyProperty = name;
        }
    }
}
