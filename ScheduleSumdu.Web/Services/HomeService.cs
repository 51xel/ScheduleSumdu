using Microsoft.Extensions.Caching.Memory;
using ScheduleSumdu.Web.Models;
using ScheduleSumdu.Web.Services.IServices;

namespace ScheduleSumdu.Web.Services
{
    public class HomeService : IHomeService
    {
        private HttpClient _httpClient { get; }
        private IMemoryCache _memoryCache { get; }
        private IConfiguration _configuration { get; }
        private ILogger<HttpClient> _logger { get; }

        public HomeService(HttpClient httpClient, IMemoryCache memoryCache, IConfiguration configuration, ILogger<HttpClient> logger)
        {
            _httpClient = httpClient;
            _memoryCache = memoryCache;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<Dictionary<string, string>?> GetListGroupsAsync()
        {
            Dictionary<string, string>? groups = null;

            if (!_memoryCache.TryGetValue("listOfGroups", out groups))
            {
                try
                {
                    groups = await _httpClient.GetFromJsonAsync<Dictionary<string, string>>(_configuration["URIGetGroups"]);

                    _memoryCache.Set(
                        "listOfGroups", 
                        groups, 
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(
                                int.Parse(_configuration["MemoryCacheListOfGroupsLifeTimeHours"]))));
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            return groups;
        }

        public async Task<Week?> GetWeekAsync(string groupName)
        {
            var groups = await GetListGroupsAsync();
            var groupId = groups.FirstOrDefault(x => x.Value == groupName);

            if (groupId.Key == null || groupId.Value == null)
            {
                return null;
            }

            var now = DateTime.Now;
            DateTime startOfWeek = now.AddDays(-(int)now.DayOfWeek + 1);

            var result = await _httpClient.GetFromJsonAsync<List<Lesson>>(string.Format(
                _configuration["URIGetSchedule"],
                groupId.Key, startOfWeek.ToShortDateString(), 
                startOfWeek.AddDays(5).ToShortDateString()
                ));
            var toReturn = new List<List<Lesson?>>();

            var week = new Week();

            int j = 0;
            for (int i = 0; i < 6; i++)
            {
                toReturn.Add(new List<Lesson?>(new Lesson?[8]));
                for (; j < result.Count && result[j].DATE_REG == startOfWeek.Date.AddDays(i).ToShortDateString(); j++)
                {
                    toReturn[i][Convert.ToInt32(result[j].NAME_PAIR[0].ToString()) - 1] = result[j];
                }

                week.Days.Add(new Day()
                {
                    Date = startOfWeek.AddDays(i).ToString("dd.MM"),
                    Lessons = toReturn[i]
                });
            }

            return week;
        }
    }
}
