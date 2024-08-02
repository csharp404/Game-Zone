

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebApplication5.Model;

namespace WebApplication5.Data
{
    public class MyDbContext: IdentityDbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> op):base(op) 
        {
            
        }
       public DbSet<Game> games { set; get; }
       public DbSet<Device> Devices { set; get; }
       public DbSet<Category> Categories { set; get; }
       public DbSet<GameDevice> GamesDevices { set; get; }
       public DbSet<GameCategory> GamesCategories { set; get; }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<GameCategory>().HasKey(x => new {x.CategoryId,x.GameId });
            modelBuilder.Entity<GameDevice>().HasKey(x => new {x.DeviceId,x.GameId });
            modelBuilder.Entity<Device>().HasData(
                new Device {Id=1, Name = "playstation"},
                new Device { Id = 2, Name = "pc"},
                new Device {Id=3, Name = "xbox"},
                new Device {Id=4, Name = "nintendo" }
                );
        }
    }
}
