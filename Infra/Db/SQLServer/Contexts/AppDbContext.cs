using Microsoft.EntityFrameworkCore;
using barbequeue.api.Domain.Models;

namespace barbequeue.api.Infra.Db.SQLServer.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Barbeque> Barbeques { get; set; }
        public DbSet<BarbequeParticipant> BarbequeParticipants { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating (ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Barbeque>().ToTable("BARBEQUES");
            builder.Entity<Barbeque>().HasKey(p => p.Id);
            builder.Entity<Barbeque>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Barbeque>().Property(p => p.Description).IsRequired().HasMaxLength(128);
            builder.Entity<Barbeque>().Property(p => p.EventDateTime).IsRequired();
            builder.Entity<Barbeque>()
                .HasMany<BarbequeParticipant>(p => p.Participants)
                .WithOne(p => p.Barbeque)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(p => p.BarbequeId)
                .IsRequired();
            
            builder.Entity<BarbequeParticipant>().ToTable("BARBEQUES_PARTICIPANTS");
            builder.Entity<BarbequeParticipant>().HasKey(p => p.Id);
            builder.Entity<BarbequeParticipant>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<BarbequeParticipant>().Property(p => p.Name).IsRequired().HasMaxLength(128);
            builder.Entity<BarbequeParticipant>().Property(p => p.Contribution).IsRequired();
            builder.Entity<BarbequeParticipant>().Property(p => p.Paid).IsRequired().HasDefaultValue(false);

            builder.Entity<User>().ToTable("USERS");
            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(p => p.Username).IsRequired().HasMaxLength(128);   
            builder.Entity<User>().Property(p => p.Password).IsRequired().HasMaxLength(255);   
            builder.Entity<User>().HasData(
                new User { Id = 10, Username = "andre", Password = "$2b$12$F9IJedOAcH4QB/Jk3h3w9OTmo/UAIKf4dEJve1OcAd9TDZyG6zU4W" }
            );
        }
    }
}