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
