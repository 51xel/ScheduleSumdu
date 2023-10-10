namespace ScheduleSumdu.Web.Models
{
    public class Day
    {
        public string Date { get; set; } = null!;
        public List<Lesson?> Lessons { get; set; } = null!;
    }
}
