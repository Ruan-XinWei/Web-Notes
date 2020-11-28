using System.Data.Entity;

namespace GuestBookSystem.Models
{
    public class GBSDBContext : DbContext
    {
        public DbSet<Guestbook> Guestbooks { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
    }

}
