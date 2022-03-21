using Microsoft.EntityFrameworkCore;


namespace Models
{
    public class MainContext : DbContext
    {

        public DbSet<Korisnik> Korisnici {get; set;}
        public DbSet<Soba> Sobe {get; set;}
        public DbSet<Prenociste> Prenocista {get; set;}
        public DbSet<Zakazivanje> Zakazivanja{get; set;}
        public MainContext(DbContextOptions options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;

        }

     }
}