using BarRating.Data;
using BarRating.Data.Entities;
using BarRating.Migrations;
using BarRating.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarRating.Repository
{
    public class BarScheduleRepository : CommonRepository<BarSchedule>
    {
        public BarScheduleRepository(ApplicationDbContext context) : base(context) { }
        
        public List<BarSchedule> GetSchedulesByBarId(int barId)
        {
            return context.Schedules
                .Include(s => s.Bar)
                .Where(s => s.BarId == barId)
                .Include(s => s.DayOfWeek)
                .ToList();
        }
    }

}
