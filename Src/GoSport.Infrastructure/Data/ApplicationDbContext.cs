namespace GoSport.Infrastructure.Data
{
    using GoSport.Infrastructure.Data.DateModels;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Discipline> Disciplines { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Organizer> Organizers { get; set; }

        public DbSet<Sport> Sports { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Venue> Venues { get; set; }
        public DbSet<EventUser> EventUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<EventUser>()
              .HasNoKey();
        }


    }
}
