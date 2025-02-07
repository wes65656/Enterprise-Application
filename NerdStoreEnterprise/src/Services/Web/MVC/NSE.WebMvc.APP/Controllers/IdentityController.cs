using Microsoft.AspNetCore.Mvc;
using NSE.WebMvc.APP.Models;
using NSE.WebMvc.APP.Service;

namespace NSE.WebMvc.APP.Controllers;

public class IdentityController : Controller
{
    private readonly IAutenticationService _autenticationService;

    public IdentityController(IAutenticationService autenticationService)
    {
        _autenticationService = autenticationService;
    }

    [HttpGet]
    [Route("register")]
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(UserRegister userRegister)
    {
        if (!ModelState.IsValid) return View(userRegister);

        var response = await _autenticationService.Register(userRegister);
        
        if (response == null) 
        {
            // Adicionar mensagem de erro
            return View(userRegister);
        }
        
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
       var response = await _autenticationService.Login(userLogin);
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