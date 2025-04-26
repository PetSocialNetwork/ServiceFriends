using Microsoft.EntityFrameworkCore;
using ServiceFriends.Domain.Entities;
using ServiceFriends.Domain.Enums;
using ServiceFriends.Domain.Interfaces;

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

        public async Task<List<FriendShip>> BySearch(Guid userId, int take, int offset, CancellationToken cancellationToken)
        {
            return await Entities
                .Where(c => c.UserId == userId && c.Status == FriendStatus.Accepted)
                .Skip(take * offset)
                .Take(offset)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<FriendShip>> GetSentRequestAsync(Guid userId, int take, int offset, CancellationToken cancellationToken)
        {
            return await Entities
                .Where(c => c.UserId == userId && c.Status == FriendStatus.Sent)
                .Skip(take * offset)
                .Take(offset)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<FriendShip>> GetReceivedRequestAsync(Guid userId, int take, int offset, CancellationToken cancellationToken)
        {
            return await Entities
                .Where(c => c.UserId == userId && c.Status == FriendStatus.Received)
                .Skip(take * offset)
                .Take(offset)
                .ToListAsync(cancellationToken);
        }

        public async Task<FriendShip?> FindSentRequestAsync(Guid userId, Guid friendId, CancellationToken cancellationToken)
        {
            return await Entities
                .SingleOrDefaultAsync(c => c.UserId == friendId && c.FriendId == userId && c.Status == FriendStatus.Sent, cancellationToken);
        }

        public async Task<FriendShip?> FindReceivedRequestAsync(Guid userId, Guid friendId, CancellationToken cancellationToken)
        {
            return await Entities
                .SingleOrDefaultAsync(c => c.UserId == userId && c.FriendId == friendId && c.Status == FriendStatus.Received, cancellationToken);
        }

        public async Task<bool> IsFriendAsync(Guid userId, Guid friendId, CancellationToken cancellationToken)
        {
            return await Entities.AnyAsync(c =>
            c.UserId == userId && c.FriendId == friendId &&
            c.Status == FriendStatus.Accepted, cancellationToken);
        }

        public async Task<bool> HasSentRequestAsync(Guid userId, Guid friendId, CancellationToken cancellationToken)
        {
            return await Entities.AnyAsync(c => 
            c.UserId == friendId && c.FriendId == userId && 
            c.Status == FriendStatus.Sent, cancellationToken);
        }
    }
}
