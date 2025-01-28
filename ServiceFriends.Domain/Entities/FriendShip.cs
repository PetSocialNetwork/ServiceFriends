using ServiceFriends.Domain.Enums;
using ServiceFriends.Domain.Interfaces;

namespace ServiceFriends.Domain.Entities
{
    public class FriendShip : IEntity
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public Guid FriendId { get; init; }
        public FriendStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public FriendShip(Guid id, Guid userId, Guid friendId)
        {
            Id = id;
            UserId = userId;
            FriendId = friendId;
        }
        protected FriendShip(){ }
    }
}