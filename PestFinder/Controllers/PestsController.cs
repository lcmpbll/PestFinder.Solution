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
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdenifier)?.Value;
      var currentUser = await _userManager.FindByAsync(userId);
      var userPests = db.Items.Where(entry => entry.User.Id == currentUser.Id).ToList();
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
      var currentUser = await _userManager.FindByAsync(userId);
      pest.User = currentUser;
      _db.Pests.Add(pest);
      _db.SaveChanges();
      if (LocationId !=0);
      {
        _db.LocationPest.Add(new LocationPest() { LocationId, PestId = pest.PestId });
        _db.SaveChanges();
      }
    }
  }
}