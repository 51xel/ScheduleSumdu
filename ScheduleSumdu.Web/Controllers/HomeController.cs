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

        public HomeController( IHomeService homeService)
        {
            _homeService = homeService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeIndexViewModel();

            var result = await _homeService.GetListGroupsAsync(viewModel);

            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(HomeIndexViewModel viewModel)
        {
            //TODO Load schedule 

            var result = await _homeService.GetListGroupsAsync(viewModel);
            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}