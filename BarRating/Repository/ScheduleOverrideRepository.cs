using BarRating.Data;
using BarRating.Data.Entities;
using BarRating.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarRating.Repository
{
    public class ScheduleOverrideRepository : CommonRepository<ScheduleOverride>
    {
        public ScheduleOverrideRepository(ApplicationDbContext context) : base(context) { }
        public List<ScheduleOverride> GetSchedulesByBarId(int barId)
        {
            return context.ScheduleOverrides
                .Include(s => s.Bar)
                .Where(s => s.BarId == barId)
                .Include(s => s.DayOfWeek)
                .ToList();
        }
    }
}
