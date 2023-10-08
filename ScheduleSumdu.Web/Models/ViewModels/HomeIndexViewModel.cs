using Microsoft.Build.Framework;

namespace ScheduleSumdu.Web.Models.ViewModels
{
    public class HomeIndexViewModel
    {
        [Required]
        public string SelectedGroupName { get; set; } = null!;

        public Dictionary<string, string>? ListGroups { get; set; }
    }
}
