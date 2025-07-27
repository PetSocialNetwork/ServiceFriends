using ServiceFriends.Domain.Interfaces;

namespace ServiceFriends.Domain.Entities
{
    public class SentFriendRequest : IEntity
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public Guid FriendId { get; init; }
        public DateTime CreatedAt { get; set; }
        public SentFriendRequest(Guid id, Guid userId, Guid friendId)
        {
            Id = id;
            UserId = userId;
            FriendId = friendId;
        }
        protected SentFriendRequest() { }
    }
}
