using Microsoft.EntityFrameworkCore;
using ServiceFriends.Domain.Entities;
using ServiceFriends.Domain.Enums;
using ServiceFriends.Domain.Interfaces;
using System.Runtime.CompilerServices;

namespace ServiceFriends.DataEntityFramework.Repositories
{
    public class FriendShipRepository : EFRepository<FriendShip>, IFriendShipRepository
    {
        public FriendShipRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<List<FriendShip>?> FindFriendsAsync(Guid userId, Guid friendId, CancellationToken cancellationToken)
        {
            var friendships = await Entities
              .Where(uf => (uf.UserId == userId && uf.FriendId == friendId) || (uf.UserId == friendId && uf.FriendId == userId))
              .ToListAsync(cancellationToken);

            return friendships;
        }
        public async IAsyncEnumerable<FriendShip> BySearch(Guid userId, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var query = Entities.Where(c => c.UserId == userId && c.Status == FriendStatus.Accepted).AsQueryable();
            await foreach (var friend in query.AsAsyncEnumerable().WithCancellation(cancellationToken))
                yield return friend;
        }
        public async Task<List<FriendShip>> GetSentRequestAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await Entities.Where(c => c.UserId == userId && c.Status == FriendStatus.Sent).ToListAsync(cancellationToken);
        }

        public async Task<List<FriendShip>> GetReceivedRequestAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await Entities.Where(c => c.UserId == userId && c.Status == FriendStatus.Received).ToListAsync(cancellationToken);
        }

        public async Task<FriendShip?> FindReceivedSentAsync(Guid userId, Guid friendId, CancellationToken cancellationToken)
        {
            return await Entities
                .SingleOrDefaultAsync(c => c.UserId == userId && c.FriendId == friendId && c.Status == FriendStatus.Sent, cancellationToken);
        }

        public async Task<FriendShip?> FindReceivedRequestAsync(Guid userId, Guid friendId, CancellationToken cancellationToken)
        {
            return await Entities
                .SingleOrDefaultAsync(c => c.UserId == friendId && c.FriendId == userId && c.Status == FriendStatus.Received, cancellationToken);
        }

        public async Task<bool> IsFriendAsync(Guid userId, Guid friendId, CancellationToken cancellationToken)
        {
      
        }
    }
}
