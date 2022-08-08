using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using PestFinder.Models;
using System.Collections.Generic;
using System.Linq;

namespace PestFinder.Controllers
{
  public class ClassOneController : Controller
  {
    private readonly PestFinderContext _db;

    public ClassOneController(PestFinderContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<ClassOne> model = _db.ClassOnes.Include(classOne => classOne.ClassTwo).ToList();
      return View(model);
    }
  }
}