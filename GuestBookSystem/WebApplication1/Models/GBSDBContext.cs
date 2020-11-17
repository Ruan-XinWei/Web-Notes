using System.Data.Entity;

namespace WebApplication1.Models
{
    public class GBSDBContext : DbContext
    {
        public DbSet<Guestbook> Guestbooks { get; set; }
    }

}
