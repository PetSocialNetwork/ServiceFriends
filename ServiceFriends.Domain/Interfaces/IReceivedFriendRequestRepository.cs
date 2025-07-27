using ServiceFriends.Domain.Entities;
using ServiceFriends.Domain.Shared;

namespace ServiceFriends.Domain.Interfaces
{
    public interface IReceivedFriendRequestRepository : IRepositoryEF<ReceivedFriendRequest>
    {
        Task<List<ReceivedFriendRequest>> GetReceivedRequestAsync(Guid userId, PaginationOptions options, CancellationToken cancellationToken);
        Task<ReceivedFriendRequest?> FindReceivedRequestAsync(Guid userId, Guid friendId, CancellationToken cancellationToken);
    }
}
