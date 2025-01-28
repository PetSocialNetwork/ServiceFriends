using ServiceFriends.Domain.Entities;

namespace ServiceFriends.Domain.Interfaces
{
    public interface IFriendShipRepository : IRepositoryEF<FriendShip>
    {
        Task<List<FriendShip>?> FindFriendsAsync(Guid userId, Guid friendId, CancellationToken cancellationToken);
        IAsyncEnumerable<FriendShip> BySearch(Guid userId, CancellationToken cancellationToken);
        Task<List<FriendShip>> GetSentRequestAsync(Guid userId, CancellationToken cancellationToken);
        Task<List<FriendShip>> GetReceivedRequestAsync(Guid userId, CancellationToken cancellationToken);
        Task<FriendShip?> FindReceivedSentAsync(Guid userId, Guid friendId, CancellationToken cancellationToken);
        Task<FriendShip?> FindReceivedRequestAsync(Guid userId, Guid friendId, CancellationToken cancellationToken);
    }
}
