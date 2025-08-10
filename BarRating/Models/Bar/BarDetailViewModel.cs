using BarRating.Data.Entities;
using BarRating.Data.Enums;
using BarRating.Models.Review;
using BarRating.Models.SavedBar;
using BarRating.Models.Schedule;

namespace BarRating.Models.Bar
{
    public class BarDetailViewModel
    {
        public int BarId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }

        // Type(s) of Bar
        public BarType BarType { get; set; }

        // Contact & Media
        public string? Website { get; set; }
        public string PhoneNumber { get; set; }
        public string? Instagram { get; set; }

        // Geo Coordinates
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // Schedule
        public List<BarScheduleViewModel> Schedules { get; set; }
        public List<ScheduleOverrideViewModel> ScheduleOverrides { get; set; }

        // Features
        public bool HasLiveMusic { get; set; } = false;
        public bool HasFood { get; set; } = false;
        public bool HasOutdoorSeating { get; set; } = false;
        public bool AcceptsReservations { get; set; } = false;
        public bool HasParking { get; set; } = false;
        public bool IsWheelchairAccessible { get; set; } = false;

        // Pricing
        public PriceCategory PriceCategory { get; set; } = PriceCategory.None;

        // Ownership & Moderation
        public bool IsVerified { get; set; } = false;
        public int? OwnerId { get; set; }

        // Ratings
        public double AverageRating { get; set; }
        public int ReviewCount { get; set; } 
        public List<ReviewViewModel> Reviews { get; set; } = new List<ReviewViewModel>();
        public bool IsSaved { get; set; } = false;
    }
}