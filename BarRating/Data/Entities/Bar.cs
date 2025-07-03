namespace BarRating.Data.Entities
{
    public class Bar : MetadataBaseEntity
    {

        // Basic Info
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Location { get; set; }

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
        public string OpenDaysCsv { get; set; } = string.Empty;
        public DailySchedule WeekSchedule { get; set; }
        public DailySchedule WeekENDSchedule { get; set; }
        public DailySchedule? HolidayWeekSchedule { get; set; }
        public DailySchedule? HolidayWeekENDSchedule { get; set; }

        // Features
        public bool HasLiveMusic { get; set; } = false;
        public bool HasFood { get; set; } = false;
        public bool HasOutdoorSeating { get; set; } = false;
        public bool AcceptsReservations { get; set; } = false;
        public bool HasParking { get; set; } = false;
        public bool IsWheelchairAccessible { get; set; } = false;

        // Pricing
        public PriceCategory PriceCategory { get; set; }
        public int AverageSpent { get; set; }

        // Ownership & Moderation
        public bool IsVerified { get; set; } = false;
        public User? Owner { get; set; }
        public int? OwnerId { get; set; }

        // Ratings
        public List<Review> Reviews { get; set; } = new List<Review>();

        // Helpers
        public List<DayOfWeek> GetOpenDays() =>
            string.IsNullOrEmpty(OpenDaysCsv)
                ? new List<DayOfWeek>()
                : OpenDaysCsv.Split(',')
                    .Select(d => Enum.Parse<DayOfWeek>(d))
                    .ToList();

        public void SetOpenDays(List<DayOfWeek> days) =>
            OpenDaysCsv = string.Join(',', days);
    }
}