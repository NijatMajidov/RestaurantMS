namespace RMS.Business.Exceptions
{
    public class FileSizeException : Exception
    {
        public string MyProperty { get; set; }
        public FileSizeException(string name, string? message) : base(message)
        {
            MyProperty = name;
        }
    }
}
