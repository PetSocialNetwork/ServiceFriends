using ServiceFriends.Domain.Entities;
using ServiceFriends.Domain.Shared;

namespace ServiceFriends.Domain.Interfaces
{
    public interface ISentFriendRequestRepository : IRepositoryEF<SentFriendRequest>
    {
        Task<List<SentFriendRequest>> GetSentRequestAsync(Guid userId, PaginationOptions options, CancellationToken cancellationToken);
        Task<SentFriendRequest?> FindSentRequestAsync(Guid userId, Guid friendId, CancellationToken cancellationToken);
        Task<bool> HasSentRequestAsync(Guid userId, Guid friendId, CancellationToken cancellationToken);
    }
}
