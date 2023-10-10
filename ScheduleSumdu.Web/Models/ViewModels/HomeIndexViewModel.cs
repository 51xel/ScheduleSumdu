using Microsoft.Build.Framework;

namespace ScheduleSumdu.Web.Models.ViewModels
{
    public class HomeIndexViewModel
    {
        [Required]
        public string SelectedGroupName { get; set; } = null!;

        public Dictionary<string, string>? ListGroups { get; set; }

        public Week? Week { get; set; }

        public static string[] LessonTime { get; } = new[] { "8.30", "10.05", "11.40", "14.00", "15.35" };
    }
}
