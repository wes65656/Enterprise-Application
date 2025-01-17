using Microsoft.AspNetCore.Mvc;
using NSE.WebMvc.APP.Models;

namespace NSE.WebMvc.APP.Controllers;

[ApiController]
public class IdentityController : Controller
{
    [HttpGet]
    [Route("Register")]
    public IActionResult Register()
    {
        if (!ModelState.IsValid) return View();
        return View();
    }
    
    [HttpPost]
    [Route("new-account")]
    public async Task<IActionResult> Register(UserRegister userRegister)
    {
        if (!ModelState.IsValid) return View(userRegister);
        // registro
        
        if (false) return View(userRegister);
        // realizar login na aplicação
        
        return RedirectToAction("Index", "Home");
    }
    
    [HttpGet]
    [Route("login")]
    public async Task<IActionResult> Login()
    {
        
        return View();
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(UserLogin userLogin)
    {
        if (!ModelState.IsValid) return View(userLogin);
        // registro
        
        if (false) return View(userLogin);
        // realizar login na aplicação
        
        return RedirectToAction("Index", "Home");
    }
    
    [HttpGet]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        return RedirectToAction("Index", "Home");
    }
}