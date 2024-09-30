namespace SipCartCore.Exceptions
{
    [Serializable]
    public class CouponNotFoundException : Exception
    {
        public CouponNotFoundException()
        {
        }

        public CouponNotFoundException(string? message) : base(message)
        {
        }

        public CouponNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}