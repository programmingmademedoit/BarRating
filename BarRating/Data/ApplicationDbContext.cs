using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BarRating.Data.Entities;
using BarRating.Data.Enums;

namespace BarRating.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Bar> Bars { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<HelpfulVote> HelpfulVotes { get; set; }
        public DbSet<SavedBar> SavedBars { get; set; }
        public DbSet<BarSchedule> Schedules { get; set; }
        public DbSet<ScheduleOverride> ScheduleOverrides { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           /* // Configure Bar relationships
            modelBuilder.Entity<Bar>()
                .HasMany(b => b.Reviews)
                .WithOne(r => r.Bar)
                .HasForeignKey(r => r.BarId);


            // Convert Enums to Integers
            modelBuilder.Entity<Bar>()
                .Property(b => b.BarType)
                .HasConversion<int>();

            modelBuilder.Entity<Bar>()
                .Property(b => b.PriceCategory)
                .HasConversion<int>();

            modelBuilder.Entity<Review>()
                .Property(r => r.Price)
                .HasConversion<int>();

            modelBuilder.Entity<Review>()
                .Property(r => r.Rating)
                .HasConversion<int>(); // Assuming Rating is stored as int

            // Complex Types: DailySchedule
            modelBuilder.Entity<Bar>()
                .Property(b => b.WeekSchedule)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, new System.Text.Json.JsonSerializerOptions()),
                    v => System.Text.Json.JsonSerializer.Deserialize<DailySchedule>(v, new System.Text.Json.JsonSerializerOptions()))
                .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<Bar>()
                .Property(b => b.WeekENDSchedule)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, new System.Text.Json.JsonSerializerOptions()),
                    v => System.Text.Json.JsonSerializer.Deserialize<DailySchedule>(v, new System.Text.Json.JsonSerializerOptions()))
                .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<Bar>()
                .Property(b => b.HolidayWeekSchedule)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, new System.Text.Json.JsonSerializerOptions()),
                    v => System.Text.Json.JsonSerializer.Deserialize<DailySchedule>(v, new System.Text.Json.JsonSerializerOptions()))
                .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<Bar>()
                .Property(b => b.HolidayWeekENDSchedule)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, new System.Text.Json.JsonSerializerOptions()),
                    v => System.Text.Json.JsonSerializer.Deserialize<DailySchedule>(v, new System.Text.Json.JsonSerializerOptions()))
                .HasColumnType("nvarchar(max)");*/

        }
    }
}