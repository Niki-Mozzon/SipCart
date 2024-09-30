namespace SipCartCore.Exceptions
{
    [Serializable]
    public class ItemNotFoundException : Exception
    {
        private int orderId;

        public ItemNotFoundException()
        {
        }

        public ItemNotFoundException(int orderId)
        {
            this.orderId = orderId;
        }

        public ItemNotFoundException(string? message) : base(message)
        {
        }

        public ItemNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}