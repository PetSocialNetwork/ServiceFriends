using Microsoft.EntityFrameworkCore;
using ServiceFriends.Domain.Entities;
using ServiceFriends.Domain.Interfaces;
using ServiceFriends.Domain.Shared;

namespace ServiceFriends.DataEntityFramework.Repositories
{
    public class SentFriendRequestRepository : EFRepository<SentFriendRequest>, ISentFriendRequestRepository
    {
        public SentFriendRequestRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<SentFriendRequest?> FindSentRequestAsync
            (Guid userId, Guid friendId, CancellationToken cancellationToken)
        {
            return await Entities
                .SingleOrDefaultAsync(c => c.UserId == friendId && c.FriendId == userId, cancellationToken);
        }

        public async Task<List<SentFriendRequest>> GetSentRequestAsync
           (Guid userId, PaginationOptions options, CancellationToken cancellationToken)
        {
            return await Entities
                .Where(c => c.UserId == userId)
                .Skip(options.Take * options.Offset)
                .Take(options.Take)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> HasSentRequestAsync
            (Guid userId, Guid friendId, CancellationToken cancellationToken)
        {
            return await Entities.AnyAsync(c =>
                c.UserId == friendId && c.FriendId == userId, cancellationToken);
        }
    }
}
