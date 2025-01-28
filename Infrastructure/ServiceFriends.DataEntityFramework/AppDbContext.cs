using Microsoft.EntityFrameworkCore;
using ServiceFriends.Domain.Entities;

namespace ServiceFriends.DataEntityFramework
{
    public class AppDbContext : DbContext
    {
        DbSet<FriendShip> Friends => Set<FriendShip>();
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
    }
}
