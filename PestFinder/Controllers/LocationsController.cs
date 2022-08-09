using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
  public class LocationsController : Controller
  {
    private readonly PestFinderContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public LocationsController(UserManager<ApplicationUser> userManager, PestFinderContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      ViewBag.Title = "Location";
      ViewBag.Subtitle = "All Locations";
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userLocations = _db.Locations.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userLocations);
    }

    public ActionResult Create()
    {
      ViewBag.Title = "Locations";
      ViewBag.Subtitle = "Add New Location";
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Location location, int PestId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      location.User = currentUser;
      _db.Locations.Add(location);
      _db.SaveChanges();
      if (PestId !=0)
      {
        _db.PestLocation.Add(new PestLocation() { PestId = PestId, LocationId = location.LocationId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
    public ActionResult Details(int id)
    {
      ViewBag.Title = "Locations";
      ViewBag.Subtitle = "Details";
      var thisLocation = _db.Locations
      .Include(location => location.JoinEntities)
      .ThenInclude(join => join.Pest)
      .FirstOrDefault(location => location.LocationId == id);
      return View(thisLocation);
    }

    public ActionResult Edit(int id)
    {
      ViewBag.Title = "Location";
      ViewBag.Subtitle = "Modify this Location";
      var thisLocation = _db.Locations.FirstOrDefault(location => location.LocationId == id);
      return View(thisLocation);
    }

    [HttpPost]
    public ActionResult Edit(Location location)
    {
      _db.Entry(location).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddPest(int id)
    {
      ViewBag.Title = "Location";
      ViewBag.Subtitle = "Add a Pest";
      var thisLocation = _db.Locations.FirstOrDefault(location => location.LocationId == id);
      ViewBag.PestId = new SelectList(_db.Pests, "PestId", "DateType");
      return View(thisLocation);
    }
  
    [HttpPost]
    public ActionResult AddPest(Location location, int PestId)
    {
      if (PestId != 0)
      {
        _db.PestLocation.Add(new PestLocation() {PestId = PestId, LocationId = location.LocationId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }
    
    public ActionResult Delete(int id)
    {
      ViewBag.Title = "Location";
      ViewBag.Subtitle = "Get rid of this location?";
      var thisLocation = _db.Locations.FirstOrDefault(location => location.LocationId == id);
      return View(thisLocation);
    }
    
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisLocation = _db.Locations.FirstOrDefault(location => location.LocationId == id);
      _db.Locations.Remove(thisLocation);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}