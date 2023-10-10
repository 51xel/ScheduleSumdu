using ScheduleSumdu.Web.Models;
using ScheduleSumdu.Web.Models.ViewModels;

namespace ScheduleSumdu.Web.Services.IServices
{
    public interface IHomeService
    {
        public Task<Dictionary<string, string>> GetListGroupsAsync();
        public Task<Week?> GetWeekAsync(string groupName);
    }
}
