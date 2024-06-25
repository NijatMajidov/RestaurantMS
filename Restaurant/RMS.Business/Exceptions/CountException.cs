namespace RMS.Business.Exceptions
{
    public class CountException : Exception
    {
        public string MyProperty { get; set; }
        public CountException(string name,string? message) : base(message)
        {
            MyProperty = name;
        }
    }
}
