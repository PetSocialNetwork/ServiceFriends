#pragma warning disable CS8618 

namespace ServiceFriends.WebApi.Models.Requests
{
    public class FriendRequest
    {
        public Guid UserId { get; init; }
        public Guid FriendId { get; init; }
    }
}
