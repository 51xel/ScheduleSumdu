using Microsoft.AspNetCore.Mvc;
using ScheduleSumdu.Web.Models;
using ScheduleSumdu.Web.Models.ViewModels;
using ScheduleSumdu.Web.Services.IServices;
using System.Diagnostics;

namespace ScheduleSumdu.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;
        private readonly IConfiguration _configuration;

        public HomeController( IHomeService homeService, IConfiguration configuration)
        {
            _homeService = homeService;
            _configuration = configuration;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var viewModel = new HomeIndexViewModel();

            viewModel.ListGroups = await _homeService.GetListGroupsAsync();

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetWeekAsync(string selectedGroupName)
        {
            if (ModelState.IsValid)
            {
                var getWeekViewModel = new HomeGetWeekViewModel();

                var result = await _homeService.GetWeekAsync(selectedGroupName);

                if (result == null)
                {
                    return NoContent();
                }

                getWeekViewModel.Week = result;
                getWeekViewModel.LessonTime = _configuration.GetSection("LessonTimeArray").Get<string[]>();

                return PartialView("_GetWeekPartial", getWeekViewModel);
            }

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}