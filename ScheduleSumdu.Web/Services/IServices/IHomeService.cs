using ScheduleSumdu.Web.Models.ViewModels;

namespace ScheduleSumdu.Web.Services.IServices
{
    public interface IHomeService
    {
        public Task<HomeIndexViewModel> GetListGroupsAsync(HomeIndexViewModel viewModel);
    }
}
