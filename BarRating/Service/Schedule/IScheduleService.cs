using BarRating.Data.Entities;
using BarRating.Models.Schedule;

namespace BarRating.Service.Schedule
{
    public interface IScheduleService
    {
        public List<BarScheduleViewModel> GetBarScheduleViewModel(List<BarSchedule> schedules);
        public List<BarSchedule> PostBarScheduleViewModel(List<BarScheduleViewModel> schedules);
        public List<ScheduleOverrideViewModel> GetBarScheduleOverrideViewModel(List<ScheduleOverride> scheduleoverrides);
        public List<ScheduleOverride> PostBarScheduleOverrideViewModel(List<ScheduleOverrideViewModel> scheduleoverrides);
    }
}