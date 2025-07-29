using BarRating.Data;
using BarRating.Data.Entities;
using BarRating.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarRating.Repository
{
    public class SavedBarRepository : CommonRepository<Data.Entities.SavedBar>
    {
        public SavedBarRepository(ApplicationDbContext context) : base(context) { }
        public List<Data.Entities.SavedBar> GetUserSavedBars(int userId)
        {
            return context.SavedBars.Include(s => s.Bar).Include(s => s.CreatedBy)
                                    .Where(s => s.CreatedById == userId)
                                    .ToList();
        }
        public Data.Entities.SavedBar GetSavedBarByBarIdandUserId(int barId, int userId)
        {
            return context.SavedBars
                .Include(s => s.Bar)
                .Include(s => s.CreatedBy)
                .Where(s => s.CreatedById == userId)
                .SingleOrDefault(s => s.BarId == barId);

        }
        public SavedBar GetSavedBarById(int savedBarId)
        {
            return context.SavedBars
                .Include(s => s.Bar)
                .Include(s => s.CreatedBy)
                .Single(s => s.Id == savedBarId);
        }
        public List<SavedBar> GetSavedBarsByBarId(int barId)
        {
            return context.SavedBars
                .Include(s => s.Bar)
                .Include(s => s.CreatedBy)
                .Where(s => s.BarId == barId)
                .ToList();
        }
    }
}
