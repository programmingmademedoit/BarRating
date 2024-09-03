using BarRating.Data.Entities;
using BarRating.Repository;
namespace BarRating.Service.Bar
{
    public class BarService : IBarService   
    {
        private readonly BarRepository repository;
        public BarService(BarRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Data.Entities.Bar> Create(Data.Entities.Bar bar, Data.Entities.User createdBy)
        {
            bar.CreatedBy = createdBy;
            return await repository.Create(bar);

        }
        public async Task<Data.Entities.Bar> Edit(Data.Entities.Bar bar)
        {
            return await repository.Edit(bar);
        }
        public async Task<Data.Entities.Bar> Delete(Data.Entities.Bar bar)
        {
            return await repository.Delete(bar);
        }
    }
}
