using Microsoft.EntityFrameworkCore;
using TicketBookingProject.Data.Models;
using TicketBookingProject.Models;

namespace TicketBookingProject.Data.ApplicationDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options) { 
            
        }
        public DbSet<TrainDetails > TrainDetails { get; set; }
        public DbSet<TrainWiseSeatAvailability> TrainWiseSeatAvailabilities { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<PassengerDetails> PassengerDetails { get; set;}
        public DbSet<UsersCredential> UsersCredential { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure RelationShips
            modelBuilder.Entity<PassengerDetails>()
                .HasOne(p => p.Train)
                .WithMany()
                .HasForeignKey(p => p.TrainNumber);

            modelBuilder.Entity<TrainDetails>()
                .HasKey(td => td.TrainNumber);

            modelBuilder.Entity<TrainWiseSeatAvailability>()
                .HasOne(sa => sa.TrainDetails)
                .WithOne(td => td.SeatAvailability)
                .HasForeignKey<TrainWiseSeatAvailability>(sa => sa.TrainNumber);
            
           
        
    }
    }
}
