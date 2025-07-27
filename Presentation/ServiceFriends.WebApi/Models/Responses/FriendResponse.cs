#pragma warning disable CS8618  

namespace ServiceFriends.WebApi.Models.Responses
{
    public class FriendResponse
    {
        public Guid Id { get; init; }
        public Guid FriendId { get; init; }
        public DateTime CreatedAt { get; set; }
    }
}
