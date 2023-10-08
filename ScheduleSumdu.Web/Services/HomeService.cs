using Microsoft.Extensions.Caching.Memory;
using ScheduleSumdu.Web.Models;
using ScheduleSumdu.Web.Models.ViewModels;
using ScheduleSumdu.Web.Services.IServices;
using System.Net.Http.Json;

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

        public async Task<HomeIndexViewModel> GetListGroupsAsync(HomeIndexViewModel viewModel)
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

            viewModel.ListGroups = groups;

            return viewModel;
        }
    }
}
