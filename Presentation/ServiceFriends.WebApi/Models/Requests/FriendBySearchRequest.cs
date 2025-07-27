#pragma warning disable CS8618

namespace ServiceFriends.WebApi.Models.Requests
{
    public class FriendBySearchRequest
    {
        public PaginationRequest Options { get; set; }
        public Guid UserId { get; set; }
    }
}
