using BarRating.Data.Entities;
using BarRating.Data.Enums;
using BarRating.Models.Review;
using BarRating.Models.Schedule;
using System.ComponentModel.DataAnnotations;

namespace BarRating.Models.Bar
{
    public class CreateBarViewModel
    {
        public int CreatedById { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public string Location { get; set; }
        public BarType BarType { get; set; }
        public string? Website { get; set; }
        public string PhoneNumber { get; set; }
        public string? Instagram { get; set; }

        // Geo Coordinates
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // Schedule
        public List<BarScheduleViewModel> Schedules { get; set; } = new List<BarScheduleViewModel>();
        public List<ScheduleOverrideViewModel>? ScheduleOverrides { get; set; } = new List<ScheduleOverrideViewModel>();

        // Features
        public bool HasLiveMusic { get; set; } = false;
        public bool HasFood { get; set; } = false;
        public bool HasOutdoorSeating { get; set; } = false;
        public bool AcceptsReservations { get; set; } = false;
        public bool HasParking { get; set; } = false;
        public bool IsWheelchairAccessible { get; set; } = false;

        public int AverageSpent { get; set; } = 0;

        // Ownership & Moderation
        public bool IsVerified { get; set; } = false;
        public int? OwnerId { get; set; }

        public List<ReviewViewModel> Reviews { get; set; } = new List<ReviewViewModel>();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
