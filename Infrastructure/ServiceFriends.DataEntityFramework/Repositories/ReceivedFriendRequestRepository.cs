using Microsoft.EntityFrameworkCore;
using ServiceFriends.Domain.Entities;
using ServiceFriends.Domain.Interfaces;
using ServiceFriends.Domain.Shared;

namespace ServiceFriends.DataEntityFramework.Repositories
{
    public class ReceivedFriendRequestRepository : EFRepository<ReceivedFriendRequest>, IReceivedFriendRequestRepository
    {
        public ReceivedFriendRequestRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<ReceivedFriendRequest?> FindReceivedRequestAsync
            (Guid userId, Guid friendId, CancellationToken cancellationToken)
        {
            return await Entities
                .SingleOrDefaultAsync(c => c.UserId == userId && c.FriendId == friendId, cancellationToken);
        }

        public async Task<List<ReceivedFriendRequest>> GetReceivedRequestAsync
            (Guid userId, PaginationOptions options, CancellationToken cancellationToken)
        {
            return await Entities
                .Where(c => c.UserId == userId)
                .Skip(options.Take * options.Offset)
                .Take(options.Take)
                .ToListAsync(cancellationToken);
        }
    }
}
