namespace ScheduleSumdu.Web.Models
{
    public class Week
    {
        public Week()
        {
            Days = new List<Day>();
        }

        public List<Day> Days { get; set; } = null!;
    }
}
