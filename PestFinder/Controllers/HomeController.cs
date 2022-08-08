using Microsoft.AspNetCore.Mvc;

namespace PestFinder.controller
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}