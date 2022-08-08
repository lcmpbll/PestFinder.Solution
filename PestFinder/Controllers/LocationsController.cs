using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PestFinder.Models;
using System.Collections.Generic;
using System.Linq;

namespace PestFinder.Controllers
{
  public class LocationsController : Controller
  {
    private readonly PestFinderContext _db;

    public LocationsController(PestFinderContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Locations> model = _db.Locations.ToList();
      return View(model);
    }
  }
}