using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PestFinder.Models;
using System.Threading.Tasks; 
using PestFinder.ViewModels;

namespace PestFinder.Controllers
{
  public class AccountController : Controller
  {
    private readonly PestFinderContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    
    public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, PestFinderContext db)
    {
      _userManager = userManager; 
      _signInManager = signInManager;
      _db = db; 
    }
    
    public ActionResult Index()
    {
      ViewBag.Title = "Pest Finder";
      ViewBag.Subtitle = "Account Management";
      return View();
    }
    
    public IActionResult Register()
    {
      ViewBag.Title = "Pest Finder";
      ViewBag.Subtitle = "Create a new Account";
      return View();
    }
    
    [HttpPost]
    public async Task<ActionResult> Register (RegisterViewModel model)
    {
      var user = new ApplicationUser { UserName = model.Email };
      IdentityResult result = await _userManager.CreateAsync(user, model.Password);
      if (result.Succeeded)
      {
        return RedirectToAction("Index");
      }
      else
      {
        return View();
      }
    }

    public ActionResult Login ()
    {
      ViewBag.Title = "Pest Finder";
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel model)
    {
      Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
      if (result.Succeeded)
      {
        return RedirectToAction("Index");
      }
      else
      {
        return View();
      }
    }

    [HttpPost]
    public async Task<ActionResult> LogOff()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index");
    }
  }
}