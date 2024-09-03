using BarRating.Data.Entities;

namespace BarRating.Service.Bar
{
    public interface IBarService
    {
        public Task<Data.Entities.Bar> Create(Data.Entities.Bar bar, Data.Entities.User createdBy);
        public Task<Data.Entities.Bar> Edit(Data.Entities.Bar bar);
        public Task<Data.Entities.Bar> Delete(Data.Entities.Bar bar);
    }
}
