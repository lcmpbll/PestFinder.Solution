using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using PestFinder.Models;
using System.Collections.Generic;
using System.Linq;

namespace PestFinder.Controllers
{
  public class PestsController : Controller
  {
    private readonly PestFinderContext _db;

    public PestsController(PestFinderContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Pest> model = _db.Pests.Include(pest => pest.Location).ToList();
      return View(model);
    }
  }
}