using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using PestFinder.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace PestFinder.Controllers
{
  [Authorize]
  public class PestsController : Controller
  {
    private readonly PestFinderContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public PestsController(UserManager<ApplicationUser> userManager, PestFinderContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userPests = _db.Pests.Where(entry => entry.User.Id == currentUser.Id).ToList();
      // List<Pest> model = _db.Pests.Include(pest => pest.Location).ToList();
      return View(userPests);
    }
    
    public ActionResult Create()
    {
      ViewBag.LocationId = new SelectList(_db.Locations, "LocationId", "Name");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Pest pest, int LocationId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      pest.User = currentUser;
      _db.Pests.Add(pest);
      _db.SaveChanges();
      if (LocationId != 0)
      {
        _db.PestLocation.Add(new PestLocation() { LocationId = LocationId, PestId = pest.PestId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

        public ActionResult Details(int id)
    {
      var thisPest = _db.Pests
        .FirstOrDefault(pest => pest.PestId == id);
      return View(thisPest);
    }

    public ActionResult Edit(int id)
    {
      var thisPest = _db.Pests.FirstOrDefault(pest => pest.PestId == id);
      ViewBag.LocationId = new SelectList(_db.Locations, "LocationId", "Name");
      return View(thisPest);
    }

    [HttpPost]
    public ActionResult Edit(Pest pest, int LocationId)
    {
      if (LocationId != 0)
      {
        _db.PestLocation.Add(new PestLocation() { LocationId = LocationId, PestId = pest.PestId });
      }
      _db.Entry(pest).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisPest = _db.Pests.FirstOrDefault(pest => pest.PestId == id);
      return View(thisPest);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisPest = _db.Pests.FirstOrDefault(pest => pest.PestId == id);
      _db.Pests.Remove(thisPest);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddLocation(int id)
    {
      var thisPest = _db.Pests.FirstOrDefault(pest => pest.PestId == id);
      ViewBag.LocationId = new SelectList(_db.Locations, "LocationId", "Name");
      return View(thisPest);
    }

    [HttpPost]
    public ActionResult AddLocation(Pest pest, int LocationId)
    {
      if (LocationId != 0)
      {
        _db.PestLocation.Add(new PestLocation() { LocationId = LocationId, PestId = pest.PestId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteLocation(int joinId)
    {
      var joinEntry = _db.PestLocation.FirstOrDefault(entry => entry.PestLocationId == joinId);
      _db.PestLocation.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}