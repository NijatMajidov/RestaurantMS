namespace RMS.Business.Exceptions
{
    public class TimeConflictException :Exception
    {
        public string MyProperty { get; set; }

        public TimeConflictException(string property, string message) : base(message)
        {
            MyProperty = property;
        }
    }
}
