
using BarRating.Data.Enums;

namespace BarRating.Data.Entities
{
    public class Bar : MetadataBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Location { get; set; }

        public BarType BarType { get; set; }

        public string? Website { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Instagram { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public List<BarSchedule> Schedules { get; set; } = new List<BarSchedule>();
        public List<ScheduleOverride>? ScheduleOverrides { get; set; } = new List<ScheduleOverride>();

        public bool HasLiveMusic { get; set; } = false;
        public bool HasFood { get; set; } = false;
        public bool HasOutdoorSeating { get; set; } = false;
        public bool AcceptsReservations { get; set; } = false;
        public bool HasParking { get; set; } = false;
        public bool IsWheelchairAccessible { get; set; } = false;
        public bool IsVerified { get; set; } = false;

        public User? Owner { get; set; }
        public int? OwnerId { get; set; }

        public PriceCategory PriceCategory { get; set; } = PriceCategory.None;
        public double AverageRating { get; set; }

        public List<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<SavedBar>? SavedBars { get; set; } = new List<SavedBar>();
    }
}