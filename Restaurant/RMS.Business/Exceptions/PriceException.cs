namespace RMS.Business.Exceptions
{
    public class PriceException:Exception
    {
        public string MyProperty { get; set; }
        public PriceException(string name, string? message) : base(message)
        {
            MyProperty = name;
        }
    }
}
