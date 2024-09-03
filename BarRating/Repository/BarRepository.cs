using BarRating.Data;
using BarRating.Data.Entities;
using BarRating.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarRating.Repository
{
    public class BarRepository : CommonRepository<Bar>
    {
        public BarRepository(ApplicationDbContext context) : base(context) { }
        public List<Bar> GetAllBars()
        {
            return context.Bars
                .Include(b => b.CreatedBy)
                .Include(b => b.Reviews)
                .ToList();
        }
        public Bar GetBarById(int id)
        {
            return context.Bars
                .Include(b => b.CreatedBy)
                .Include(b => b.Reviews)
                .ThenInclude(r => r.CreatedBy)
                .Where(b => b.Id == id)
                .Single();
        }
        public List<Bar> Search(string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                return context.Bars.ToList();
            }

            return context.Bars
                                .AsEnumerable()                
                                .Where(b => b.Name
                                .StartsWith(searchQuery, StringComparison.OrdinalIgnoreCase))
                                .ToList();
        }
        public List<Data.Entities.Bar> SearchReview(int id, string searchQuery)
        {
            // Step 1: Fetch the restaurant(s) by ID and include related entities
            var query = context.Bars
                .Include(r => r.CreatedBy)
                .Include(r => r.Reviews)
                .ThenInclude(r => r.CreatedBy)
                .Where(b => b.Id == id);

            // Step 2: If a search query is provided, filter the reviews in-memory
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.AsEnumerable()
                    .Where(b => b.Reviews.Any(
                        r => r.Text.StartsWith(searchQuery, StringComparison.OrdinalIgnoreCase)))
                    .AsQueryable();
            }

            // Step 3: Return the final list of restaurants
            return query.ToList();
        }

    }
}
