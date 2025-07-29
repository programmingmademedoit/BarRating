using BarRating.Data.Entities;
using BarRating.Models.Bar;

namespace BarRating.Service.Bar
{
    public interface IBarService
    {
        public Task<Data.Entities.Bar> Create(CreateBarViewModel model, Data.Entities.User createdBy);
        public Task<EditBarViewModel> GetEdit(int barId);
        public Task<Data.Entities.Bar> PostEdit(EditBarViewModel model);
        public Task<Data.Entities.Bar> Delete(Data.Entities.Bar bar);
        public BarDetailViewModel Specify(int barId, int userId);
        public BarsViewModel Index();
    }
}
