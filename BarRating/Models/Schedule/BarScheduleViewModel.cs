namespace BarRating.Models.Schedule
{
    public class BarScheduleViewModel
    {
        public int Id { get; set; }
        public int BarId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan Opening { get; set; }
        public TimeSpan Closing { get; set; }
        public bool IsClosed { get; set; } = false;
    }
}