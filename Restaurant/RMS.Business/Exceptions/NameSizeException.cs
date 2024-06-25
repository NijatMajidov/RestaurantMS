namespace RMS.Business.Exceptions
{
    public class NameSizeException : Exception
    {
        public string MyProperty { get; set; }
        public NameSizeException(string name, string? message) : base(message)
        {
            MyProperty = name;
        }
    }
}
