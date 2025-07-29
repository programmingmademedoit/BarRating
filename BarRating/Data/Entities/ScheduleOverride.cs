namespace BarRating.Data.Entities
{
    public class ScheduleOverride : BaseEntity
    {
        public int BarId { get; set; }
        public Bar Bar { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan Opening { get; set; }
        public TimeSpan Closing { get; set; }
        public bool IsClosed { get; set; } = false;
        public string Reason { get; set; } = string.Empty;
    }
}