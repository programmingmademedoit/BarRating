namespace BarRating.Models.Schedule
{
    public class ScheduleOverrideViewModel
    {
        public int Id { get; set; }
        public int BarId { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan? Opening { get; set; }
        public TimeSpan? Closing { get; set; }
        public bool IsClosed { get; set; } = false;
        public string? Reason { get; set; } = string.Empty;
    }
}