namespace SipCartCore.Exceptions
{
    [Serializable]
    public class EmptyCartException : Exception
    {
        public EmptyCartException()
        {
        }

        public EmptyCartException(string? message) : base(message)
        {
        }

        public EmptyCartException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}