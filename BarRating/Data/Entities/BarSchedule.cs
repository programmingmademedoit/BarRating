namespace BarRating.Data.Entities
{
    public class BarSchedule : BaseEntity
    {
        public int BarId { get; set; }
        public Bar Bar { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan Opening { get; set; }
        public TimeSpan Closing { get; set; }
        public bool IsClosed { get; set; } = false;
    }
}