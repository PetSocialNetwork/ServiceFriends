using ServiceFriends.Domain.Entities;
using ServiceFriends.Domain.Shared;

namespace ServiceFriends.Domain.Interfaces
{
    public interface IFriendShipRepository : IRepositoryEF<FriendShip>
    {
        Task<FriendShip?> FindFriendsAsync(Guid userId, Guid friendId, CancellationToken cancellationToken);
        Task<List<FriendShip>> BySearch(Guid id, PaginationOptions options, CancellationToken cancellationToken);
        Task<bool> IsFriendAsync(Guid userId, Guid friendId, CancellationToken cancellationToken);
    }
}
