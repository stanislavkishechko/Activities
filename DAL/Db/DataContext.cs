using DAL.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Db
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Activity> Activities { get; set; }
    }
}
