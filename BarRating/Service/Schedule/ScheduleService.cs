using BarRating.Data.Entities;
using BarRating.Models.Schedule;
using BarRating.Repository;

namespace BarRating.Service.Schedule
{
    public class ScheduleService : IScheduleService
    {
        private readonly BarRepository barRepository;

        public ScheduleService(BarRepository barRepository) 
        {
            this.barRepository = barRepository;
        }
        public List<BarScheduleViewModel> GetBarScheduleViewModel(List<BarSchedule> schedules)
        {
            return schedules.Select(s => new  BarScheduleViewModel
            {
                Id = s.Id,
                BarId = s.BarId,
                DayOfWeek = s.DayOfWeek,
                Opening = s.Opening,
                Closing = s.Closing,
                IsClosed = s.IsClosed,

            }).ToList();
            
        }
        public List<BarSchedule> PostBarScheduleViewModel(List<BarScheduleViewModel> schedules)
        {
            return schedules.Select(s => new BarSchedule
            {
                Id = s.Id,
                BarId = s.BarId,
                DayOfWeek = s.DayOfWeek,
                Opening = s.Opening,
                Closing = s.Closing,
                IsClosed = s.IsClosed,

            }).ToList();

        }
        public List<ScheduleOverrideViewModel> GetBarScheduleOverrideViewModel(List<ScheduleOverride> scheduleoverrides)
        {
            return scheduleoverrides.Select(s => new ScheduleOverrideViewModel
            {
                Id = s.Id,
                BarId = s.BarId,
                DayOfWeek = s.DayOfWeek,
                Opening = s.Opening,
                Closing = s.Closing,
                IsClosed = s.IsClosed,
                Reason = s.Reason,

            }).ToList();

        }
        public List<ScheduleOverride> PostBarScheduleOverrideViewModel(List<ScheduleOverrideViewModel> scheduleoverrides)
        {
            return scheduleoverrides.Select(s => new ScheduleOverride
            {
                Id = s.Id,
                BarId = s.BarId,
                DayOfWeek = s.DayOfWeek,
                Opening = s.Opening,
                Closing = s.Closing,
                IsClosed = s.IsClosed,
                Reason = s.Reason,

            }).ToList();

        }
    }
}
