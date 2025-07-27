using Microsoft.EntityFrameworkCore;
using ServiceFriends.Domain.Entities;

namespace ServiceFriends.DataEntityFramework
{
    public class AppDbContext : DbContext
    {
        DbSet<FriendShip> Friends => Set<FriendShip>();
        DbSet<SentFriendRequest> SentFriendRequests => Set<SentFriendRequest>();
        DbSet<ReceivedFriendRequest> ReceivedFriendRequests => Set<ReceivedFriendRequest>();
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
    }
}
