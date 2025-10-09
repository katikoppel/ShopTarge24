using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;


namespace ShopTARge24.Data
{
    public class ShopTARge24Context : DbContext
    {

        public ShopTARge24Context(DbContextOptions<ShopTARge24Context> options)
            : base(options) { }
        public DbSet<Kindergarten> Kindergartens { get; set; }
        public DbSet<KindergartenFileToDatabase> KindergartenFileToDatabases { get; set; }
    }
}
