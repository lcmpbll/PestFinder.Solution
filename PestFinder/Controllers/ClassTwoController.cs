using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PestFinder.Models;
using System.Collections.Generic;
using System.Linq;

namespace PestFinder.Controllers
{
  public class ClassTwoController : Controller
  {
    private readonly PestFinderContext _db;

    public CuisineController(PestFinderContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<ClassTwo> model = _db.ClassTwo.ToList();
      return View(model);
    }
  }
}