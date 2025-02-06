using ServiceFriends.Domain.Entities;

namespace ServiceFriends.Domain.Interfaces
{
    public interface IFriendShipRepository : IRepositoryEF<FriendShip>
    {
        Task<List<FriendShip>?> FindFriendsAsync(Guid userId, Guid friendId, CancellationToken cancellationToken);
        Task<List<FriendShip>> BySearch(Guid userId, CancellationToken cancellationToken);
        Task<List<FriendShip>> GetSentRequestAsync(Guid userId, CancellationToken cancellationToken);
        Task<List<FriendShip>> GetReceivedRequestAsync(Guid userId, CancellationToken cancellationToken);
        Task<FriendShip?> FindSentRequestAsync(Guid userId, Guid friendId, CancellationToken cancellationToken);
        Task<FriendShip?> FindReceivedRequestAsync(Guid userId, Guid friendId, CancellationToken cancellationToken);
        Task<bool> IsFriendAsync(Guid userId, Guid friendId, CancellationToken cancellationToken);
        Task<bool> HasSentRequestAsync(Guid userId, Guid friendId, CancellationToken cancellationToken);
    }
}
