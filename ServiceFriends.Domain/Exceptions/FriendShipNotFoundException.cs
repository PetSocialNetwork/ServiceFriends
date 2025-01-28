namespace ServiceFriends.Domain.Exceptions
{
    public class FriendShipNotFoundException : DomainException
    {
        public FriendShipNotFoundException(string? message) : base(message)
        {
        }

        public FriendShipNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
