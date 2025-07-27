using Microsoft.EntityFrameworkCore;
using ServiceFriends.Domain.Entities;
using ServiceFriends.Domain.Interfaces;
using ServiceFriends.Domain.Shared;

namespace ServiceFriends.DataEntityFramework.Repositories
{
    public class FriendShipRepository : EFRepository<FriendShip>, IFriendShipRepository
    {
        public FriendShipRepository(AppDbContext appDbContext) : base(appDbContext) { }
        public async Task<List<FriendShip>> BySearch
            (Guid id, PaginationOptions options, CancellationToken cancellationToken)
        {
            return await Entities
                .Where(c => c.UserId == id || c.FriendId == id)
                .Skip(options.Take * options.Offset)
                .Take(options.Take)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsFriendAsync
           (Guid userId, Guid friendId, CancellationToken cancellationToken)
        {
            return await Entities.AnyAsync(c =>
                (c.UserId == userId && c.FriendId == friendId) ||
                (c.FriendId == userId && c.UserId == friendId),cancellationToken);
        }

        public async Task<FriendShip?> FindFriendsAsync
            (Guid userId, Guid friendId, CancellationToken cancellationToken)
        {
            return await Entities.Where(uf =>
                (uf.UserId == userId && uf.FriendId == friendId) ||
                (uf.UserId == friendId && uf.FriendId == userId))
                .FirstOrDefaultAsync(cancellationToken);
        }   
    }
}
