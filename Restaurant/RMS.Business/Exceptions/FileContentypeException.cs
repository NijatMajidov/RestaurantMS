namespace RMS.Business.Exceptions
{
    public class FileContentypeException : Exception
    {
        public string MyProperty { get; set; }
        public FileContentypeException(string name, string? message) : base(message)
        {
            MyProperty = name;
        }
    }
}
